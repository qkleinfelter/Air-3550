using Air_3550.Models;
using Air_3550.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Utiltities
{
    class UserUtilities
    {
        public static void RegisterUser()
        {

        }

        public static void AwardCredit(User user, int amount)
        {
            using (var db = new AirContext())
            {
                var customerInfo = db.Users.Where(dbuser => dbuser.UserId == user.UserId).First().CustInfo;
                if (customerInfo != null)
                    customerInfo.CreditBalance += amount;
            }
        }
    }
}
