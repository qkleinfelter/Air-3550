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
                CustomerInfo customerInfo = user.CustInfo;
                WelcomeText.Text = $"Welcome back {customerInfo.Name}!";
                PointsText.Text = $"You currently have {customerInfo.PointsAvailable} points available, and overall you have used {customerInfo.PointsUsed} points.";
                CreditText.Text = $"You currently have a credit balance of ${customerInfo.CreditBalance} with us.";
                TicketSummaryText.Text = $"You have booked {customerInfo.Trips.ToArray().Length} trips with us.";
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
                                                .ThenInclude(trip => trip.Tickets).Single(dbuser => dbuser.UserId == UserSession.userId);
                CustomerInfo customerInfo = user.CustInfo;
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
            // TODO: Check if any flight is scheduled to take off in less than 1 hr
            // and confirm the cancellation
            var db = new AirContext();
            var user = db.Users.Include(dbuser => dbuser.CustInfo).Single(dbuser => dbuser.UserId == UserSession.userId);
            var SelectedTrip = TripList.SelectedItem as Trip;
            TicketUtilities.CancelTrip(SelectedTrip, user);
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
