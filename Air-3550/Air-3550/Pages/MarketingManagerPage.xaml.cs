using Air_3550.Repo;
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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MarketingManagerPage : Page
    {
        public MarketingManagerPage()
        {
            this.InitializeComponent();

            departurePicker.MinDate = DateTime.Now;
        }


        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (ValidateSearchParameters())
            {
                using var db = new AirContext();
                // valid search params, so actually search
                string originCode = StripAirportCode(originPicker.Text);
                var originAirport = db.Airports.Single(airport => airport.AirportCode == originCode);
                

                var depDate = departurePicker.Date.Value.Date; // this gets only the date portion of the departure pickers chosen date
                MMSelectPlanePage.MMParameters passIn;
                
                passIn = new MMSelectPlanePage.MMParameters(originAirport, depDate);

                

                Frame.Navigate(typeof(MMSelectPlanePage), passIn);
            }
            else
            {
                OutputInfo.Title = "Invalid Input!";
                OutputInfo.Severity = InfoBarSeverity.Error;
                OutputInfo.IsOpen = true;
            }
            
        }

        private string StripAirportCode(string full)
        {
            return full.Substring(full.IndexOf("(") + 1, 3);
        }

        private bool ValidateSearchParameters()
        {
            // start with our return as true, it will get changed to false if either of our input parameters are bad
            bool valid = true;
            OutputInfo.Message = "Your search could not be processed due to invalid parameters: ";
            if (string.IsNullOrEmpty(originPicker.Text))
            {
                OutputInfo.Message += "\nYou must select an origin airport";
                valid = false;
            }
            if (departurePicker.Date == null)
            {
                OutputInfo.Message += "\nYou must select a departure date!";
                valid = false;
            }

            return valid;
        }

        private void changeAccountInfoButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ChangeAccountInfoPage));
        }

        private void logoutNavigator_Click(object sender, RoutedEventArgs e)
        {
            UserSession.userId = 0;
            UserSession.userLoggedIn = false;
            Frame.Navigate(typeof(MainPage));
        }
    }
}
