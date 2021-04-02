using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Air_3550.Models
{
    public class User
    {
        public int UserId { get; set; }
        public int CustomerNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public Role UserRole { get; set; }
        [Required]
        public string CreditCardNumber { get; set; }
        public int PointsUsed { get; set; }
        public int PointsAvailable { get; set; }
        public int CreditBalance { get; set; }
        [Required]
        public List<Ticket> TicketsBooked { get; set; } = new List<Ticket>();
        [Required]
        public List<Ticket> TicketsTaken { get; set; } = new List<Ticket>();
        [Required]
        public List<Ticket> TicketsCanceled { get; set; } = new List<Ticket>();
    }
}
