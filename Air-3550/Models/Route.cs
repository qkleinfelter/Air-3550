using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Air_3550.Models
{
    public class Route
    {
        public int RouteId { get; set; }
        [Required]
        public List<Flight> Flights { get; set; } = new List<Flight>();
        [Required]
        public Airport Origin { get; set; }
        [Required]
        public Airport Destination { get; set; }
        [Required]
        public List<Airport> Connections { get; set; } = new List<Airport>();
    }
}
