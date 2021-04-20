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
            // note: we'll have to adjust this because multi-leg flights will have full one way pricing for each leg
            int basePrice = 5000; // $50 fixed costs for flights, in cents, because floating point math is usually unsafe for pricing
            var originAirport = Flight.Flight.Origin;
            var destinationAirport = Flight.Flight.Destination;
            double distance = originAirport.DistanceToOtherAirport(destinationAirport); // distance between our airports in meters
            distance = distance / 1609; // convert meters to miles
            double milePrice = distance * 12; // 12 cents per mile
            int price = basePrice + (int)milePrice + 800;

            DateTime depart = Flight.DepartureTime;
            DateTime arrival = Flight.GetArrivalTime();
            if (depart.Hour <= 5 || arrival.Hour <= 5)
            {
                // Leaving or arriving between midnight and 5am
                // therefore 20% red-eye discount
                Price = (int) (price - (price * .20));
                return; // return here so we don't accidentally 10% discount
            }

            if (depart.Hour <= 8 || arrival.Hour >= 19)
            {
                // Leaving before 8am, or arriving after 7pm
                // therefore 10% off-peak discount
                Price = (int)(price - (price * .10));
                return;
            }
        }
    }
}
