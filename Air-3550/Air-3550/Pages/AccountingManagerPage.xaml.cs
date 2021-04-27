// AccountingManagerPage.xaml.cs - Air 3550 Project
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
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Air_3550.Pages
{
    public sealed partial class AccountingManagerPage : Page
    {
        public AccountingManagerPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Generate the list of flights
            List<ScheduledFlight> flights = GenerateFlights();
            // and set our items source to the list
            FlightMoney.ItemsSource = flights;
            // count em up
            TotalFlights.Text += flights.Count;
            // calculate total earnings and display
            Earnings.Text += GetTotalMoney();
        }

        private static List<ScheduledFlight> GenerateFlights()
        {
            using var db = new AirContext();
            // grab all the scheduled flights that have taken off
            List<ScheduledFlight> SchedFlights = db.ScheduledFlights.Include(flight => flight.Flight)
                                                                        .ThenInclude(fl => fl.Origin)
                                                                    .Include(flight => flight.Flight)
                                                                        .ThenInclude(fl => fl.Destination)
                                                                    .Include(flight => flight.Flight)
                                                                        .ThenInclude(fl => fl.PlaneType)
                                                                    .Where(flight => flight.DepartureTime.CompareTo(DateTime.Now) < 0)
                                                                    .ToList();
            return SchedFlights;
        }

        private void ChangeAccountInfoButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ChangeAccountInfoPage));
        }

        private void LogoutNavigator_Click(object sender, RoutedEventArgs e)
        {
            // update session and send them back to the main page
            UserSession.userId = 0;
            UserSession.userLoggedIn = false;
            Frame.Navigate(typeof(MainPage));
        }

        private static string GetTotalMoney()
        {
            int Total = 0;
            using (var db = new AirContext())
            {
                // grab all the non canceled trips that have taken off
                var Trips = db.Trips.Where(trip => !trip.IsCanceled 
                                     && (trip.Tickets.First().Flight.DepartureTime.CompareTo(DateTime.Now) < 0))
                                    .ToList();
                // and sum the trips pricing
                foreach (var trip in Trips)
                {
                    Total += trip.TotalCost;
                }
            }

            // divide by 100 and return the string version so we have the money 
            // as dollars instead
            return (Total / 100).ToString();
        }
    }
}
