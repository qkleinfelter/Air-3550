using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Database.Utiltities;
using Microsoft.UI.Xaml.Media.Animation;
using Air_3550.Repo;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Pages
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            //get user password
            string userIdInput = userID.Text.ToString();
            string userPasswordInput = passwordBox.Password.ToString();
            string hashed = PasswordHandler.HashPassword(userPasswordInput);
            bool accountCorrect = PasswordHandler.CompareHashedToStored(userIdInput, hashed);
            if (!accountCorrect)
            {
                // For security reasons, we always display the same message
                // so that users cannot brute force to determine login ids
                // the extra space on the left is for formatting reasons
                outputBlock.Text = " Username or Password is incorrect!";
            } else
            {
                outputBlock.Text = "";
                using (var db = new AirContext())
                {
                    var user = db.Users.Where(dbuser => dbuser.LoginId == userIdInput).FirstOrDefault();
                    UserSession.user = user;
                    UserSession.userLoggedIn = true;
                }
                // Once they're logged in, send them back to the main page
                Frame.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
            }
        }

        private void createAccountButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegisterPage));
        }
    }
}
