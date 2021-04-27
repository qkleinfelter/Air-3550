// LoadEngineerPage.xaml.cs - Air 3550 Project
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

using Air_3550.Models;
using Air_3550.Repo;
using Database.Utiltities;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;

namespace Air_3550.Pages
{
    public sealed partial class LoadEngineerPage : Page
    {
        public LoadEngineerPage()
        {
            this.InitializeComponent();
            // make sure the minimum date of the departure picker is now
            departurePicker.MinDate = DateTime.Now;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateSearchParameters())
            {
                // Bring up list of flights for Origin Airport
                using var db = new AirContext();
                // valid search params, so actually search
                string originCode = StripAirportCode(originPicker.Text);
                var originAirport = db.Airports.Single(airport => airport.AirportCode == originCode);


                var depDate = departurePicker.Date.Value.Date; // this gets only the date portion of the departure pickers chosen date
                LEManageFlightsPage.LEParameters passIn;

                passIn = new LEManageFlightsPage.LEParameters(originAirport, depDate);

                // send them to the manage flights page
                Frame.Navigate(typeof(LEManageFlightsPage), passIn);
            }
            else
            {
                OutputInfoTop.Title = "Invalid Input!";
                OutputInfoTop.Severity = InfoBarSeverity.Error;
                OutputInfoTop.IsOpen = true;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateAddParameters())
            {
                // get data from GUI and add flight
                using var db = new AirContext();
                
                string originCode = StripAirportCode(originPickerAdd.Text);
                string destCode = StripAirportCode(destPickerAdd.Text);
                var originAirport = db.Airports.Single(Airport => Airport.AirportCode == originCode);
                var destAirport = db.Airports.Single(Airport => Airport.AirportCode == destCode);
                TimeSpan selectedTime = (TimeSpan)timePickerAdd.SelectedTime;

                // Make sure that this isn't a new route
                if (EnsureExistingRoute(originAirport, destAirport))
                {
                    // Add new flight
                    // plane type defaults to 737
                    Flight flight = new()
                    {
                        Origin = originAirport,
                        Destination = destAirport,
                        PlaneType = db.Planes.Single(plane => plane.PlaneId == 1),
                        DepartureTime = selectedTime
                    };
                    db.Flights.Add(flight);
                    db.SaveChanges();
                    
                    OutputInfo.Title = "Success!";
                    OutputInfo.Message = $"Flight was successfully added!";
                    OutputInfo.Severity = InfoBarSeverity.Success;
                    OutputInfo.IsOpen = true;
                }
                else
                {
                    OutputInfo.Title = "Route Doesn't Exist!";
                    OutputInfo.Message = $"Talk to your manager if you really think we should add a brand new route.";
                    OutputInfo.Severity = InfoBarSeverity.Error;
                    OutputInfo.IsOpen = true;
                }
            }
            else
            {
                OutputInfo.Title = "Invalid Input!";
                OutputInfo.Severity = InfoBarSeverity.Error;
                OutputInfo.IsOpen = true;
            }

        }

        private bool ValidateSearchParameters()
        {
            // start with our return as true, it will get changed to false if either of our input parameters are bad
            bool valid = true;
            OutputInfoTop.Message = "Your search could not be processed due to invalid parameters: ";
            if (string.IsNullOrEmpty(originPicker.Text))
            {
                OutputInfoTop.Message += "\nYou must select an origin airport";
                valid = false;
            }
            if (!departurePicker.Date.HasValue)
            {
                OutputInfoTop.Message += "\nYou must select a valid date";
                valid = false;
            }
            return valid;
        }

        private static bool EnsureExistingRoute(Airport originAirport, Airport destinationAirport)
        {
            using var db = new AirContext();
            // This query grabs all direct flights, comments follow inline
            var direct = db.Flights // on the entire flights table of the db
                           .Include(flight => flight.Origin) // ensure that we have access to the origin airports info later
                           .Include(flight => flight.Destination) // ensure that we have access to the destination airports info later
                           .Where(flight => !flight.IsCanceled // only take flights that are not canceled (by staff member)
                           && flight.Origin == originAirport // the origin of the flight should match the origin airport passed in
                           && flight.Destination == destinationAirport) // and the destination airports should match
                           .ToList(); // then turn it into a list

            // If there are flights at this point, they are existing routes.
            // This makes them safe to add new flights.
            if (direct.Count > 0)
            {
                return true;
            }
            return false;
        }

        private bool ValidateAddParameters()
        {
            // start with our return as true, it will get changed to false if either of our input parameters are bad
            bool valid = true;
            OutputInfo.Message = "Your search could not be processed due to invalid parameters: ";
            if (string.IsNullOrEmpty(originPickerAdd.Text))
            {
                OutputInfo.Message += "\nYou must select an origin airport";
                valid = false;
            }
            if (string.IsNullOrEmpty(destPickerAdd.Text))
            {
                OutputInfo.Message += "\nYou must select a destination airport";
                valid = false;
            }
            // Don't want this displaying with both fields blank
            if (valid == true && originPickerAdd.Text == destPickerAdd.Text)
            {
                OutputInfo.Message += "\nOrigin and destination airports must be different";
                valid = false;
            }
            if (!timePickerAdd.SelectedTime.HasValue)
            {
                OutputInfo.Message += "\nYou must select a valid time";
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
            // reset the session and send the user to the main page
            UserSession.userId = 0;
            UserSession.userLoggedIn = false;
            Frame.Navigate(typeof(MainPage));
        }

        private static string StripAirportCode(string full)
        {
            return full.Substring(full.IndexOf("(") + 1, 3);
        }
    }
}
