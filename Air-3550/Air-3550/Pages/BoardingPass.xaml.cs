// BoardingPass.xaml.cs - Air 3550 Project
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
using Microsoft.UI.Xaml.Navigation;
using System.Linq;

namespace Air_3550.Pages
{
    public sealed partial class BoardingPass : Page
    {
        public BoardingPass()
        {
            this.InitializeComponent();
            // grab the user for the current session to display boarding passes for
            var db = new AirContext();
            var user = db.Users.Include(dbuser => dbuser.CustInfo)
                                    .ThenInclude(custInfo => custInfo.Trips)
                                .Single(dbuser => dbuser.UserId == UserSession.userId);
            BPuser = user;
        }
        private User BPuser;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // turn the parameter we passed in into a Trip variable so we can use it properly
            Trip Btrip = e.Parameter as Trip;
            // Fill out the appropriate information from the trip & user data
            FlightNumber.Text += Btrip.TripId;
            FullName.Text += BPuser.CustInfo.Name;
            AccountId.Text += BPuser.LoginId;
            Origin.Text = Btrip.Origin.AirportCode;
            Destination.Text = Btrip.Destination.AirportCode;
            // Determine whether or not the trip is one way or two way
            // by checking if the origin is equivalent to the final flights destination
            if(Btrip.OriginAirportId != Btrip.Tickets.Last().Flight.Flight.Destination.AirportId)
            {
                RoundTrip.Text = "One Way";
            }
            else
            {
                RoundTrip.Text = "Two Way";
            }
            // set the items source to the list of tickets
            flightList.ItemsSource = Btrip.Tickets;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AccountPage));
        }
    }
}
