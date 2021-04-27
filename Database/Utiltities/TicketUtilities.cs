using Air_3550.Models;
using Air_3550.Repo;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Database.Utiltities
{
    public class TicketUtilities
    {
        public static void CancelTrip(Trip trip, User user)
        {
            using (var db = new AirContext())
            {
                // Loop through all tickets
                foreach (Ticket ticket in trip.Tickets)
                {
                    var dbticket = db.Tickets.Include(ticket => ticket.Flight).Single(dbtick => dbtick.TicketId == ticket.TicketId);
                    // cancel them
                    ticket.isCanceled = true;
                    dbticket.isCanceled = true;
                    // subtract 1 ticket purchased from scheduled flight
                    ticket.Flight.TicketsPurchased -= 1;
                    dbticket.Flight.TicketsPurchased -= 1;
                    db.SaveChanges();
                }
                // award user credit based on the overall price
                UserUtilities.AwardCredit(user, trip.totalCost);
                // cancel the trip
                trip.isCanceled = true;
                var dbtrip = db.Trips.Single(dbtripinterior => dbtripinterior.TripId == trip.TripId);
                dbtrip.isCanceled = true;
                db.SaveChanges();
            }
        }

        public static List<Ticket> CreateListOfTickets(FlightPath path, PaymentType paymentType, DateTime date)
        {
            var scheduledFlights = new List<ScheduledFlight>();

            using (var db = new AirContext())
            {
                foreach (Flight flight in path.flights)
                {
                    // see if a scheduled flight exists for this flight on this day already
                    var sf = db.ScheduledFlights.Include(sf => sf.Flight).ThenInclude(fl => fl.PlaneType).Where(sf => sf.Flight.FlightId == flight.FlightId).SingleOrDefault(sf => sf.DepartureTime.Date == date);
                    if (sf != null)
                    {
                        // If a flight exists already, simply add it to our list of scheduled flights
                        scheduledFlights.Add(sf);
                    }
                    else
                    {
                        // Otherwise, we need to make a scheduled flight for it
                        var sfNew = new ScheduledFlight
                        {
                            FlightId = flight.FlightId,
                            DepartureTime = date.Add(flight.DepartureTime),
                            TicketsPurchased = 1
                        };
                        // add it to our list
                        scheduledFlights.Add(sfNew);
                        // and the db
                        db.ScheduledFlights.Add(sfNew);
                        db.SaveChanges();
                    }
                }

                var tickets = new List<Ticket>();
                foreach (ScheduledFlight sf in scheduledFlights)
                {
                    // Now we need to make a ticket for each scheduled flight
                    var ticket = new Ticket
                    {
                        Flight = sf,
                        PaymentType = paymentType
                    };
                    // Increment the amount of tickets purchased
                    sf.TicketsPurchased = sf.TicketsPurchased + 1;
                    // add it to our list to return
                    tickets.Add(ticket);
                    // and add it to the db
                    db.Tickets.Add(ticket);
                    db.SaveChanges();
                }

                // return the tickets we made
                return tickets;
            }
        }

        public static void HandlePurchase(FlightPath leavingPath, FlightPath? returningPath, DateTime leavingDate, DateTime? returningDate, PaymentType paymentType, bool oneWay)
        {
            List<Ticket> tickets = new List<Ticket>();
            // make tickets for the appropriate flight paths
            if (oneWay)
            {
                tickets = CreateListOfTickets(leavingPath, paymentType, leavingDate);
            }
            else
            {
                if (returningDate != null)
                {
                    var nonNullable = (DateTime)returningDate;
                    tickets = CreateListOfTickets(leavingPath, paymentType, leavingDate);
                    tickets.AddRange(CreateListOfTickets(returningPath, paymentType, nonNullable));
                }
            }

            // determine the total cost by whether or not the trip is one way
            int totalCost = oneWay ? leavingPath.IntPrice : leavingPath.IntPrice + returningPath.IntPrice;

            

            // add the trip to the db table, and to the customer in the db
            using (var db = new AirContext())
            {
                var custInfo = db.Users.Include(user => user.CustInfo).Single(dbuser => dbuser.UserId == UserSession.userId).CustInfo;
                // make a new trip with the appropriate info
                Trip trip = new Trip
                {
                    OriginAirportId = leavingPath.flights[0].Origin.AirportId,
                    DestinationAirportId = leavingPath.flights[leavingPath.flights.Count - 1].Destination.AirportId,
                    Tickets = tickets,
                    totalCost = totalCost,
                    CustomerInfoId = custInfo.CustomerInfoId
                };

                db.Tickets.AttachRange(tickets);
                db.Trips.Add(trip);
                db.SaveChanges();

                // Grab the users trips
                var user = db.Users.Include(dbuser => dbuser.CustInfo).Single(dbuser => dbuser.UserId == UserSession.userId);
                var customerTrips = db.Users.Include(dbuser => dbuser.CustInfo).ThenInclude(custInfo => custInfo.Trips).Single(dbuser => dbuser.LoginId == user.LoginId).CustInfo.Trips;
                customerTrips.Add(trip);
                db.SaveChanges();
            }
        }
    }
}
