using Air_3550.Models;
using Air_3550.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Utiltities
{
    public class UserUtilities
    {
        public static void RegisterUser()
        {

        }

        public static void AwardCredit(User user, int amount)
        {
            using (var db = new AirContext())
            {
                var customerInfo = db.Users.Where(dbuser => dbuser.UserId == user.UserId).FirstOrDefault().CustInfo;
                if (customerInfo != null)
                    customerInfo.CreditBalance += amount;
            }
        }

        public static bool LoginIDExists(int loginId)
        {
            using (var db = new AirContext())
            {
                // i know theres a better way to do this but c# was giving me weird expression tree errors
                // that i cant be asked to figure out right now, so i'm using this garbage method
                var users = db.Users;
                foreach (var dbuser in users)
                {
                    int id;
                    bool success = int.TryParse(dbuser.LoginId, out id);
                    if (success && id == loginId)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
