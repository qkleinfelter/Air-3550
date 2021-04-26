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
