﻿using Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Air_3550.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        [Required]
        public ScheduledFlight Flight { get; set; }
        public int Price { get; set; }
        [Required]
        public PaymentType PaymentType { get; set; }
        [Required]
        public Trip Trip { get; set; }
    }
}
