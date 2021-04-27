// CostHandler.cs - Air 3550 Project
//
// This program acts as a flight reservation system for a new airline - called Air-3550,
// which allows customers to book and manage trips, which contain many flights, as well as enabling various staff members
// to update the available flights and view statistics about them individually or as a whole.
//
// Authors:     Quinn Kleinfelter, James Golden, & Edward Walsh
// Class:       EECS 3550-001 Software Engineering, Spring 2021
// Instructor:  Dr. Thomas
// Date:        April 28, 2021
// Copyright:   Copyright 2021 by Quinn Kleinfelter, James Golden, & Edward Walsh. All rights reserved.

using Air_3550.Models;
using System;
using System.Collections.Generic;

namespace Database.Utiltities
{
    public class CostHandler
    {
        // Calculates a price of a list of flights
        public static int PriceListOfFlights(List<Flight> flights)
        {
            int basePrice = 5000; // base price of $50 in cents
            int tsafees = 8 * 100 * flights.Count; // $8 in cents per ticket, a.k.a. per takeoff
            double discount = 0;
            int piecePrices = 0;
            foreach (Flight flight in flights)
            {
                // Calculate the price for the smaller leg
                piecePrices += flight.GetCost();
                
                // we are a generous company, and will give you the maximum discount for your whole trip, as long as at least one leg qualifies
                TimeSpan depart = flight.DepartureTime;
                TimeSpan arrival = flight.GetArrivalTime();
                if (depart.Hours <= 5 || arrival.Hours <= 5)
                {
                    // Leaving or arriving between midnight and 5am
                    // therefore 20% red-eye discount
                    discount = .10;
                }

                if (depart.Hours <= 8 || arrival.Hours >= 19)
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
            totalPrice = (int)(totalPrice - (totalPrice * discount));
            // then return our updated price!
            return totalPrice;
        }
    }
}
