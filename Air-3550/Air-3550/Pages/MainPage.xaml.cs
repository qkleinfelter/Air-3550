// MainPage.xaml.cs - Air 3550 Project
//
// This program acts as a flight reservation system for a new airline - called Air-3550,
// which allows customers to book and manage trips, which contain many flights, as well as enabling various staff members
// to update the available flights and view statistics about them individually or as a whole.
//
// Authors:     Quinn Kleinfelter, James Golden, & Edward Walsh
// Class:       EECS 3550-001 Software Engineering, Spring 2021
// Instructor:  Dr. Thomas
// Date:        April 28, 2021
// Copyright:   Copyright 2021 by Quinn Kleinfelter, James Golden, & Edward Walsh. All rights reserved.

using Air_3550.Repo;
using Database.Utiltities;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;

namespace Air_3550.Pages
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            // if theres a user logged in, show the account navigator
            // and not the login navigator
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

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateSearchParameters())
            {
                using var db = new AirContext();
                // valid search params, so actually search
                // grab the airports
                string originCode = StripAirportCode(originPicker.Text);
                var originAirport = db.Airports.Single(airport => airport.AirportCode == originCode);
                string destCode = StripAirportCode(destPicker.Text);
                var destAirport = db.Airports.Single(airport => airport.AirportCode == destCode);

                var depDate = departurePicker.Date.Value.Date; // this gets only the date portion of the departure pickers chosen date
                FlightDisplayPage.Parameters passIn;
                // navigate to the flight display page with the appropriate parameters
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
                // otherwise display an error because they messed up in search params
                OutputInfo.Title = "Invalid Input!";
                OutputInfo.Severity = InfoBarSeverity.Error;
                OutputInfo.IsOpen = true;
            }
        }

        private void LoginNavigator_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }

        private void AccountNavigator_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AccountPage));
        }

        private bool ValidateSearchParameters()
        {
            // start with our return as true, it will get changed to false if any of our input parameters are bad
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
            if (returnPicker.IsEnabled && returnPicker.Date == null)
            {
                OutputInfo.Message += "\nYou must select a return date for round-trip flights!";
                valid = false;
            }
            if (!UserSession.userLoggedIn)
            {
                OutputInfo.Message += "\nYou must be logged in to search for flights";
                valid = false;
            }
            // return whether or not the search boxes are valid
            return valid;
        }

        // helper method to grab the airport code from a string that matches the format of our input boxes
        private static string StripAirportCode(string full)
        {
            return full.Substring(full.IndexOf("(") + 1, 3);
        }

        private void RadioSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // if we have the round trip box selected, make sure that they can access
            // the return date picker
            // otherwise they can't
            if (sender is RadioButtons radioButtons)
            {
                if (radioButtons.SelectedIndex == 0)
                {
                    // Round-Trip
                    returnPicker.IsEnabled = true;
                }
                else
                {
                    // One-Way
                    returnPicker.IsEnabled = false;
                }
            }
        }
    }
}
