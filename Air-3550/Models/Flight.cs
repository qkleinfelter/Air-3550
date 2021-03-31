using System;
using System.Collections.Generic;
using System.Text;

namespace Air_3550.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public Airport Origin { get; set; }
        public Airport Destination { get; set; }
        public Plane PlaneType { get; set; }
        public int Cost { get; set; }
        public int TicketsPurchased { get; set; }
    }
}
