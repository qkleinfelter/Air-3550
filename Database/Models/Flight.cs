using System;
using System.ComponentModel.DataAnnotations;

namespace Air_3550.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        [Required]
        public Airport Origin { get; set; }
        [Required]
        public Airport Destination { get; set; }
        [Required]
        public Plane PlaneType { get; set; }
        [Required]
        public TimeSpan DepartureTime { get; set; }
        public int FlightNumber { get; set; }
        public bool IsCanceled { get; set; }

        public TimeSpan GetDuration()
        {
            // 30 minutes flat per flight, for takeoff and landing
            TimeSpan duration = new(0, 30, 0);
            if (Origin == null)
            {
                return new TimeSpan(0, 0, 0);
            }
            double distance = Origin.DistanceToOtherAirport(Destination); // Distance between the airports in meters
            distance /= 1609; // convert meters to miles
            double hours = distance / 500; // assuming 500mph in the air
            double seconds = hours * 3600; // convert to seconds
            duration = duration.Add(new TimeSpan(0, 0, (int)seconds)); // cast seconds to an int, we lose miniscule amounts of time so don't worry about it
            return duration;
        }

        public int GetCost()
        {
            if (Origin == null)
            {
                return 0;
            }
            double distance = Origin.DistanceToOtherAirport(Destination); // distance between our airports in meters
            distance /= 1609; // convert meters to miles
            double milePrice = distance * 12; // 12 cents per mile
            return (int)milePrice; // cast to an int and return it
        }

        public TimeSpan GetArrivalTime()
        {
            TimeSpan arrivalTime = DepartureTime; // start at the departure time
            TimeSpan flightDuration = GetDuration(); // get the duration of the flight
            arrivalTime = arrivalTime.Add(flightDuration); // and add it to the departure time to get the arrival
            return arrivalTime; // then return
        }
    }
}
