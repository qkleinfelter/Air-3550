// UserUtilities.cs - Air 3550 Project
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
using Air_3550.Repo;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Database.Utiltities
{
    public class UserUtilities
    {
        // adds the specified amount of credit to the specified user
        public static void AwardCredit(User user, int amount)
        {
            using var db = new AirContext();
            var customerInfo = db.Users.Include(user => user.CustInfo).Where(dbuser => dbuser.UserId == user.UserId).FirstOrDefault().CustInfo;
            if (customerInfo != null)
                customerInfo.CreditBalance += amount;
            db.SaveChanges();
        }

        // adds the specified amount of points to the specified user
        public static void AwardPoints(User user, int amount)
        {
            using var db = new AirContext();
            var customerInfo = db.Users.Include(user => user.CustInfo).Where(dbuser => dbuser.UserId == user.UserId).FirstOrDefault().CustInfo;
            if (customerInfo != null)
                customerInfo.PointsAvailable += amount;
            db.SaveChanges();
        }

        // subtracts the specified amount of points from the specified user
        public static void UsePoints(User user, int amount)
        {
            using var db = new AirContext();
            var customerInfo = db.Users.Include(user => user.CustInfo).Where(dbuser => dbuser.UserId == user.UserId).FirstOrDefault().CustInfo;
            if (customerInfo != null)
            {
                customerInfo.PointsUsed += amount;
                customerInfo.PointsAvailable -= amount;
            }
            db.SaveChanges();
        }

        // subtracts the specified amount of credit from the specified user
        public static void UseCredit(User user, int amount)
        {
            using var db = new AirContext();
            var customerInfo = db.Users.Include(user => user.CustInfo).Where(dbuser => dbuser.UserId == user.UserId).FirstOrDefault().CustInfo;
            if (customerInfo != null)
            {
                customerInfo.CreditBalance -= amount;
            }
            db.SaveChanges();
        }

        // checks if a specified login id exists in the db
        public static bool LoginIDExists(string loginId)
        {
            using (var db = new AirContext())
            {
                var user = db.Users.SingleOrDefault(dbuser => dbuser.LoginId == loginId);
                if (user != null)
                {
                    return true;
                }
            }
            return false;
        }

        // adds a user to the db
        public static void AddUserToDB(User user)
        {
            using var db = new AirContext();
            db.Users.Add(user);
            db.SaveChanges();
        }
    }
}
