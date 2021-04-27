// UserSession.cs - Air 3550 Project
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

namespace Database.Utiltities
{
    public class UserSession
    {
        // public class to store our Users session
        // whether or not there is a user logged in
        public static bool userLoggedIn = false;
        // the userid of the logged in user, or 0 if there isn't
        public static int userId = 0;
    }
}
