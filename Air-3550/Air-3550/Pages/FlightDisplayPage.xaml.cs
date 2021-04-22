using Air_3550.Models;
using Air_3550.Repo;
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
        private Parameters passedInParams;
        public FlightDisplayPage()
        {
            this.InitializeComponent();
        }

        override protected void OnNavigatedTo(NavigationEventArgs e)
        {
            // change this parameter to an object instead of a string i need to parse later
            passedInParams = e.Parameter as Parameters;
            var returningDate = passedInParams.returningDate;
            DepartHeader.Text += " - " + passedInParams.departingDate.ToShortDateString();

            // This flight list is only for the first leg of a one way, we'll need to add another list view for
            // the return trip at some point as well
            DepartList.ItemsSource = GenerateRoutes(passedInParams.origin, passedInParams.destination);
            if (returningDate != null)
            {
                var nonNullable = (DateTime)returningDate;
                ReturnList.ItemsSource = GenerateRoutes(passedInParams.destination, passedInParams.origin);
                ReturnHeader.Text += " - " + nonNullable.ToShortDateString();
            }
            else
            {
                ReturnHeader.Visibility = Visibility.Collapsed;
                ReturnList.Visibility = Visibility.Collapsed;
            }

        }

        private List<Flight> GenerateRoutes(Airport originAirport, Airport destinationAirport)
        {
            // Right now this just generates all the one shot flights and doesn't take into account
            // scheduled flights, this is where we need to handle that generation
            var db = new AirContext();
            var validFlights = db.Flights.Include(flight => flight.Origin)
                                         .Include(flight => flight.Destination)
                                         .Where(flight => (flight.Origin.AirportId == originAirport.AirportId
                                         && (flight.Destination.AirportId == destinationAirport.AirportId)))
                                         .ToList();
            
            // My poor attempts to get exactly the flights I need...

            //var flights = db.Flights.Include(flight => flight.Origin)
            //                        .Include(flight => flight.Destination)
            //                        .Where(flight => (flight.Origin.AirportCode != destinationAirportCode))
            //                        .ToList();
            
            // Very close here, not sure how to get the AirportID from the originAirportCode so I can eliminate the Origin from the destination...
            // Have it down to 28, need the list down to the distinct 14 before going further...

            //var tempAirport = db.Airports.FromSqlRaw("SELECT AirportId WHERE AirportCode == " + originAirportCode).ToList();
            //string code = tempAirport[0].AirportCode;
            //var myFlights = db.Flights.FromSqlRaw("SELECT * FROM flights GROUP BY OriginAirportId, " +
            //    "DestinationAirportId WHERE DestinationAirportId != " + code).ToList();


            return validFlights;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void AddFlight_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
