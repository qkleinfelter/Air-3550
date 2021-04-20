using Air_3550.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class ScheduledFlight
    {
        public int ScheduledFlightId { get; set; }
        [Required]
        public Flight Flight { get; set; }
        [Required]
        public DateTime DepartureTime { get; set; }

        public DateTime GetArrivalTime()
        {
            DateTime arrivalTime = DepartureTime;
            TimeSpan flightDuration = Flight.GetDuration();
            arrivalTime = arrivalTime.Add(flightDuration);
            return arrivalTime;
        }
    }
}
