// CustomerInfo.cs - Air 3550 Project
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

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Air_3550.Models
{
    public class CustomerInfo
    {
        public int CustomerInfoId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Zip { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        [Required]
        public string CreditCardNumber { get; set; }
        public int PointsUsed { get; set; }
        public int PointsAvailable { get; set; }
        public int CreditBalance { get; set; }
        [Required]
        public List<Trip> Trips { get; set; } = new List<Trip>();
    }
}
