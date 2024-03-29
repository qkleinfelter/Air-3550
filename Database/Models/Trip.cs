﻿// Trip.cs - Air 3550 Project
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
        public int OriginAirportId { get; set; }
        [Required]
        public Airport Origin { get; set; }
        public int DestinationAirportId { get; set; }
        [Required]
        public Airport Destination { get; set; }
        public bool IsCanceled { get; set; }
        public int TotalCost { get; set; }
        public bool PointsClaimed { get; set; }
        public int CustomerInfoId { get; set; }

        public string GetFormattedCancelation()
        {
            if (IsCanceled)
            {
                return "Trip Canceled!";
            } 
            else
            {
                return "";
            }
        }

        public string GetFormattedDeparted()
        {
            if (IsCanceled)
            {
                // if the trip is canceled we don't want to display both messages
                return "";
            }
            DateTime now = DateTime.Now;
            // check if any flight in the trip has taken off
            foreach (Ticket ticket in Tickets)
            {
                var sf = ticket.Flight;
                DateTime departureTime = sf.DepartureTime;
                if (departureTime < now)
                {
                    // This flight has already taken off
                    return "Trip has departed!";
                }
            }
            return "";
        }

        public string GetFormattedDateString()
        {
            Ticket first = Tickets[0];
            Ticket last = Tickets[^1];

            if (first.Flight.DepartureTime.ToShortDateString() != last.Flight.DepartureTime.ToShortDateString())
            {
                // only show 2 dates if they're different
                return first.Flight.DepartureTime.ToShortDateString() + " to " + last.Flight.DepartureTime.ToShortDateString();
            }
            else
            {
                return first.Flight.DepartureTime.ToShortDateString();
            }
        }
    }
}
