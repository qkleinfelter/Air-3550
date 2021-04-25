﻿using System;
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
    }
}
