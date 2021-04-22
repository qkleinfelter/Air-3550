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
        [Required]
        public Airport Origin { get; set; }
        [Required]
        public Airport Destination { get; set; }
    }
}
