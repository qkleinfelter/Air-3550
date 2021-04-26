using Air_3550.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
    public class ScheduledFlight
    {
        public int ScheduledFlightId { get; set; }
        public int FlightId { get; set; }
        [Required]
        public Flight Flight { get; set; }
        [Required]
        public DateTime DepartureTime { get; set; }
        public int TicketsPurchased { get; set; }
    }
}
