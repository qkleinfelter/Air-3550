using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Air_3550.Models
{
    public class Airport
    {
        public int AirportId { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Column(TypeName = "Decimal(8,6)")]
        public decimal Latitude { get; set; }
        [Column(TypeName = "Decimal(9,6)")]
        public decimal Longitude { get; set; }
        [Required]
        public string AirportCode { get; set; }
        [Required]
        public List<Airport> ConnectedAirports { get; set; }
    }
}
