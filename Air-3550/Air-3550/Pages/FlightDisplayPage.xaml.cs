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
            DepartList.ItemsSource = GenerateRoutes(passedInParams.origin, passedInParams.destination, passedInParams.departingDate);
            if (returningDate != null)
            {
                var nonNullable = (DateTime)returningDate;
                ReturnList.ItemsSource = GenerateRoutes(passedInParams.destination, passedInParams.origin, nonNullable);
                ReturnHeader.Text = $"{passedInParams.destination.City} to {passedInParams.origin.City} - {nonNullable.ToShortDateString()}";
                PurchaseButton.Content += "s"; // "Purchase Flight" -> "Purchase Flights" if its round trip
            }
            else
            {
                ReturnHeader.Visibility = Visibility.Collapsed;
                ReturnList.Visibility = Visibility.Collapsed;
            }

        }

        private List<FlightPath> GenerateRoutes(Airport originAirport, Airport destinationAirport, DateTime date)
        {
            // This method generates all flights with 0, 1, or 2 connections
            // returning after it finds flights with the smallest amount of connections,
            // because they will always be ideal
            // This does not return a single best path to take, as we want to provide
            // options to our consumers, so we provide ALL paths from originAirport
            // to destinationAirport with the smallest amount of connections possible

            TimeSpan minLayover = new TimeSpan(0, 40, 0); // 40 minute minimum layover
            TimeSpan maxLayover = new TimeSpan(8, 0, 0); // 8 hour maximum layover -- not in specification, but will make for nicer trips

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
                var directPaths = direct.Select(flight => new FlightPath(flight)).ToList();
                var toRemoveOne = new List<FlightPath>();
                for (int i = 0; i < directPaths.Count; i++)
                {
                    var f1 = directPaths[i].flights[0];
                    var sf1 = db.ScheduledFlights.Include(sf => sf.Flight).ThenInclude(fl => fl.PlaneType).Where(sf => sf.Flight.FlightId == f1.FlightId).SingleOrDefault(sf => sf.DepartureTime.Date == date);
                    if (sf1 != null && sf1.TicketsPurchased >= sf1.Flight.PlaneType.MaxSeats)
                    {
                        // Flight 1 already exists in db & is full
                        toRemoveOne.Add(directPaths[i]);
                        continue;
                    }
                }

                foreach (FlightPath spot in toRemoveOne)
                {
                    directPaths.Remove(spot);
                }

                if (directPaths.Count > 0)
                {
                    // If we have direct flights, they will literally always be better
                    // than non-direct in both cost and time so we can just return them and not
                    // calculate any worse flights
                    // also convert them to a FlightPath with a single flight, so that
                    // we can display them properly
                    return directPaths;
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
                                     // only do this where the connections destination, is the overall destination of the trip
                                     select new FlightPath(flight, connection); // turn the results into a new flight path, with both the first flight and the connection

                var twoLeggedFlights = twoLeggedQuery.ToList(); // turn the results of our query into a list so that we can return it nicely

                var toRemoveTwo = new List<FlightPath>();
                for (int i = 0; i < twoLeggedFlights.Count; i++)
                {
                    FlightPath route = twoLeggedFlights[i];
                    Flight f1 = route.flights[0];
                    Flight f2 = route.flights[1];
                    TimeSpan f1Arrive = f1.GetArrivalTime();
                    if (f1Arrive > f2.DepartureTime)
                    {
                        // Arrival time is after departure of the other flight
                        toRemoveTwo.Add(route);
                        continue;
                    }

                    if (f2.DepartureTime - f1.GetArrivalTime() < minLayover)
                    {
                        // Arrival time is less than 40 minutes before next flight would take off, so we don't offer
                        toRemoveTwo.Add(route);
                        continue;
                    }

                    if (f2.DepartureTime - f1.GetArrivalTime() > maxLayover)
                    {
                        // Arrival time is more than 8 hours before next flight would take off, so we don't offer
                        toRemoveTwo.Add(route);
                        continue;
                    }

                    var sf1 = db.ScheduledFlights.Include(sf => sf.Flight).ThenInclude(fl => fl.PlaneType).Where(sf => sf.Flight.FlightId == f1.FlightId).SingleOrDefault(sf => sf.DepartureTime.Date == date);
                    var sf2 = db.ScheduledFlights.Include(sf => sf.Flight).ThenInclude(fl => fl.PlaneType).Where(sf => sf.Flight.FlightId == f2.FlightId).SingleOrDefault(sf => sf.DepartureTime.Date == date);
                    if (sf1 != null && sf1.TicketsPurchased >= sf1.Flight.PlaneType.MaxSeats)
                    {
                        // Flight 1 already exists in db & is full
                        toRemoveTwo.Add(route);
                        continue;
                    }
                    if (sf2 != null && sf2.TicketsPurchased >= sf2.Flight.PlaneType.MaxSeats)
                    {
                        // Flight 2 already exists in db & is full
                        toRemoveTwo.Add(route);
                        continue;
                    }
                }

                foreach (FlightPath spot in toRemoveTwo)
                {
                    twoLeggedFlights.Remove(spot);
                }

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
                                       // only do this where the secondConnections destination is the overall destination of the trip
                                       select new FlightPath(flight, connection, secondConnection); // turn the results into a new flight path, with all 3 flights

                var threeLeggedFlights = threeLeggedQuery.ToList(); // turn the results of our query into a list so that we can return it nicely
                var toRemoveThree = new List<FlightPath>();
                for (int i = 0; i < threeLeggedFlights.Count; i++)
                {
                    FlightPath route = threeLeggedFlights[i];
                    Flight f1 = route.flights[0];
                    Flight f2 = route.flights[1];
                    Flight f3 = route.flights[2];
                    TimeSpan f1Arrive = f1.GetArrivalTime();
                    TimeSpan f2Arrive = f2.GetArrivalTime();
                    
                    if (f1Arrive > f2.DepartureTime || f2Arrive > f3.DepartureTime)
                    {
                        // Arrival time is after departure of the other flight
                        // on either leg, so we remove
                        toRemoveThree.Add(route);
                        continue;
                    }

                    if (f2.DepartureTime - f1Arrive < minLayover || f3.DepartureTime - f2Arrive < minLayover)
                    {
                        // Arrival time is less than 40 minutes before next flight would take off, so we don't offer
                        // on either leg
                        toRemoveThree.Add(route);
                        continue;
                    }

                    if (f2.DepartureTime - f1Arrive > maxLayover || f3.DepartureTime - f2Arrive > maxLayover)
                    {
                        // Arrival time is more than 8 hours before next flight would take off, so we don't offer
                        // on either leg
                        toRemoveThree.Add(route);
                        continue;
                    }

                    var sf1 = db.ScheduledFlights.Include(sf => sf.Flight).ThenInclude(fl => fl.PlaneType).Where(sf => sf.Flight.FlightId == f1.FlightId).SingleOrDefault(sf => sf.DepartureTime.Date == date);
                    var sf2 = db.ScheduledFlights.Include(sf => sf.Flight).ThenInclude(fl => fl.PlaneType).Where(sf => sf.Flight.FlightId == f2.FlightId).SingleOrDefault(sf => sf.DepartureTime.Date == date);
                    var sf3 = db.ScheduledFlights.Include(sf => sf.Flight).ThenInclude(fl => fl.PlaneType).Where(sf => sf.Flight.FlightId == f3.FlightId).SingleOrDefault(sf => sf.DepartureTime.Date == date);
                    if (sf1 != null && sf1.TicketsPurchased >= sf1.Flight.PlaneType.MaxSeats)
                    {
                        // Flight 1 already exists in db & is full
                        toRemoveThree.Add(route);
                        continue;
                    }
                    if (sf2 != null && sf2.TicketsPurchased >= sf2.Flight.PlaneType.MaxSeats)
                    {
                        // Flight 2 already exists in db & is full
                        toRemoveThree.Add(route);
                        continue;
                    }
                    if (sf3 != null && sf3.TicketsPurchased >= sf3.Flight.PlaneType.MaxSeats)
                    {
                        // Flight 3 already exists in db & is full
                        toRemoveThree.Add(route);
                        continue;
                    }
                }

                foreach (FlightPath spot in toRemoveThree)
                {
                    threeLeggedFlights.Remove(spot);
                }

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
            // than 2 connections, so we error out and return a blank list
            outputInfo.Title = "No flights available!";
            outputInfo.Message = "Unfortunately, we don't have any flights available on this route at this time, sorry.";
            outputInfo.Severity = InfoBarSeverity.Error;
            outputInfo.IsOpen = true;
            return new List<FlightPath>();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void PurchaseButton_Click(object sender, RoutedEventArgs e)
        {
            var departRoute = DepartList.SelectedItem as FlightPath;
            FlightPath returnRoute = null;
            if (passedInParams.returningDate != null)
            {
                returnRoute = ReturnList.SelectedItem as FlightPath;
            }
            Frame.Navigate(typeof(CheckoutPage), new CheckoutPage.Parameters(departRoute, returnRoute));
        }
    }
}
