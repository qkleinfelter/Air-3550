using Air_3550.Models;
using Air_3550.Repo;
using Database.Models;
using Database.Utiltities;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Linq;

namespace Air_3550.Pages
{
    public sealed partial class CheckoutPage : Page
    {
        public class Parameters
        {
            public FlightPath leavingPath;
            public FlightPath? returningPath;
            public DateTime departingDate;
            public DateTime? returningDate;
            public Parameters(FlightPath depart, FlightPath returning, DateTime depDate, DateTime? retDate)
            {
                leavingPath = depart;
                returningPath = returning;
                departingDate = depDate;
                returningDate = retDate;
            }
        }
        // some fields we need for this page
        private Parameters passedInParams;
        private int leavingPathCost = 0;
        private int returningPathCost = 0;
        private CustomerInfo custInfo = null;
        private bool oneWay;
        private int totalCost = 0;

        public CheckoutPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            // set our passed in params value to the parameters casted to the correct class
            passedInParams = e.Parameter as Parameters;
            // parse out the costs
            leavingPathCost = int.Parse(passedInParams.leavingPath.Price.Substring(1));
            if (passedInParams.returningPath == null)
            {
                returningPathCost = 0;
                oneWay = true; // if theres no returning path its one way
            }
            else
            {
                oneWay = !int.TryParse(passedInParams.returningPath.Price.Substring(1), out returningPathCost);
            }
            var db = new AirContext();
            var user = db.Users.Include(dbuser => dbuser.CustInfo).Single(dbuser => dbuser.UserId == UserSession.userId);
            // grab their customer info
            custInfo = user.CustInfo;
            // and display info to the user
            DisplayInfo();
        }
        
        private void DisplayInfo()
        {
            // update our total cost variable depending on whether its one way or not
            totalCost = oneWay ? leavingPathCost : leavingPathCost + returningPathCost;

            // if they have enough credit or points, display the buttons
            if (custInfo.CreditBalance >= totalCost)
            {
                useCredit.Visibility = Visibility.Visible;
            }
            if (custInfo.PointsAvailable >= 100 * (totalCost))
            {
                usePoints.Visibility = Visibility.Visible;
            }
            int numFlights = passedInParams.leavingPath.flights.Count;
            Airport origin = passedInParams.leavingPath.flights[0].Origin;
            Airport dest = passedInParams.leavingPath.flights[numFlights - 1].Destination;
            
            // display info about the departing flight(s)
            departInfo.Text = "Departing Flight Information:";
            departInfo.Text += $"\nFrom {origin.City} to {dest.City}";
            departInfo.Text += $"\nTotal Flight Duration: {passedInParams.leavingPath.FormattedDuration}";
            departInfo.Text += $"\nWith stops: {passedInParams.leavingPath.FormattedStops}";
            departInfo.Text += $"\nPrice: ${leavingPathCost}";

            if (!oneWay)
            {
                // display info about the returning flight(s) if they exist
                returnInfo.Text = "Returning Flight Information:";
                returnInfo.Text += $"\nFrom {dest.City} to {origin.City}";
                returnInfo.Text += $"\nTotal Flight Duration: {passedInParams.returningPath.FormattedDuration}";
                returnInfo.Text += $"\nWith stops: {passedInParams.returningPath.FormattedStops}";
                returnInfo.Text += $"\nPrice: ${returningPathCost}";
            }
            
            // display the total cost
            summaryInfo.Text = "Summary:";
            summaryInfo.Text += $"\nTotal Cost: ${totalCost}";

            // display the user's balances so they know what they have
            userInfo.Text = $"Your balances: Credit: ${custInfo.CreditBalance / 100}, Points: {custInfo.PointsAvailable}";
        }

        private void useCredit_Click(object sender, RoutedEventArgs e)
        {
            // grab the user
            var db = new AirContext();
            var user = db.Users.Include(dbuser => dbuser.CustInfo).Single(dbuser => dbuser.UserId == UserSession.userId);
            // use credit from their account
            UserUtilities.UseCredit(user, totalCost * 100);
            // handle the purchase
            TicketUtilities.HandlePurchase(passedInParams.leavingPath, passedInParams.returningPath, passedInParams.departingDate, passedInParams.returningDate, PaymentType.CREDIT_BALANCE, oneWay);
            // and display success & stop them from buying again
            DisplaySuccess();
            DisablePurchaseButtons();
        }

        private void usePoints_Click(object sender, RoutedEventArgs e)
        {
            // grab the user
            var db = new AirContext();
            var user = db.Users.Include(dbuser => dbuser.CustInfo).Single(dbuser => dbuser.UserId == UserSession.userId);
            // use points from their account
            UserUtilities.UsePoints(user, 100 * totalCost);
            // handle the purchase
            TicketUtilities.HandlePurchase(passedInParams.leavingPath, passedInParams.returningPath, passedInParams.departingDate, passedInParams.returningDate, PaymentType.POINTS, oneWay);
            // and display success & stop them from buying again
            DisplaySuccess();
            DisablePurchaseButtons();
        }

        private void useCreditCard_Click(object sender, RoutedEventArgs e)
        {
            // handle the purchase
            TicketUtilities.HandlePurchase(passedInParams.leavingPath, passedInParams.returningPath, passedInParams.departingDate, passedInParams.returningDate, PaymentType.CREDIT_CARD, oneWay);
            // and display success & stop them from buying again
            DisplaySuccess();
            DisablePurchaseButtons();
        }

        private void DisplaySuccess()
        {
            // Fill out the success info bar and display it
            successBar.Title = "Success! Your trip is booked!";
            successBar.Message = $"Thanks for flying with Air-3550, please choose us again soon!";
            successBar.Severity = InfoBarSeverity.Success;
            successBar.IsOpen = true;
        }

        private void DisablePurchaseButtons()
        {
            // turn off all the buttons for purchasing
            useCredit.IsEnabled = false;
            useCreditCard.IsEnabled = false;
            usePoints.IsEnabled = false;
        }

        private void backHome_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
