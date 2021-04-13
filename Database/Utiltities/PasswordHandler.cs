using Air_3550.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Database.Utiltities
{
    public class PasswordHandler
    {
        public static string HashPassword(string password)
        {
            // Using built-in Cryptography class to produce SHA512 hash of passwords.
            using (SHA512 sha512Hash = SHA512.Create())
            {
                // Have to go from String to byte array
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha512Hash.ComputeHash(inputBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

                return hash.ToLower();
            }
        }

        public static bool CompareHashedToStored(string loginID, string hashedPassword)
        {
            using (var db = new AirContext())
            {
                var user = db.Users.Where(user => user.LoginId == loginID).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }

                if (user.HashedPass == hashedPassword)
                {
                    return true;
                } else
                {
                    return false;
                }
            }
        }
    }
}
