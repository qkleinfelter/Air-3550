using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Air_3550.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        [Required]
        public Airport Origin { get; set; }
        [Required]
        public Airport Destination { get; set; }
        [Required]
        public Plane PlaneType { get; set; }
        [Required]
        public DateTime DepartureTime { get; set; }
        [Required]
        public DateTime ArrivalTime { get; set; }
        public int Cost { get; set; }
        public int TicketsPurchased { get; set; }
        public int FlightNumber { get; set; }
    }
}
