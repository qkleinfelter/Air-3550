using Air_3550.Models;
using Air_3550.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Database.Utiltities;

namespace Air_3550.Pages
{
    public sealed partial class MMSelectPlanePage : Page //INotifyPropertyChanged
    {
        public class MMParameters
        {
            public Airport origin;
            public DateTime departDate;

            public MMParameters(Airport departPort, DateTime depart)
            {
                origin = departPort;
                departDate = depart;
            }
        }
        private MMParameters passedMMParams;
        
        public MMSelectPlanePage()
        {
            this.InitializeComponent();
        }

        override protected void OnNavigatedTo(NavigationEventArgs e)
        {
            // grab the parameters in the appropriate format
            passedMMParams = e.Parameter as MMParameters;
            // fill out the header
            DepartHeader.Text = $"{passedMMParams.origin.City} : {passedMMParams.departDate.ToShortDateString()}";
            // and the items source
            DepartList.ItemsSource = GenerateFlights(passedMMParams.origin);
        }

        private static List<FlightPath> GenerateFlights(Airport originAirport)
        {
            using var db = new AirContext();
            //query database for flights with correct origin and departure date
            var MMFlights = db.Flights
                              .Include(flight => flight.Origin)
                              .Include(flight => flight.Destination)
                              .Include(flight => flight.PlaneType)
                              .Where(flight => !flight.IsCanceled
                              && flight.Origin == originAirport)
                              .ToList();
            return MMFlights.Select(flight => new FlightPath(flight)).ToList();
        }

        private void B737_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AirContext();
            var planeChangeflight = DepartList.SelectedItem as FlightPath;

            // check that a flight was selected
            if (planeChangeflight != null)
            {
                // get plane from db to change to change plane
                var NewPlane = db.Planes.Single(plane => plane.PlaneId == 1);
                db.Flights.Single(flight => flight.FlightId == planeChangeflight.flights[0].FlightId).PlaneType = NewPlane;
                db.SaveChanges();

                // refresh list
                Frame.Navigate(typeof(MMSelectPlanePage), passedMMParams);
            }
            else
            {
                OutputInfo.Title = "Invalid Input!";
                OutputInfo.Message = "You must select a flight first.";
                OutputInfo.Severity = InfoBarSeverity.Error;
                OutputInfo.IsOpen = true;
            }
        }

        private void B747_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AirContext();
            var planeChangeflight = DepartList.SelectedItem as FlightPath;

            // check that a flight was selected
            if (planeChangeflight != null)
            {
                // get plane from db to change to change plane
                var NewPlane = db.Planes.Single(plane => plane.PlaneId == 2);
                db.Flights.Single(flight => flight.FlightId == planeChangeflight.flights[0].FlightId).PlaneType = NewPlane;
                db.SaveChanges();

                // refresh list
                Frame.Navigate(typeof(MMSelectPlanePage), passedMMParams);
            }
            else
            {
                OutputInfo.Title = "Invalid Input!";
                OutputInfo.Message = "You must select a flight first.";
                OutputInfo.Severity = InfoBarSeverity.Error;
                OutputInfo.IsOpen = true;
            }


        }

        private void B757_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AirContext();
            var planeChangeflight = DepartList.SelectedItem as FlightPath;

            // check that a flight was selected
            if (planeChangeflight != null)
            {
                // get plane from db to change to change plane
                var NewPlane = db.Planes.Single(plane => plane.PlaneId == 3);
                db.Flights.Single(flight => flight.FlightId == planeChangeflight.flights[0].FlightId).PlaneType = NewPlane;
                db.SaveChanges();

                // refresh list
                Frame.Navigate(typeof(MMSelectPlanePage), passedMMParams);
            }
            else
            {
                OutputInfo.Title = "Invalid Input!";
                OutputInfo.Message = "You must select a flight first.";
                OutputInfo.Severity = InfoBarSeverity.Error;
                OutputInfo.IsOpen = true;
            }
        }

        private void B777_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AirContext();
            var planeChangeflight = DepartList.SelectedItem as FlightPath;

            // check that a flight was selected
            if (planeChangeflight != null)
            {
                // get plane from db to change to change plane
                var NewPlane = db.Planes.Single(plane => plane.PlaneId == 4);
                db.Flights.Single(flight => flight.FlightId == planeChangeflight.flights[0].FlightId).PlaneType = NewPlane;
                db.SaveChanges();

                // refresh list
                Frame.Navigate(typeof(MMSelectPlanePage), passedMMParams);
            }
            else
            {
                OutputInfo.Title = "Invalid Input!";
                OutputInfo.Message = "You must select a flight first.";
                OutputInfo.Severity = InfoBarSeverity.Error;
                OutputInfo.IsOpen = true;
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MarketingManagerPage));
        }
    }
}
