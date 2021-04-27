// Airport.cs - Air 3550 Project
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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Air_3550.Models
{
    public class Airport
    {
        public int AirportId { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Column(TypeName = "Decimal(8,6)")]
        public decimal Latitude { get; set; }
        [Column(TypeName = "Decimal(9,6)")]
        public decimal Longitude { get; set; }
        public int Elevation { get; set; }
        [Required]
        public string AirportCode { get; set; }

        /**
         * Calculates the distance between 2 airports using the Haversine formula
         * based on this stackoverflow: https://stackoverflow.com/questions/3694380/calculating-distance-between-two-points-using-latitude-longitude/16794680#16794680
         * returns the distance in meters for now
         */
        public double DistanceToOtherAirport(Airport other)
        {
            if (other == null)
            {
                return 0d;
            }
            static double toRadians(double degrees)
            {
                return (Math.PI / 180) * degrees;
            }

            int R = 6371; // Radius of the earth
            double elevation1 = this.Elevation / 3.281; // converts our elevation in feet to meters
            double elevation2 = other.Elevation / 3.281; // converts our other airport's elevation in feet to meters
            // variables for the various lat & long to keep track of which is which
            double lat1 = (double) this.Latitude;
            double lat2 = (double) other.Latitude;
            double lon1 = (double) this.Longitude;
            double lon2 = (double) other.Longitude;

            double latDistance = toRadians(lat2 - lat1);
            double lonDistance = toRadians(lon2 - lon1);

            double a = Math.Sin(latDistance / 2) * Math.Sin(latDistance / 2)
                + Math.Cos(toRadians(lat1)) * Math.Cos(toRadians(lat2))
                * Math.Sin(lonDistance / 2) * Math.Sin(lonDistance / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = R * c * 1000;
            double height = elevation1 - elevation2;
            distance = Math.Pow(distance, 2) + Math.Pow(height, 2);

            return Math.Sqrt(distance);
        }
    }
}
