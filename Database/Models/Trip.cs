using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Air_3550.Models
{
    public class Trip
    {
        public int TripId { get; set; }
        [Required]
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
        public int OriginAirportId { get; set; }
        [Required]
        public Airport Origin { get; set; }
        public int DestinationAirportId { get; set; }
        [Required]
        public Airport Destination { get; set; }
        public bool isCanceled { get; set; }
        public int totalCost { get; set; }

        public string getFormattedCancelation()
        {
            if (isCanceled)
            {
                return "Trip Canceled!";
            } 
            else
            {
                return "";
            }
        }

        public string getFormattedDeparted()
        {
            if (isCanceled)
            {
                // if the trip is canceled we don't want to display both messages
                return "";
            }
            DateTime now = DateTime.Now;
            foreach (Ticket ticket in Tickets)
            {
                var sf = ticket.Flight;
                DateTime departureTime = sf.DepartureTime;
                if (departureTime < now)
                {
                    // This flight has already taken off or will in the next hour
                    return "Trip has departed!";
                }
            }
            return "";
        }

        public string getFormattedDateString()
        {
            Ticket first = Tickets[0];
            Ticket last = Tickets[Tickets.Count - 1];

            if (first.Flight.DepartureTime.ToShortDateString() != last.Flight.DepartureTime.ToShortDateString())
            {
                // only show 2 dates if they're different
                return first.Flight.DepartureTime.ToShortDateString() + " to " + last.Flight.DepartureTime.ToShortDateString();
            }
            else
            {
                return first.Flight.DepartureTime.ToShortDateString();
            }
        }
    }
}
