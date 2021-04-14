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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Pages
{
    public sealed partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            this.InitializeComponent();
        }

        private void backButton_Click(Object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
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

        int MakeUserID()
        {
            // Make 6-digit random int without leading 0s
            // Instantiate random number generator using system-supplied value as seed.
            var rand = new Random();

            // Random 6-digit number
            int userID = rand.Next(99999, 1000000);

            return userID;

            //still need to validate userID is unique to database with some type of loop
        }

        private void createAccountButton_Click(Object sender, RoutedEventArgs e)
        {
            // Input validation.
            if (ValidateInput())
            {
                outputBlock.Text = "";

                // Get Random UserID

                //printing for now to test, bout to head to work...
                int userID = MakeUserID();
                outputBlock.Text = userID.ToString();

            }
        }
    }
}
