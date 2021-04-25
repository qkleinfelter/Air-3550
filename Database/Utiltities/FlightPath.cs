using Air_3550.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Utiltities
{
    public class FlightPath
    {
        public List<Flight> flights;
        public int NumberOfStops => flights.Count - 1;
        public FlightPath(params Flight[] flights)
        {
            this.flights = new(flights);
        }

        public string DepartureTime
        {
            get
            {
                return DateTime.Today.Add(flights.First().DepartureTime).ToString("h:mm tt");
            }
        }

        public string ArrivalTime
        {
            get
            {
                return DateTime.Today.Add(flights.Last().GetArrivalTime()).ToString("h:mm tt");
            }
        }

        public string FormattedStops
        {
            get
            {
                return NumberOfStops switch
                {
                    0 => "Nonstop",
                    1 => NumberOfStops + " stop (" + flights[0].Destination.AirportCode + ")",
                    _ => NumberOfStops + " stops (" + flights[0].Destination.AirportCode + ", " + flights[1].Destination.AirportCode + ")"
                };
            }
        }

        public string FormattedDuration
        {
            get
            {
                var timeSpan = new TimeSpan();

                // For the first flight, we just add its duration directly.
                timeSpan += flights[0].GetDuration();

                TimeSpan fourtyMinutes = new(0, 40, 0);

                for (int i = 1; i < flights.Count; i++)
                {
                    var previousFlight = flights[i - 1];
                    var flight = flights[i];

                    if (flight.DepartureTime < previousFlight.GetArrivalTime().Add(fourtyMinutes))
                    {
                        // The flight departs before the previous flight arrives (plus the 40
                        // minute layovver), so we
                        // need to proceed to the next day to determine where the proper
                        // flight duration.
                        timeSpan += new TimeSpan(1, 0, 0, 0); // Add a day
                        timeSpan -= previousFlight.GetArrivalTime() - flight.DepartureTime; // Subtract the previous flight arrival time from the current flight's departure time
                    }
                    else
                    {
                        timeSpan += flight.DepartureTime - previousFlight.GetArrivalTime();
                    }

                    timeSpan += flight.GetDuration();
                }

                string result = "";

                if (timeSpan.Days > 0)
                {
                    result += timeSpan.Days + "d ";
                }

                if (timeSpan.Hours > 0)
                {
                    result += timeSpan.Hours + "h ";
                }

                if (timeSpan.Minutes > 0)
                {
                    result += timeSpan.Minutes + "m";
                }

                return result.Trim();
            }
        }

        public string Price
        {
            get
            {
                return "$" + CostHandler.PriceListOfFlights(flights) / 100;
            }
        }

        public string Plane
        {
            get
            {
                return flights[0].PlaneType.Model;
            }
            set
            {
                
            }
            
        }

        public string DestinationPort
        {
            get
            {
                return flights[0].Destination.AirportCode;
            }
        }

        public string FlightId
        {
            get
            {
                return flights[0].FlightId.ToString();
            }
        }
    }
}
