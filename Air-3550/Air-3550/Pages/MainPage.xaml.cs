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

namespace Air_3550.Pages
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            if (UserSession.userLoggedIn)
            {
                loginNavigator.Visibility = Visibility.Collapsed;
                accountNavigator.Visibility = Visibility.Visible;
            }
            // Can't select dates earlier than today
            departurePicker.MinDate = DateTime.Now;
            returnPicker.MinDate = DateTime.Now;
            // or dates farther than 6 months in the future
            departurePicker.MaxDate = DateTime.Now.AddMonths(6);
            returnPicker.MaxDate = DateTime.Now.AddMonths(6);
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateSearchParameters())
            {
                // valid search params, so actually search
            }
            else
            {
                OutputInfo.Title = "Invalid Input!";
                OutputInfo.Severity = InfoBarSeverity.Error;
                OutputInfo.IsOpen = true;
            }
        }

        private void loginNavigator_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }

        private void accountNavigator_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AccountPage));
        }

        private bool ValidateSearchParameters()
        {
            OutputInfo.Message = "Your search could not be processed due to invalid parameters: ";
            if (string.IsNullOrEmpty(originPicker.Text))
            {
                OutputInfo.Message += "\nYou must select an origin airport";
            }
            if (string.IsNullOrEmpty(destPicker.Text))
            {
                OutputInfo.Message += "\nYou must select a destination airport";
            }
            return false;
        }
    }
}
