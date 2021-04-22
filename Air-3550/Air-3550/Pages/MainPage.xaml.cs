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
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            if (UserSession.userLoggedIn)
            {
                loginNavigator.Visibility = Visibility.Collapsed;
                accountNavigator.Visibility = Visibility.Visible;
            }
            // Can't select dates earlier than today
            departurePicker.MinDate = DateTime.Now;
            returnPicker.MinDate = DateTime.Now;
            // or dates farther than 6 months in the future
            departurePicker.MaxDate = DateTime.Now.AddMonths(6);
            returnPicker.MaxDate = DateTime.Now.AddMonths(6);
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateSearchParameters())
            {
                using var db = new AirContext();
                // valid search params, so actually search
                string originCode = StripAirportCode(originPicker.Text);
                var originAirport = db.Airports.Single(airport => airport.AirportCode == originCode);
                string destCode = StripAirportCode(destPicker.Text);
                var destAirport = db.Airports.Single(airport => airport.AirportCode == destCode);

                var depDate = departurePicker.Date.Value.Date; // this gets only the date portion of the departure pickers chosen date
                FlightDisplayPage.Parameters passIn;
                if (returnPicker.Date != null)
                {
                    passIn = new FlightDisplayPage.Parameters(originAirport, destAirport, depDate, returnPicker.Date.Value.Date);

                }
                else
                {
                    passIn = new FlightDisplayPage.Parameters(originAirport, destAirport, depDate);
                }

                Frame.Navigate(typeof(FlightDisplayPage), passIn);
            }
            else
            {
                OutputInfo.Title = "Invalid Input!";
                OutputInfo.Severity = InfoBarSeverity.Error;
                OutputInfo.IsOpen = true;
            }
        }

        private void loginNavigator_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }

        private void accountNavigator_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AccountPage));
        }

        private bool ValidateSearchParameters()
        {
            // start with our return as true, it will get changed to false if either of our input parameters are bad
            bool valid = true;
            OutputInfo.Message = "Your search could not be processed due to invalid parameters: ";
            if (string.IsNullOrEmpty(originPicker.Text))
            {
                OutputInfo.Message += "\nYou must select an origin airport";
                valid = false;
            }
            if (string.IsNullOrEmpty(destPicker.Text))
            {
                OutputInfo.Message += "\nYou must select a destination airport";
                valid = false;
            }
            if (departurePicker.Date == null)
            {
                OutputInfo.Message += "\nYou must select a departure date!";
                valid = false;
            }
            if (departurePicker.Date > returnPicker.Date)
            {
                OutputInfo.Message += "\nReturning date must be later than departure date!";
                valid = false;
            }
            if (originPicker.Text == destPicker.Text)
            {
                OutputInfo.Message += "\nOrigin and Destination cannot be the same!";
                valid = false;
            }
            // don't validate if there is anything in the return picker, because we want to allow 1 way flights
            return valid;
        }

        private string StripAirportCode(string full)
        {
            return full.Substring(full.IndexOf("(") + 1, 3);
        }
    }
}
