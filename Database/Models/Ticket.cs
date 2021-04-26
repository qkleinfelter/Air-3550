using Database.Models;
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
        [Required]
        public PaymentType PaymentType { get; set; }
        public bool isCanceled { get; set; }

        public string ArrivalTime()
        {
            TimeSpan time = Flight.Flight.GetArrivalTime();
            DateTime tempTime = DateTime.Today.Add(time);
            return tempTime.ToString("hh:mm tt");
        }
    }

    
}
