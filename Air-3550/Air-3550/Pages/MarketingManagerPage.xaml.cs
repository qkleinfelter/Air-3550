// MarketingManagerPage.xaml.cs - Air 3550 Project
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
    public sealed partial class MarketingManagerPage : Page
    {
        public MarketingManagerPage()
        {
            this.InitializeComponent();
            // minimum date is today
            departurePicker.MinDate = DateTime.Now;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateSearchParameters())
            {
                using var db = new AirContext();
                // valid search params, so actually search
                string originCode = StripAirportCode(originPicker.Text);
                var originAirport = db.Airports.Single(airport => airport.AirportCode == originCode);
                
                var depDate = departurePicker.Date.Value.Date; // this gets only the date portion of the departure pickers chosen date
                MMSelectPlanePage.MMParameters passIn;
                
                passIn = new MMSelectPlanePage.MMParameters(originAirport, depDate);
                // send them to the selection page
                Frame.Navigate(typeof(MMSelectPlanePage), passIn);
            }
            else
            {
                OutputInfo.Title = "Invalid Input!";
                OutputInfo.Severity = InfoBarSeverity.Error;
                OutputInfo.IsOpen = true;
            }
        }

        private static string StripAirportCode(string full)
        {
            return full.Substring(full.IndexOf("(") + 1, 3);
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
            if (departurePicker.Date == null)
            {
                OutputInfo.Message += "\nYou must select a departure date!";
                valid = false;
            }
            return valid;
        }

        private void ChangeAccountInfoButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ChangeAccountInfoPage));
        }

        private void LogoutNavigator_Click(object sender, RoutedEventArgs e)
        {
            // reset the user session and send them to the main page
            UserSession.userId = 0;
            UserSession.userLoggedIn = false;
            Frame.Navigate(typeof(MainPage));
        }
    }
}
