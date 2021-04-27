using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Linq;
using Database.Utiltities;
using Microsoft.UI.Xaml.Media.Animation;
using Air_3550.Repo;
using Microsoft.EntityFrameworkCore;

namespace Air_3550.Pages
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            //get user input
            string userIdInput = userID.Text.ToString();
            string userPasswordInput = passwordBox.Password.ToString();
            // hash the password
            string hashed = PasswordHandler.HashPassword(userPasswordInput);
            // and check if its correct
            bool accountCorrect = PasswordHandler.CompareHashedToStored(userIdInput, hashed);
            if (!accountCorrect)
            {
                // For security reasons, we always display the same message
                // so that users cannot brute force to determine login ids
                outputInfo.Title = "Bad Login";
                outputInfo.Message = "The UserID or Password you entered is incorrect!";
                outputInfo.Severity = InfoBarSeverity.Error;
                outputInfo.IsOpen = true;
            } 
            else
            {
                using (var db = new AirContext())
                {
                    // grab the user and update the session
                    var user = db.Users.Include(user => user.CustInfo)
                                        .Where(dbuser => dbuser.LoginId == userIdInput).FirstOrDefault();
                    UserSession.userId = user.UserId;
                    UserSession.userLoggedIn = true;

                    // then send them to the appropriate page
                    if (user.UserRole == Role.MARKETING_MANAGER)
                    {
                        Frame.Navigate(typeof(MarketingManagerPage));
                    }
                    else if (user.UserRole == Role.LOAD_ENGINEER)
                    {
                        Frame.Navigate(typeof(LoadEngineerPage));
                    }
                    else if (user.UserRole == Role.FLIGHT_MANAGER)
                    {
                        Frame.Navigate(typeof(FlightManagerPage));
                    }
                    else if (user.UserRole == Role.ACCOUNTING_MANAGER)
                    {
                        Frame.Navigate(typeof(AccountingManagerPage));
                    }
                    else
                    {
                        Frame.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
                    }
                }
            }
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegisterPage));
        }
    }
}
