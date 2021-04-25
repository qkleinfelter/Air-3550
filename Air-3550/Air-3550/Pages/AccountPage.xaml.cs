using Air_3550.Models;
using Air_3550.Repo;
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
    public sealed partial class AccountPage : Page
    {
        private bool isCustomer = false;
        public AccountPage()
        {
            this.InitializeComponent();
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
            UserSession.userId = 0;
            UserSession.userLoggedIn = false;
            Frame.Navigate(typeof(MainPage));
        }

        override protected void OnNavigatedTo(NavigationEventArgs e)
        {
            if (isCustomer)
            {
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
                                                            .Single(dbuser => dbuser.UserId == UserSession.userId);
                CustomerInfo customerInfo = user.CustInfo;
                int newPoints = 0;
                foreach (Trip trip in customerInfo.Trips)
                {
                    // this is a bad way to do this determination but whatever
                    if (trip.getFormattedDeparted() == "Trip has departed!")
                    {
                        // make sure they haven't claimed the points already
                        if (!trip.pointsClaimed)
                        {
                            UserUtilities.AwardPoints(user, trip.totalCost / 100);
                            newPoints = trip.totalCost / 100;
                            trip.pointsClaimed = true;
                            var dbtrip = db.Trips.Single(dbtripinterior => dbtripinterior.TripId == trip.TripId);
                            dbtrip.pointsClaimed = true;
                            db.SaveChanges();
                        }
                    }
                }

                WelcomeText.Text = $"Welcome back {customerInfo.Name}!";
                PointsText.Text = $"You currently have {customerInfo.PointsAvailable + newPoints} points available, and overall you have used {customerInfo.PointsUsed} points.";
                CreditText.Text = $"You currently have a credit balance of ${customerInfo.CreditBalance / 100} with us.";
                TicketSummaryText.Text = $"You have booked {customerInfo.Trips.ToArray().Length} trips with us.";

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

            TicketUtilities.CancelTrip(SelectedTrip, user);
            OutputInfo.Title = "Flight Cancelled!";
            OutputInfo.Message = "Your trip was successfully cancelled!";
            OutputInfo.Severity = InfoBarSeverity.Success;
            OutputInfo.IsOpen = true;
        }

        private void TripList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TripList.SelectedItem != null)
            {
                cancelTrip.Visibility = Visibility.Visible;
            }
        }
    }
}
