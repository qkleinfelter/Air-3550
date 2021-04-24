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
using Microsoft.EntityFrameworkCore;

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
                outputInfo.Title = "Bad Login";
                outputInfo.Message = "The UserID or Password you entered is incorrect!";
                outputInfo.Severity = InfoBarSeverity.Error;
                outputInfo.IsOpen = true;
            } 
            else
            {
                using (var db = new AirContext())
                {
                    var user = db.Users.Include(user => user.CustInfo).Where(dbuser => dbuser.LoginId == userIdInput).FirstOrDefault();
                    UserSession.user = user;
                    UserSession.userLoggedIn = true;

                    if (user.UserRole == Role.MARKETING_MANAGER)
                    {
                        Frame.Navigate(typeof(MarketingManagerPage));
                    }
                    else if (user.UserRole == Role.LOAD_ENGINEER)
                    {
                        Frame.Navigate(typeof(LoadEngineerPage));
                    }
                    else
                    {
                        Frame.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
                    }
                }
                // Once they're logged in, send them back to the main page
                
                //Frame.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
            }
        }

        private void createAccountButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegisterPage));
        }
    }
}
