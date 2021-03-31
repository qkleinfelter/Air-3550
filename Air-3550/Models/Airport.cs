using System;
using System.Collections.Generic;
using System.Text;

namespace Air_3550.Models
{
    public class Airport
    {
        public int AirportId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string AirportCode { get; set; }
        public List<Airport> ConnectedAirports { get; set; }
    }
}
