// ScheduledFlight.cs - Air 3550 Project
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
