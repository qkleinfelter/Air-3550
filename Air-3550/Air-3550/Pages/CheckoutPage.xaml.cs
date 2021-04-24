using Air_3550.Models;
using Air_3550.Repo;
using Database.Models;
using Database.Utiltities;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

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
            passedInParams = e.Parameter as Parameters;
            leavingPathCost = int.Parse(passedInParams.leavingPath.Price.Substring(1));
            oneWay = !int.TryParse(passedInParams.returningPath.Price.Substring(1), out returningPathCost);
            custInfo = UserSession.user.CustInfo;
            DisplayInfo();
        }
        
        private void DisplayInfo()
        {
            totalCost = oneWay ? leavingPathCost : leavingPathCost + returningPathCost;

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
            departInfo.Text = "Departing Flight Information:";
            departInfo.Text += $"\nFrom {origin.City} to {dest.City}";
            departInfo.Text += $"\nTotal Flight Duration: {passedInParams.leavingPath.FormattedDuration}";
            departInfo.Text += $"\nWith stops: {passedInParams.leavingPath.FormattedStops}";
            departInfo.Text += $"\nPrice: ${leavingPathCost}";

            if (!oneWay)
            {
                returnInfo.Text = "Returning Flight Information:";
                returnInfo.Text += $"\nFrom {dest.City} to {origin.City}";
                returnInfo.Text += $"\nTotal Flight Duration: {passedInParams.returningPath.FormattedDuration}";
                returnInfo.Text += $"\nWith stops: {passedInParams.returningPath.FormattedStops}";
                returnInfo.Text += $"\nPrice: ${returningPathCost}";
            }
            
            summaryInfo.Text = "Summary:";
            summaryInfo.Text += $"\nTotal Cost: ${totalCost}";

            userInfo.Text = $"Your balances: Credit: ${custInfo.CreditBalance}, Points: {custInfo.PointsAvailable}";
        }

        private void useCredit_Click(object sender, RoutedEventArgs e)
        {
            UserUtilities.UseCredit(UserSession.user, totalCost);
            TicketUtilities.HandlePurchase(passedInParams.leavingPath, passedInParams.returningPath, passedInParams.departingDate, passedInParams.returningDate, PaymentType.CREDIT_BALANCE, oneWay);
        }

        private void usePoints_Click(object sender, RoutedEventArgs e)
        {
            UserUtilities.UsePoints(UserSession.user, 100 * totalCost);
            TicketUtilities.HandlePurchase(passedInParams.leavingPath, passedInParams.returningPath, passedInParams.departingDate, passedInParams.returningDate, PaymentType.POINTS, oneWay);
        }

        private void useCreditCard_Click(object sender, RoutedEventArgs e)
        {
            TicketUtilities.HandlePurchase(passedInParams.leavingPath, passedInParams.returningPath, passedInParams.departingDate, passedInParams.returningDate, PaymentType.CREDIT_CARD, oneWay);
        }
    }
}
