// FlightDisplayPage.xaml.cs - Air 3550 Project
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
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using Database.Utiltities;

namespace Air_3550.Pages
{
    public sealed partial class FlightDisplayPage : Page
    {
        public class Parameters
        {
            public Airport origin;
            public Airport destination;
            public DateTime departingDate;
            public DateTime? returningDate;
            public Parameters(Airport originAirport, Airport destinationAirport, DateTime depart, DateTime returnD)
            {
                origin = originAirport;
                destination = destinationAirport;
                departingDate = depart;
                returningDate = returnD;
            }
            public Parameters(Airport originAirport, Airport destinationAirport, DateTime depart)
            {
                origin = originAirport;
                destination = destinationAirport;
                departingDate = depart;
                returningDate = null;
            }
        }
        // parameters that we need to be able to display flights
        private Parameters passedInParams;
        public FlightDisplayPage()
        {
            this.InitializeComponent();
        }

        override protected void OnNavigatedTo(NavigationEventArgs e)
        {
            // grab the parameters as an object
            passedInParams = e.Parameter as Parameters;
            var returningDate = passedInParams.returningDate;
            // fill out the departing list header
            DepartHeader.Text = $"{passedInParams.origin.City} to {passedInParams.destination.City} - {passedInParams.departingDate.ToShortDateString()}";

            // generate the departing paths
            var possibleDepartingPaths = FlightAlgorithm.GenerateRoutes(passedInParams.origin, passedInParams.destination, passedInParams.departingDate);

            if (possibleDepartingPaths.Count == 0)
            {
                outputInfo.Title = "No flights available!";
                outputInfo.Message = "Unfortunately, we don't have any flights available on this route at this time, sorry.";
                outputInfo.Severity = InfoBarSeverity.Error;
                outputInfo.IsOpen = true;
            }

            // and set the list views item source to the departing paths
            DepartList.ItemsSource = possibleDepartingPaths;

            if (returningDate != null)
            {
                // if we have a return date, generate flights for it
                var nonNullable = (DateTime)returningDate;
                var possibleReturningPaths = FlightAlgorithm.GenerateRoutes(passedInParams.destination, passedInParams.origin, nonNullable);

                if (possibleReturningPaths.Count == 0)
                {
                    outputInfo.Title = "No flights available!";
                    outputInfo.Message = "Unfortunately, we don't have any flights available on this route at this time, sorry.";
                    outputInfo.Severity = InfoBarSeverity.Error;
                    outputInfo.IsOpen = true;
                }

                // and set the list views item source to the return paths
                ReturnList.ItemsSource = possibleReturningPaths;
                // also fill out the return header
                ReturnHeader.Text = $"{passedInParams.destination.City} to {passedInParams.origin.City} - {nonNullable.ToShortDateString()}";
                PurchaseButton.Content += "s"; // "Purchase Flight" -> "Purchase Flights" if its round trip
            }
            else
            {
                // otherwise, hide the return items because its a one way trip
                ReturnHeader.Visibility = Visibility.Collapsed;
                ReturnList.Visibility = Visibility.Collapsed;
            }

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void PurchaseButton_Click(object sender, RoutedEventArgs e)
        {
            // grab the flight paths from the list views
            var departRoute = DepartList.SelectedItem as FlightPath;
            FlightPath returnRoute = null;           
            if (passedInParams.returningDate != null)
            {
                returnRoute = ReturnList.SelectedItem as FlightPath;
            }
            // and navigate to the checkoutpage with the appropriate parameters being passed in
            Frame.Navigate(typeof(CheckoutPage), new CheckoutPage.Parameters(departRoute, returnRoute, passedInParams.departingDate, passedInParams.returningDate));
        }
    }
}
