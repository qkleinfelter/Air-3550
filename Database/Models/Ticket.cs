// Ticket.cs - Air 3550 Project
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

using Database.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Air_3550.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        [Required]
        public ScheduledFlight Flight { get; set; }
        [Required]
        public PaymentType PaymentType { get; set; }
        public bool IsCanceled { get; set; }

        public string ArrivalTime()
        {
            TimeSpan time = Flight.Flight.GetArrivalTime(); // get the arrival time of the flight
            DateTime tempTime = DateTime.Today.Add(time); // put it into a date time
            return tempTime.ToString("hh:mm tt"); // and return the templated string
        }
    }

    
}
