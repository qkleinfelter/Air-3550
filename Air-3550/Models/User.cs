using System;
using System.Collections.Generic;
using System.Text;

namespace Air_3550.Models
{
    public class User
    {
        public int CustomerNumber { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Roles> UserRoles { get; set; } = new List<Roles>();
        public string CreditCardNumber { get; set; }
        public int PointsUsed { get; set; }
        public int PointsAvailable { get; set; }
        public int CreditBalance { get; set; }
        public List<Ticket> TicketsBooked { get; set; } = new List<Ticket>();
        public List<Ticket> TicketsTaken { get; set; } = new List<Ticket>();
        public List<Ticket> TicketsCanceled { get; set; } = new List<Ticket>();
    }
}
