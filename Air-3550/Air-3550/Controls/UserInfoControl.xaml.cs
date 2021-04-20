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
                    if (UserSession.userLoggedIn)
                    {
                        CustomerInfo customerInfo = UserSession.user.CustInfo;
                        NameInput.Text = customerInfo.Name;
                        AddressInput.Text = customerInfo.Address;
                        CityInput.Text = customerInfo.City;
                        StateInput.Text = customerInfo.State;
                        ZipInput.Text = customerInfo.Zip;
                        PhoneInput.Text = customerInfo.PhoneNumber;
                        AgeInput.Text = customerInfo.Age.ToString();
                        CreditCardInput.Text = customerInfo.CreditCardNumber;
                    }
                }
            };
        }

        private void confirmButton_Click(Object sender, RoutedEventArgs e)
        {
            if (IsRegister)
            {
                HandleNewAccount();
            }
            else
            {
                HandleUpdateAccount();
            }
        }

        private void backButton_Click(Object sender, RoutedEventArgs e)
        {
            OnNavigateParentReady(this, null);
        }

        private bool ValidateInput()
        {
            bool valid = true;
            outputInfo.Title = "Errors creating account!";
            outputInfo.Message = "Please fix the following errors and try again: ";

            if (string.IsNullOrWhiteSpace(NameInput.Text))
            {
                outputInfo.Message += "\nPlease provide your full name.";
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(AddressInput.Text))
            {
                outputInfo.Message += "\nPlease provide your address.";
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(CityInput.Text))
            {
                outputInfo.Message += "\nPlease provide your city.";
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(StateInput.Text))
            {
                outputInfo.Message += "\nPlease provide your state.";
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(ZipInput.Text))
            {
                outputInfo.Message += "\nPlease provide your zip code.";
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(PhoneInput.Text))
            {
                outputInfo.Message += "\nPlease provide your phone number.";
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(AgeInput.Text))
            {
                outputInfo.Message += "\nPlease provide your age.";
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(CreditCardInput.Text))
            {
                outputInfo.Message += "\nPlease provide your credit card.";
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(PasswordInput.Password))
            {
                outputInfo.Message += "\nPlease provide a password.";
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(ConfirmPasswordInput.Password))
            {
                outputInfo.Message += "\nPlease confirm your password.";
                valid = false;
            }

            if (!(PasswordInput.Password.Equals(ConfirmPasswordInput.Password)))
            {
                outputInfo.Message += "\nPassords do not match.";
                valid = false;
            }

            if (!valid)
            {
                outputInfo.Severity = InfoBarSeverity.Error;
                outputInfo.IsOpen = true;
            }

            return valid;
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

        private void HandleNewAccount()
        {
            // Input validation.
            if (ValidateInput())
            {
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

        private void HandleUpdateAccount()
        {
            outputInfo.Title = "Account Information Updated!";
            outputInfo.Message = "Your Account Information was updated successfully!";
            outputInfo.Severity = InfoBarSeverity.Success;
            outputInfo.IsOpen = true;
        }
    }
}
