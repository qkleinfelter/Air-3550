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
        public int Price { get; set; }
        [Required]
        public PaymentType PaymentType { get; set; }
        [Required]
        public Trip Trip { get; set; }

        public void CalculatePrice()
        {
            var originAirport = Flight.Flight.Origin;
            var destinationAirport = Flight.Flight.Destination;
            double distance = originAirport.DistanceToOtherAirport(destinationAirport); // distance between our airports in meters
            distance = distance / 1609; // convert meters to miles
            double milePrice = distance * 12; // 12 cents per mile
            Price = (int)milePrice; // cast to an int and store it in our price field to make sure we can use it later
        }
    }
}
