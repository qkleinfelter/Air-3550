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
