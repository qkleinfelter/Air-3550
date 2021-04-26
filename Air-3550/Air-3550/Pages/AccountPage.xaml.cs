using Air_3550.Models;
using Air_3550.Repo;
using Database.Utiltities;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Linq;

namespace Air_3550.Pages
{
    public sealed partial class AccountPage : Page
    {
        private bool isCustomer = false;
        public AccountPage()
        {
            this.InitializeComponent();
            // after we initialize the page, determine whether we have a customer or staff account member
            var db = new AirContext();
            var user = db.Users.Include(dbuser => dbuser.CustInfo)
                                .ThenInclude(custInfo => custInfo.Trips)
                                .Single(dbuser => dbuser.UserId == UserSession.userId);

            if (user.CustInfo != null)
            {
                isCustomer = true;
            }
        }

        private void changeAccountInfoButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ChangeAccountInfoPage));
        }

        private void logoutNavigator_Click(object sender, RoutedEventArgs e)
        {
            // log the user out of the session and send them to the main page
            UserSession.userId = 0;
            UserSession.userLoggedIn = false;
            Frame.Navigate(typeof(MainPage));
        }

        override protected void OnNavigatedTo(NavigationEventArgs e)
        {
            if (isCustomer)
            {
                // if we have a customer we need to load a bunch of info
                var db = new AirContext();
                var user = db.Users.Include(dbuser => dbuser.CustInfo)
                                        .ThenInclude(custInfo => custInfo.Trips)
                                                .ThenInclude(trip => trip.Origin)
                                        .Include(user => user.CustInfo)
                                            .ThenInclude(custInfo => custInfo.Trips)
                                                .ThenInclude(trip => trip.Destination)
                                        .Include(user => user.CustInfo)
                                            .ThenInclude(custInfo => custInfo.Trips)
                                                .ThenInclude(trip => trip.Tickets)
                                                    .ThenInclude(ticket => ticket.Flight)
                                                        .ThenInclude(scheduledFlight => scheduledFlight.Flight)
                                                            .Single(dbuser => dbuser.UserId == UserSession.userId);
                CustomerInfo customerInfo = user.CustInfo;
                // award points for any trips that have departed & were not yet claimed
                int newPoints = 0;
                // loop through all of their trips
                foreach (Trip trip in customerInfo.Trips)
                {
                    // check if the trip has departed
                    if (trip.getFormattedDeparted() == "Trip has departed!")
                    {
                        // make sure they haven't claimed the points already
                        if (!trip.pointsClaimed)
                        {
                            // Award the appropriate points to the user
                            UserUtilities.AwardPoints(user, trip.totalCost / 100);
                            newPoints = trip.totalCost / 100; // keep track of this for the ui
                            trip.pointsClaimed = true; // set the points claimed to true
                            var dbtrip = db.Trips.Single(dbtripinterior => dbtripinterior.TripId == trip.TripId);
                            dbtrip.pointsClaimed = true; // and update the db as well
                            db.SaveChanges();
                        }
                    }
                }

                // Display basic info about the user
                WelcomeText.Text = $"Welcome back {customerInfo.Name}!";
                PointsText.Text = $"You currently have {customerInfo.PointsAvailable + newPoints} points available, and overall you have used {customerInfo.PointsUsed} points.";
                CreditText.Text = $"You currently have a credit balance of ${customerInfo.CreditBalance / 100} with us.";
                TicketSummaryText.Text = $"You have booked {customerInfo.Trips.ToArray().Length} trips with us.";

                // and fill our list view with the customers trips
                TripList.ItemsSource = customerInfo.Trips;
            }
        }

        private void backHome_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void cancelTrip_Click(object sender, RoutedEventArgs e)
        {
            // Can only see this button if a trip is selected, so don't need
            // to check
            var db = new AirContext();
            var user = db.Users.Include(dbuser => dbuser.CustInfo).Single(dbuser => dbuser.UserId == UserSession.userId);
            var SelectedTrip = TripList.SelectedItem as Trip;
            // If a flight has taken off already or will do so in less than an hour
            // we need to stop the cancellation
            DateTime inOneHour = DateTime.Now.AddHours(1);
            bool takenOff = false;
            foreach (Ticket ticket in SelectedTrip.Tickets)
            {
                var sf = ticket.Flight;
                DateTime departureTime = sf.DepartureTime; 
                if (departureTime < inOneHour)
                {
                    // This flight has already taken off or will in the next hour
                    takenOff = true;
                }
            }

            if (takenOff || SelectedTrip.isCanceled)
            {
                // Not allowed to cancel, so display an error
                OutputInfo.Title = "No cancellation possible";
                OutputInfo.Message = "One or more of your flights has already taken off, or will in less than an hour, so you cannot cancel this trip";
                OutputInfo.Severity = InfoBarSeverity.Error;
                OutputInfo.IsOpen = true;
                // and return
                return;
            }

            // cancel the trip
            TicketUtilities.CancelTrip(SelectedTrip, user);
            // and display a message saying we did to the user
            OutputInfo.Title = "Flight Cancelled!";
            OutputInfo.Message = "Your trip was successfully cancelled!";
            OutputInfo.Severity = InfoBarSeverity.Success;
            OutputInfo.IsOpen = true;
        }

        private void TripList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // only display the cancel trip and boarding pass buttons if a trip is selected
            if (TripList.SelectedItem != null)
            {
                cancelTrip.Visibility = Visibility.Visible;
                BoardingPass.Visibility = Visibility.Visible;
            }
        }

        private void boardingPass_Click(object sender, RoutedEventArgs e)
        {
            // send the user to the boarding pass page if its allowed
            Trip BPTrip = TripList.SelectedItem as Trip;
            if(!BPTrip.isCanceled && (DateTime.Now.CompareTo(BPTrip.Tickets[0].Flight.DepartureTime.AddDays(-1)) > 0) 
                && (DateTime.Now.CompareTo(BPTrip.Tickets[0].Flight.DepartureTime) < 0))
            {
                Frame.Navigate(typeof(BoardingPass), BPTrip);
            }
            // otherwise display an appropriate error
            else if (BPTrip.isCanceled)
            {
                OutputInfo.Title = "Flight Cancelled!";
                OutputInfo.Message = "You can not print a boarding pass for a cancelled flight";
                OutputInfo.Severity = InfoBarSeverity.Error;
                OutputInfo.IsOpen = true;
            }
            else if (DateTime.Now.CompareTo(BPTrip.Tickets[0].Flight.DepartureTime.AddDays(-1)) < 0)
            {
                OutputInfo.Title = "Too Early!";
                OutputInfo.Message = "You can not print a boarding pass more than 24 hours before a flight";
                OutputInfo.Severity = InfoBarSeverity.Error;
                OutputInfo.IsOpen = true;
            }
            else
            {
                OutputInfo.Title = "Too Late!";
                OutputInfo.Message = "You can not print a boarding pass for a flight that has already departed";
                OutputInfo.Severity = InfoBarSeverity.Error;
                OutputInfo.IsOpen = true;
            }
        }
    }
}
