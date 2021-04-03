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
        public Flight Flight { get; set; }
        public int Price { get; set; }
        [Required]
        public string PaymentType { get; set; }
        [Required]
        public DateTime DepartureDate { get; set; }
        [Required]
        public DateTime ArrivalDate { get; set; }
        [Required]
        public Trip Trip { get; set; }
    }
}
