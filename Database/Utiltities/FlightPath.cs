using Air_3550.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Database.Utiltities
{
    public class FlightPath
    {
        // the flights in this flightpath
        public List<Flight> flights;
        // the number of stops, which is equivalent to the number of flights - 1
        public int NumberOfStops => flights.Count - 1;
        public FlightPath(params Flight[] flights)
        {
            this.flights = new(flights);
        }

        public string DepartureTime
        {
            get
            {
                // returns the nicely formatted departure time of the first flight
                // because the flight first in the path, is always the very first flight
                // we take
                return DateTime.Today.Add(flights.First().DepartureTime).ToString("h:mm tt");
            }
        }

        public string ArrivalTime
        {
            get
            {
                // returns the nicely formatted arrival time of the last flight
                // because the flight last in the path, is always the very last flight
                // we take
                return DateTime.Today.Add(flights.Last().GetArrivalTime()).ToString("h:mm tt");
            }
        }

        public string FormattedStops
        {
            get
            {
                // formats the number of stops for nice outputting
                return NumberOfStops switch
                {
                    // 0 stops, means non stop
                    0 => "Nonstop",
                    // 1 stop, and include the airport code in parentheses afterwards
                    1 => NumberOfStops + " stop (" + flights[0].Destination.AirportCode + ")",
                    // 2 stops, and include the airport codes in parentheses afterwards
                    _ => NumberOfStops + " stops (" + flights[0].Destination.AirportCode + ", " + flights[1].Destination.AirportCode + ")"
                };
            }
        }

        public string FormattedDuration
        {
            // formats the duration of the flightpath nicely
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
                        // The flight departs before the previous flight arrives
                        // (with a 40 minute minimum layovver), so we
                        // need to proceed to the next day to determine where the proper
                        // flight duration.
                        timeSpan += new TimeSpan(1, 0, 0, 0); // Add a day
                        timeSpan -= previousFlight.GetArrivalTime() - flight.DepartureTime; // Subtract the previous flight arrival time from the current flight's departure time
                    }
                    else
                    {
                        // otherwise we can just add on this flights departure - the previous flights arrival
                        timeSpan += flight.DepartureTime - previousFlight.GetArrivalTime();
                    }

                    // and add on the current flights duration
                    timeSpan += flight.GetDuration();
                }

                // formats the timespan into a nice output string
                string result = "";

                if (timeSpan.Days > 0)
                {
                    // only include days if we have some
                    result += timeSpan.Days + "d ";
                }

                if (timeSpan.Hours > 0)
                {
                    // only include hours if we have some
                    result += timeSpan.Hours + "h ";
                }

                if (timeSpan.Minutes > 0)
                {
                    // only include minutes if we have some
                    result += timeSpan.Minutes + "m";
                }

                // trim the string and return it
                return result.Trim();
            }
        }

        public string Price
        {
            // returns the nicely formatted price
            get
            {
                // calculates the price of the list of flights, divide by 100 to get the price in dollars
                // and throw a $ symbol in front
                return "$" + CostHandler.PriceListOfFlights(flights) / 100;
            }
        }

        public int IntPrice
        {
            // returns the price as an int, for other use
            get
            {
                // calculates the price of the list of flights, don't worry about putting it in dollars for this one
                return CostHandler.PriceListOfFlights(flights);
            }
        }

        public string Plane
        {
            // returns the plane model of the first flight
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
            // returns the destination of the first flight's airport code
            get
            {
                return flights[0].Destination.AirportCode;
            }
        }

        public string FlightId
        {
            // returns the flight id of the first string
            get
            {
                return flights[0].FlightId.ToString();
            }
        }
    }
}
