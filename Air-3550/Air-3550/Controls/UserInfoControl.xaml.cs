using Air_3550.Models;
using Air_3550.Pages;
using Database.Utiltities;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace Air_3550.Controls
{
    public sealed partial class UserInfoControl : UserControl
    {
        public delegate void MyEventHandler(object source, EventArgs e);
        public static event MyEventHandler OnNavigateParentReady;

        public bool IsRegister { get; set; }

        public UserInfoControl()
        {
            this.InitializeComponent();

            this.Loaded += (sender, e) =>
            {
                if (IsRegister)
                {
                    TitleText.Text = "Register";
                    confirmButton.Content = "Create Account";
                }
                else
                {
                    TitleText.Text = "Update Account Info";
                    confirmButton.Content = "Update Information";
                }
            };
        }

        private void confirmButton_Click(Object sender, RoutedEventArgs e)
        {
            // Input validation.
            if (ValidateInput())
            {
                outputBlock.Text = "";

                // Get Random UserID
                int userID = MakeUserID();

                CustomerInfo customerInfo = new()
                {
                    Name = NameInput.Text,
                    Address = AddressInput.Text,
                    City = CityInput.Text,
                    State = StateInput.Text,
                    Zip = ZipInput.Text,
                    PhoneNumber = PhoneInput.Text,
                    Age = (int)AgeInput.Value,
                    CreditCardNumber = CreditCardInput.Text
                };

                User user = new()
                {
                    LoginId = userID.ToString(),
                    HashedPass = PasswordHandler.HashPassword(PasswordInput.Password),
                    UserRole = Role.CUSTOMER,
                    CustInfo = customerInfo
                };

                UserUtilities.AddUserToDB(user);

                outputInfo.Title = "Account Creation Successful!";
                outputInfo.Message = $"Your Login ID is: {userID}, please remember it for future logins!";
                outputInfo.Severity = InfoBarSeverity.Success;
                outputInfo.IsOpen = true;

            }
        }

        private void backButton_Click(Object sender, RoutedEventArgs e)
        {
            OnNavigateParentReady(this, null);
        }

        private bool ValidateInput()
        {
            outputBlock.Text = "";

            if (string.IsNullOrWhiteSpace(NameInput.Text))
            {
                outputBlock.Text = "Please provide your full name.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(AddressInput.Text))
            {
                outputBlock.Text = "Please provide your address.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(CityInput.Text))
            {
                outputBlock.Text = "Please provide your city.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(StateInput.Text))
            {
                outputBlock.Text = "Please provide your state.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(ZipInput.Text))
            {
                outputBlock.Text = "Please provide your zip code.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(PhoneInput.Text))
            {
                outputBlock.Text = "Please provide your phone number.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(AgeInput.Text))
            {
                outputBlock.Text = "Please provide your age.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(CreditCardInput.Text))
            {
                outputBlock.Text = "Please provide your credit card.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(PasswordInput.Password))
            {
                outputBlock.Text = "Please provide a password.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(ConfirmPasswordInput.Password))
            {
                outputBlock.Text = "Please confirm your password.";
                return false;
            }

            if (!(PasswordInput.Password.Equals(ConfirmPasswordInput.Password)))
            {
                outputBlock.Text = "Passords do not match.";
                return false;
            }

            return true;
        }

        private int MakeUserID()
        {
            // Make 6-digit random int without leading 0s
            // Instantiate random number generator using system-supplied value as seed.
            var rand = new Random();

            int userID;
            while (true)
            {
                // Random 6-digit number, .next is inclusive on minimum & exclusive on max
                userID = rand.Next(100000, 1000000);
                if (!UserUtilities.LoginIDExists(userID.ToString()))
                {
                    // If the login id doesn't exist, then break out of the while loop and return
                    break;
                }
            }

            return userID;
        }
    }
}
