using Air_3550.Models;
using Database.Utiltities;
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
            if (UserSession.user.CustInfo != null)
            {
                isCustomer = true;
                CustomerInfo customerInfo = UserSession.user.CustInfo;
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
            UserSession.user = null;
            UserSession.userLoggedIn = false;
            Frame.Navigate(typeof(MainPage));
        }

        override protected void OnNavigatedTo(NavigationEventArgs e)
        {
            if (isCustomer)
            {
                CustomerInfo customerInfo = UserSession.user.CustInfo;
                TripList.ItemsSource = customerInfo.Trips;
            }
        }

        private void backHome_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
