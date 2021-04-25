using Air_3550.Models;
using Air_3550.Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Utiltities
{
    public class UserUtilities
    {
        public static void AwardCredit(User user, int amount)
        {
            using var db = new AirContext();
            var customerInfo = db.Users.Include(user => user.CustInfo).Where(dbuser => dbuser.UserId == user.UserId).FirstOrDefault().CustInfo;
            if (customerInfo != null)
                customerInfo.CreditBalance += amount;
            db.SaveChanges();
        }

        public static void AwardPoints(User user, int amount)
        {
            using var db = new AirContext();
            var customerInfo = db.Users.Include(user => user.CustInfo).Where(dbuser => dbuser.UserId == user.UserId).FirstOrDefault().CustInfo;
            if (customerInfo != null)
                customerInfo.PointsAvailable += amount;
            db.SaveChanges();
        }

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

        public static void AddUserToDB(User user)
        {
            using var db = new AirContext();
            db.Users.Add(user);
            db.SaveChanges();
        }
    }
}
