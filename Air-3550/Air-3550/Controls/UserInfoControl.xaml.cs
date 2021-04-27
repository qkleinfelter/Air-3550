// UserInfoControl.xaml.cs - Air 3550 Project
//
// This program acts as a flight reservation system for a new airline - called Air-3550,
// which allows customers to book and manage trips, which contain many flights, as well as enabling various staff members
// to update the available flights and view statistics about them individually or as a whole.
//
// Authors:     Quinn Kleinfelter, James Golden, & Edward Walsh
// Class:       EECS 3550-001 Software Engineering, Spring 2021
// Instructor:  Dr. Thomas
// Date:        April 28, 2021
// Copyright:   Copyright 2021 by Quinn Kleinfelter, James Golden, & Edward Walsh. All rights reserved.

using Air_3550.Models;
using Air_3550.Repo;
using Database.Utiltities;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;

namespace Air_3550.Controls
{
    /**
     * This UserControl that we have called UserInfoControl is used a couple times 
     * in our app, when creating accounts or editing account information
     * instead of copy and pasting the same giant form and validation
     */
    public sealed partial class UserInfoControl : UserControl
    {
        public delegate void MyEventHandler(object source, EventArgs e);
        public static event MyEventHandler OnNavigateParentReady;

        // determines if the userinfocontrol is a register box
        public bool IsRegister { get; set; }

        public UserInfoControl()
        {
            this.InitializeComponent();

            // After the component is loaded, we need to do a few things
            this.Loaded += (sender, e) =>
            {
                // Change some text depending on whether or not its a register box
                if (IsRegister)
                {
                    TitleText.Text = "Register";
                    confirmButton.Content = "Create Account";
                }
                else
                {
                    TitleText.Text = "Update Account Info";
                    confirmButton.Content = "Update Information";
                    // If theres a user logged in, we need to adjust some things
                    if (UserSession.userLoggedIn)
                    {
                        var db = new AirContext();
                        var user = db.Users.Include(dbuser => dbuser.CustInfo).Single(dbuser => dbuser.UserId == UserSession.userId);
                        // grab the user thats logged in from the db ^
                        if (user.UserRole != Role.CUSTOMER)
                        {
                            // only show password fields if they aren't a customer
                            NameInput.Visibility = Visibility.Collapsed;
                            AddressInput.Visibility = Visibility.Collapsed;
                            CityInput.Visibility = Visibility.Collapsed;
                            StateInput.Visibility = Visibility.Collapsed;
                            ZipInput.Visibility = Visibility.Collapsed;
                            PhoneInput.Visibility = Visibility.Collapsed;
                            AgeInput.Visibility = Visibility.Collapsed;
                            CreditCardInput.Visibility = Visibility.Collapsed;
                            return;
                        }

                        // if they are a customer & logged in, fill out their info
                        // so they can update it easily
                        CustomerInfo customerInfo = user.CustInfo;
                        NameInput.Text = customerInfo.Name;
                        AddressInput.Text = customerInfo.Address;
                        CityInput.Text = customerInfo.City;
                        StateInput.Text = customerInfo.State;
                        ZipInput.Text = customerInfo.Zip;
                        PhoneInput.Text = customerInfo.PhoneNumber;
                        AgeInput.Text = customerInfo.Age.ToString();
                        CreditCardInput.Text = customerInfo.CreditCardNumber;
                        PasswordInput.PlaceholderText = "Update Password";
                        ConfirmPasswordInput.PlaceholderText = "Confirm Updated Password";
                    }
                    
                }
            };
        }

        private void ConfirmButton_Click(Object sender, RoutedEventArgs e)
        {
            // Depending on whether or not its a register box we need to handle
            // the confirm button click differently
            if (IsRegister)
            {
                HandleNewAccount();
            }
            else
            {
                HandleUpdateAccount();
            }
        }

        private void BackButton_Click(Object sender, RoutedEventArgs e)
        {
            OnNavigateParentReady(this, null);
        }

        // private helper function to validate the input
        private bool ValidateInput()
        {
            bool valid = true;
            outputInfo.Title = "Errors creating account!";
            outputInfo.Message = "Please fix the following errors and try again: ";
            var db = new AirContext();
            // grab the user if they exist
            var user = db.Users.Include(dbuser => dbuser.CustInfo).SingleOrDefault(dbuser => dbuser.UserId == UserSession.userId);
            if (!(UserSession.userLoggedIn && user.UserRole != Role.CUSTOMER))
            {
                // Don't validate these other fields if they aren't a customer
                // because we don't show them to them
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

                if (string.IsNullOrWhiteSpace(StateInput.Text) || StateInput.Text.Length != 2)
                {
                    outputInfo.Message += "\nPlease provide your 2-letter state code.";
                    valid = false;
                }

                if (string.IsNullOrWhiteSpace(ZipInput.Text) || ZipInput.Text.Length < 5)
                {
                    outputInfo.Message += "\nPlease provide your zip code.";
                    valid = false;
                }

                if (string.IsNullOrWhiteSpace(PhoneInput.Text) || PhoneInput.Text.Length < 10)
                {
                    outputInfo.Message += "\nPlease provide your phone number.";
                    valid = false;
                }

                if (string.IsNullOrWhiteSpace(AgeInput.Text) || AgeInput.Value < 0 || AgeInput.Value > 125)
                {
                    outputInfo.Message += "\nPlease provide your age.";
                    valid = false;
                }

                if (string.IsNullOrWhiteSpace(CreditCardInput.Text) || CreditCardInput.Text.Length < 15)
                {
                    outputInfo.Message += "\nPlease provide your credit card.";
                    valid = false;
                }
            }
            
            if (IsRegister || !string.IsNullOrWhiteSpace(PasswordInput.Password) || !string.IsNullOrWhiteSpace(ConfirmPasswordInput.Password))
            {
                // If they're just updating their account info they don't need to change their password
                // so we only require it in here
                // however, if they have a value in either spot, then we want them to validate both again
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
            }
            
            // if any of the checks above failed, change the outputinfo's severity
            // and display it
            if (!valid)
            {
                outputInfo.Severity = InfoBarSeverity.Error;
                outputInfo.IsOpen = true;
            }

            // then return whether or not its valid
            return valid;
        }

        private static int MakeUserID()
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

                // fill out a new customer info object with their info from
                // the fields
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

                // fill out a new user object with their info
                User user = new()
                {
                    LoginId = userID.ToString(),
                    HashedPass = PasswordHandler.HashPassword(PasswordInput.Password),
                    UserRole = Role.CUSTOMER,
                    CustInfo = customerInfo
                };

                // add the user to the database
                UserUtilities.AddUserToDB(user);

                // and display information to the user about their account being created
                outputInfo.Title = "Account Creation Successful!";
                outputInfo.Message = $"Your Login ID is: {userID}, please remember it for future logins!";
                outputInfo.Severity = InfoBarSeverity.Success;
                outputInfo.IsOpen = true;
            }
        }

        private void HandleUpdateAccount()
        {
            // validate input
            if (ValidateInput())
            {
                User currentUser = null;
                CustomerInfo custInfo = null;
                if (UserSession.userLoggedIn)
                {
                    var db = new AirContext();
                    var user = db.Users.Include(dbuser => dbuser.CustInfo).Single(dbuser => dbuser.UserId == UserSession.userId);
                    currentUser = user;
                    // if the current user is a customer, then update their information from the fields
                    if (user.UserRole == Role.CUSTOMER)
                    {
                        custInfo = currentUser.CustInfo;
                        custInfo.Name = NameInput.Text;
                        custInfo.Address = AddressInput.Text;
                        custInfo.City = CityInput.Text;
                        custInfo.State = StateInput.Text;
                        custInfo.Zip = ZipInput.Text;
                        custInfo.PhoneNumber = PhoneInput.Text;
                        custInfo.Age = (int)AgeInput.Value;
                        custInfo.CreditCardNumber = CreditCardInput.Text;
                    }
                }
                
                // if they are updating their password then we need to update their hashed password
                if (!string.IsNullOrWhiteSpace(PasswordInput.Password) && !string.IsNullOrWhiteSpace(ConfirmPasswordInput.Password))
                {
                    currentUser.HashedPass = PasswordHandler.HashPassword(PasswordInput.Password);
                }

                using (var db = new AirContext())
                {
                    // save the updated customer info in the database
                    var dbuser = db.Users.Single(user => user.LoginId == currentUser.LoginId);
                    if (custInfo != null)
                        dbuser.CustInfo = custInfo;
                    dbuser.HashedPass = currentUser.HashedPass;
                    db.SaveChanges();
                }

                // display to the user that we updated their info successfull
                outputInfo.Title = "Account Information Updated!";
                outputInfo.Message = "Your Account Information was updated successfully!";
                outputInfo.Severity = InfoBarSeverity.Success;
                outputInfo.IsOpen = true;
            }
            
        }
    }
}
