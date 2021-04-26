using Database.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Air_3550.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        [Required]
        public ScheduledFlight Flight { get; set; }
        [Required]
        public PaymentType PaymentType { get; set; }
        public bool isCanceled { get; set; }

        public string ArrivalTime()
        {
            TimeSpan time = Flight.Flight.GetArrivalTime(); // get the arrival time of the flight
            DateTime tempTime = DateTime.Today.Add(time); // put it into a date time
            return tempTime.ToString("hh:mm tt"); // and return the templated string
        }
    }

    
}
