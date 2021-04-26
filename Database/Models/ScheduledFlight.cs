using Air_3550.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
    public class ScheduledFlight
    {
        public int ScheduledFlightId { get; set; }
        public int FlightId { get; set; }
        [Required]
        public Flight Flight { get; set; }
        [Required]
        public DateTime DepartureTime { get; set; }
        public int TicketsPurchased { get; set; }

        public string PercentCapacity()
        {
            int Perc = (TicketsPurchased / Flight.PlaneType.MaxSeats) * 100;
            string percent = Perc.ToString();
            return percent;
        }

        public string TotalMoney()
        {
            double money = (TicketsPurchased * Flight.GetCost()) / 100;
            return money.ToString();
        }

        public string AvgCost()
        {
            return (Flight.GetCost() / 100).ToString();
        }
    }
}
