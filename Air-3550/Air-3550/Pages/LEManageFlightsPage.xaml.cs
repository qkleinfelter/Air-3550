using Air_3550.Models;
using Air_3550.Repo;
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
    public sealed partial class LEManageFlightsPage : Page
    {
        public class LEParameters
        {
            public Airport origin;
            public DateTime departDate;

            public LEParameters(Airport departPort, DateTime depart)
            {
                origin = departPort;
                departDate = depart;
            }
        }
        private LEParameters passedLEParams;

        public LEManageFlightsPage()
        {
            this.InitializeComponent();
        }

        override protected void OnNavigatedTo(NavigationEventArgs e)
        {
            // save the params
            passedLEParams = e.Parameter as LEParameters;

            // and update the header text
            DepartHeader.Text = $"{passedLEParams.origin.City} : {passedLEParams.departDate.ToShortDateString()}";

            // also generate flights around the airport and add them to the list
            DepartList.ItemsSource = GenerateFlights(passedLEParams.origin);
        }

        private static List<FlightPath> GenerateFlights(Airport originAirport)
        {
            using var db = new AirContext();
            //query database for flights with correct origin and departure date
            var LEFlights = db.Flights
                              .Include(flight => flight.Origin)
                              .Include(flight => flight.Destination)
                              .Where(flight => !flight.IsCanceled
                              && flight.Origin == originAirport)
                              .OrderBy(flight => flight.Destination)
                              .ToList();
            return LEFlights.Select(flight => new FlightPath(flight)).ToList();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoadEngineerPage));
        }

        private void DeleteFlight_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AirContext();
            // grab object that was selected from list
            var deleteFlight = DepartList.SelectedItem as FlightPath;
            // check if the flight has already been scheduled at least once
            var existingFlights = db.ScheduledFlights.Include(sf => sf.Flight)
                                                     .Where(sf => sf.Flight.FlightId == deleteFlight.flights[0].FlightId)
                                                     .ToList();
            // if the flight already exists, display an error and return
            if (existingFlights.Count > 0)
            {
                OutputInfo.Title = "You cannot delete this flight!";
                OutputInfo.Message = "This flight has already been scheduled at least once so it cannot be deleted!";
                OutputInfo.Severity = InfoBarSeverity.Error;
                OutputInfo.IsOpen = true;
                return;
            }

            // check that something was selected
            if (deleteFlight != null)
            {
                // get plane from db to delete from list
                db.Remove(db.Flights.Single(flight => flight.FlightId == deleteFlight.flights[0].FlightId));
                db.SaveChanges();

                // refresh list
                Frame.Navigate(typeof(LEManageFlightsPage), passedLEParams);
            }
            else
            {
                OutputInfo.Title = "Invalid Input!";
                OutputInfo.Message = "You must select a flight first.";
                OutputInfo.Severity = InfoBarSeverity.Error;
                OutputInfo.IsOpen = true;
            }

        }

        private void EditDepartButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateEditParameters())
            {
                // Ready to change time.
                using var db = new AirContext();
                // grab object that was selected from list
                var editFlight = DepartList.SelectedItem as FlightPath;

                // check if the flight has already been scheduled
                var existingFlights = db.ScheduledFlights.Include(sf => sf.Flight)
                                                         .Where(sf => sf.Flight.FlightId == editFlight.flights[0].FlightId)
                                                         .ToList();
                // if the flight has been scheduled you can't edit it
                if (existingFlights.Count > 0)
                {
                    OutputInfo.Title = "You cannot edit this flight!";
                    OutputInfo.Message = "This flight has already been scheduled at least once so it cannot be edited!";
                    OutputInfo.Severity = InfoBarSeverity.Error;
                    OutputInfo.IsOpen = true;
                    return;
                }

                // check that something was selected
                if (editFlight != null)
                {
                    // get time from time picker
                    TimeSpan selectedTime = (TimeSpan)timePickerAdd.SelectedTime;

                    // get plane from db to delete from list
                    db.Flights.Single(flight => flight.FlightId == editFlight.flights[0].FlightId).DepartureTime = selectedTime;
                    db.SaveChanges();

                    // refresh list
                    Frame.Navigate(typeof(LEManageFlightsPage), passedLEParams);
                }
                else
                {
                    OutputInfo.Title = "Invalid Input!";
                    OutputInfo.Message = "You must select a flight first.";
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

        private bool ValidateEditParameters()
        {
            // start with our return as true, it will get changed to false if either of our input parameters are bad
            bool valid = true;
            OutputInfo.Message = "Your search could not be processed due to invalid parameters: ";
            
            // might need check to make sure something was clicked here...

            if (!timePickerAdd.SelectedTime.HasValue)
            {
                OutputInfo.Message += "\nYou must select a valid time";
                valid = false;
            }
            return valid;
        }
    }
}
