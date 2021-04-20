using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Air_3550.Models
{
    public class Trip
    {
        public int TripId { get; set; }
        [Required]
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
        [Required]
        public Airport Origin { get; set; }
        [Required]
        public Airport Destination { get; set; }

        public int GetTotalPrice()
        {
            int basePrice = 5000; // base price of $50 in cents
            int tsafees = 8 * 100 * Tickets.Count; // $8 in cents per ticket, a.k.a. per takeoff
            double discount = 0;
            int piecePrices = 0;
            foreach (Ticket ticket in Tickets)
            {
                // Calculate the price for the smaller leg
                ticket.CalculatePrice();
                // add it to the piece prices
                piecePrices += ticket.Price;

                // we are a generous company, and will give you the maximum discount for your whole trip, as long as at least one leg qualifies
                DateTime depart = ticket.Flight.DepartureTime;
                DateTime arrival = ticket.Flight.GetArrivalTime();
                if (depart.Hour <= 5 || arrival.Hour <= 5)
                {
                    // Leaving or arriving between midnight and 5am
                    // therefore 20% red-eye discount
                    discount = .10;
                }

                if (depart.Hour <= 8 || arrival.Hour >= 19)
                {
                    // Leaving before 8am, or arriving after 7pm
                    // therefore 10% off-peak discount
                    discount = .20;
                    break; // break out of the loop if we hit a 20% discount because its our maximum
                }
            }
            // add up all the prices
            int totalPrice = basePrice + tsafees + piecePrices;
            // and remove the appropriate discount
            totalPrice = (int) (totalPrice - (totalPrice * discount));
            // then return our updated price!
            return totalPrice;
        }
    }
}
