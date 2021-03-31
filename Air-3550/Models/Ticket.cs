using System;
using System.Collections.Generic;
using System.Text;

namespace Air_3550.Models
{
    public class Ticket
    {
        public User Customer { get; set; }
        public Flight ConnectedFlight { get; set; }
        public Route ConnectedRoute { get; set; }
        public int Price { get; set; }
        public string PaymentType { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
    }
}
