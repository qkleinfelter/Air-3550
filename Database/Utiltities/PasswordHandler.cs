// PasswordHandler.cs - Air 3550 Project
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

using Air_3550.Repo;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Database.Utiltities
{
    public class PasswordHandler
    {
        public static string HashPassword(string password)
        {
            // Using built-in Cryptography class to produce SHA512 hash of passwords.
            using SHA512 sha512Hash = SHA512.Create();
            // Have to go from String to byte array
            byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha512Hash.ComputeHash(inputBytes);
            string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

            return hash.ToLower();
        }

        public static bool CompareHashedToStored(string loginID, string hashedPassword)
        {
            using (var db = new AirContext())
            {
                // grab the user with this loginid
                var user = db.Users.Where(user => user.LoginId == loginID).FirstOrDefault();
                if (user == null)
                {
                    // if there isn't a user return false
                    // because we don't have anything to compare
                    return false;
                }

                // return whether or not the hashed password matches the password in the db
                return user.HashedPass == hashedPassword;
            }
        }
    }
}
