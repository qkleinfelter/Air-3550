using System;
using System.Collections.Generic;
using System.Text;

namespace Air_3550.Models
{
    public class Route
    {
        public int RouteId { get; set; }
        public List<Flight> Flights { get; set; } = new List<Flight>();
        public Airport Origin { get; set; }
        public Airport Destination { get; set; }
        public List<Airport> Connections { get; set; } = new List<Airport>();
    }
}
