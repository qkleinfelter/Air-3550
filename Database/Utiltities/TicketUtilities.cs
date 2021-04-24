using Air_3550.Models;
using Air_3550.Repo;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Utiltities
{
    public class TicketUtilities
    {
        public static void CancelFlightTicket(Ticket ticket)
        {
            
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
                    // add it to our list to return
                    tickets.Add(ticket);
                    // and add it to the db
                    db.Tickets.Add(ticket);
                    db.SaveChanges();
                }

                return tickets;
            }
        }

        public static void HandlePurchase(FlightPath leavingPath, FlightPath? returningPath, DateTime leavingDate, DateTime? returningDate, PaymentType paymentType, bool oneWay)
        {
            List<Ticket> tickets = new List<Ticket>();
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

            Trip trip = new Trip
            {
                OriginAirportId = leavingPath.flights[0].Origin.AirportId,
                DestinationAirportId = leavingPath.flights[leavingPath.flights.Count - 1].Destination.AirportId,
                Tickets = tickets
            };

            // add the trip to the db table, and to the customer in the db
            using (var db = new AirContext())
            {
                db.Tickets.AttachRange(tickets);
                db.Trips.Add(trip);
                db.SaveChanges();

                // Grab the users trips
                var customerTrips = db.Users.Include(user => user.CustInfo).ThenInclude(custInfo => custInfo.Trips).Single(user => user.LoginId == UserSession.user.LoginId).CustInfo.Trips;
                customerTrips.Add(trip);
                db.SaveChanges();
            }
        }
    }
}
