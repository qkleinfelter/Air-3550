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
using Windows.Networking.Connectivity;
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
            DepartHeader.Text = $"{passedInParams.origin.City} to {passedInParams.destination.City} - {passedInParams.departingDate.ToShortDateString()}";

            // This flight list is only for the first leg of a one way, we'll need to add another list view for
            // the return trip at some point as well
            DepartList.ItemsSource = GenerateRoutes(passedInParams.origin, passedInParams.destination);
            if (returningDate != null)
            {
                var nonNullable = (DateTime)returningDate;
                ReturnList.ItemsSource = GenerateRoutes(passedInParams.destination, passedInParams.origin);
                ReturnHeader.Text = $"{passedInParams.destination.City} to {passedInParams.origin.City} - {nonNullable.ToShortDateString()}";
                PurchaseButton.Content += "s"; // "Purchase Flight" -> "Purchase Flights" if its round trip
            }
            else
            {
                ReturnHeader.Visibility = Visibility.Collapsed;
                ReturnList.Visibility = Visibility.Collapsed;
            }

        }

        private List<FlightPath> GenerateRoutes(Airport originAirport, Airport destinationAirport)
        {
            // This method generates all flights with 0, 1, or 2 connections
            // returning after it finds flights with the smallest amount of connections,
            // because they will always be ideal
            // This does not return a single best path to take, as we want to provide
            // options to our consumers, so we provide ALL paths from originAirport
            // to destinationAirport with the smallest amount of connections possible
            // TODO: Handle layover timing, we'll need a minimum layover of 40 minutes,
            // and we probably want a maximum layover too, to prevent the system from offering
            // you to wait overnight
            
            using (var db = new AirContext())
            {
                // This query grabs all direct flights, comments follow inline
                var direct = db.Flights // on the entire flights table of the db
                               .Include(flight => flight.Origin) // ensure that we have access to the origin airports info later
                               .Include(flight => flight.Destination) // ensure that we have access to the destination airports info later
                               .Where(flight => !flight.isCanceled // only take flights that are not canceled (by staff member)
                               && flight.Origin == originAirport // the origin of the flight should match the origin airport passed in
                               && flight.Destination == destinationAirport) // and the destination airports should match
                               .ToList(); // then turn it into a list

                if (direct.Count > 0)
                {
                    // If we have direct flights, they will literally always be better
                    // than non-direct in both cost and time so we can just return them and not
                    // calculate any worse flights
                    // also convert them to a FlightPath with a single flight, so that
                    // we can display them properly
                    return direct.Select(flight => new FlightPath(flight)).ToList();
                }

                // this query grabs all non canceled flights from the db
                var flights = db.Flights // on the entire flights table of the db
                               .Include(flight => flight.Origin) // ensure that we have access to the origin airports info later
                               .Include(flight => flight.Destination) // ensure that we have access to the destination airports info later
                               .Where(flight => !flight.isCanceled); // only take flights that are not canceled (by staff member)
                // this query uses the flights query we just made to grab all flights
                // with 2 legs, i.e. 1 connection from the db
                TimeSpan ts = new TimeSpan(0, 40, 0);
                var twoLeggedQuery = from flight in flights // for each flight in the flights variable
                                     where flight.Origin == originAirport // where the origin of the flight is our origin airport
                                     // join in a new flight "connection" from the flights variable, that has the same origin as our first flights destination
                                     join connection in flights on flight.Destination equals connection.Origin
                                     where connection.Destination == destinationAirport 
                                     //&& TimeSpan.Compare(flight.GetArrivalTime(), connection.DepartureTime.Subtract(ts)) == -1)
                                     // only do this where the connections destination, is the overall destination of the trip and the connection departs 40 min 
                                     // after first flight arrives
                                     select new FlightPath(flight, connection); // turn the results into a new flight path, with both the first flight and the connection

                var twoLeggedFlights = twoLeggedQuery.ToList(); // turn the results of our query into a list so that we can return it nicely
                if (twoLeggedFlights.Count > 0)
                {
                    // Again if we have 2 legged flights, they will always be better 
                    // than 3 legged flights in cost & time, so we can return them
                    // and not calculate any worse flights
                    return twoLeggedFlights;
                }
                
                // this query uses the flights query we made earlier to grab all flights
                // with 3 legs, i.e. 2 connections from the db
                var threeLeggedQuery = from flight in flights // for each flight in the flights variable
                                       where flight.Origin == originAirport // where the origin of our flight is our origin airport
                                       // join in a new flight "connection" from the flights variable, that has the same origin as our first flights destination
                                       join connection in flights on flight.Destination equals connection.Origin
                                       // join in a new flight "secondConnection" from the flights variable, that has the same origin as our first connections destination
                                       join secondConnection in flights on connection.Destination equals secondConnection.Origin
                                       where secondConnection.Destination == destinationAirport
                                       //&& TimeSpan.Compare(connection.GetArrivalTime(), secondConnection.DepartureTime.Subtract(ts)) == -1)
                                       // only do this where the secondConnections destination is the overall destination of the trip
                                       select new FlightPath(flight, connection, secondConnection); // turn the results into a new flight path, with all 3 flights

                var threeLeggedFlights = threeLeggedQuery.ToList(); // turn the results of our query into a list so that we can return it nicely
                if (threeLeggedFlights.Count > 0)
                {
                    // Finally return our 3 legged flights
                    return threeLeggedFlights;
                }
            }

            // if we made it out of the algorithm without returning, then that means
            // that it is not possible to get from originAirport to destinationAirport 
            // with 2 or fewer connections.
            // according to the specifications of this project, we cannot have more
            // than 2 connections, so we return a blank list
            return new List<FlightPath>();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void PurchaseButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
