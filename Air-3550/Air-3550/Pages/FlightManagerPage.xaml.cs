// FlightManagerPage.xaml.cs - Air 3550 Project
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
using Database.Models;
using Database.Utiltities;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Air_3550.Pages
{
    public sealed partial class FlightManagerPage : Page
    {
        public FlightManagerPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // grab all the departed flights
            var departedFlights = GenerateFlights();

            // if we don't have any departed flights,
            // hide some things and display an error
            if (departedFlights.Count == 0)
            {
                ListHeader.Visibility = Visibility.Collapsed;
                AvailableFlights.Visibility = Visibility.Collapsed;
                OutputInfo.Title = "Sorry no flights to show!";
                OutputInfo.Message = "No flights have departed yet";
                OutputInfo.Severity = InfoBarSeverity.Error;
                OutputInfo.IsOpen = true;
            }
            // set the items source for the flights list
            AvailableFlights.ItemsSource = departedFlights;
        }

        private static List<ScheduledFlight> GenerateFlights()
        {
            // generate a list of all the scheduled flights that have departed
            using var db = new AirContext();
            List<ScheduledFlight> ScheduledFlights = db.ScheduledFlights.Include(flight => flight.Flight)
                                                                            .ThenInclude(fl => fl.Origin)
                                                                        .Include(flight => flight.Flight)
                                                                            .ThenInclude(fl => fl.Destination)
                                                                        .Include(flight => flight.Flight)
                                                                            .ThenInclude(fl => fl.PlaneType)
                                                                        .ToList();
            List<ScheduledFlight> toRemove = new();
            // remove any flight where the departure time is after now
            foreach (ScheduledFlight sf in ScheduledFlights)
            {
                if (sf.DepartureTime > DateTime.Now)
                {
                    toRemove.Add(sf);
                }
            }

            foreach (ScheduledFlight sf in toRemove)
            {
                ScheduledFlights.Remove(sf);
            }

            // then return the updated list
            return ScheduledFlights;
        }

        // Manage Account & logout at top right of page
        private void ChangeAccountInfoButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ChangeAccountInfoPage));
        }

        private void LogoutNavigator_Click(object sender, RoutedEventArgs e)
        {
            // update session and send the user to the main page
            UserSession.userId = 0;
            UserSession.userLoggedIn = false;
            Frame.Navigate(typeof(MainPage));
        }

        private void ShowManifest_Click(object sender, RoutedEventArgs e)
        {
            // if there is no selected item return
            if (AvailableFlights.SelectedItem == null)
            {
                return;
            }
            else
            {
                // otherwise, send them to the appropriate flight manifest page
                ScheduledFlight sf = AvailableFlights.SelectedItem as ScheduledFlight;
                Frame.Navigate(typeof(FlightManifestPage), sf);
            }
        }

        private void AvailableFlights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // only show the manifest button if they have selected a flight
            if (AvailableFlights.SelectedItem != null)
            {
                ShowManifest.Visibility = Visibility.Visible;
            }
        }
    }
}
