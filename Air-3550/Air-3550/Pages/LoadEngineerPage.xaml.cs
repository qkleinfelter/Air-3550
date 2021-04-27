using Air_3550.Models;
using Air_3550.Repo;
using Database.Utiltities;
using Microsoft.EntityFrameworkCore;
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
    public sealed partial class LoadEngineerPage : Page
    {
        public LoadEngineerPage()
        {
            this.InitializeComponent();
            departurePicker.MinDate = DateTime.Now;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateSearchParameters())
            {
                // Bring up list of flights for Origin Airport
                using var db = new AirContext();
                // valid search params, so actually search
                string originCode = StripAirportCode(originPicker.Text);
                var originAirport = db.Airports.Single(airport => airport.AirportCode == originCode);


                var depDate = departurePicker.Date.Value.Date; // this gets only the date portion of the departure pickers chosen date
                LEManageFlightsPage.LEParameters passIn;

                passIn = new LEManageFlightsPage.LEParameters(originAirport, depDate);



                Frame.Navigate(typeof(LEManageFlightsPage), passIn);

                // probably get rid of these messages
                OutputInfoTop.Message = "We can work with this!";

                OutputInfoTop.Title = "Success!";
                OutputInfoTop.Severity = InfoBarSeverity.Success;
                OutputInfoTop.IsOpen = true;
            }
            else
            {
                OutputInfoTop.Title = "Invalid Input!";
                OutputInfoTop.Severity = InfoBarSeverity.Error;
                OutputInfoTop.IsOpen = true;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateAddParameters())
            {
                // get data from GUI and add flight
                using var db = new AirContext();
                
                string originCode = StripAirportCode(originPickerAdd.Text);
                string destCode = StripAirportCode(destPickerAdd.Text);
                var originAirport = db.Airports.Single(Airport => Airport.AirportCode == originCode);
                var destAirport = db.Airports.Single(Airport => Airport.AirportCode == destCode);
                TimeSpan selectedTime = (TimeSpan)timePickerAdd.SelectedTime;

                // Make sure that this isn't a new route
                if (EnsureExistingRoute(originAirport, destAirport))
                {
                    // Randomly adding plane because it is necessary to add flights
                    // without causing trouble elsewhere...
                    var rand = new Random();

                    int planeID = rand.Next(1, 5);

                    // Add new flight
                    Flight flight = new()
                    {
                        Origin = originAirport,
                        Destination = destAirport,
                        PlaneType = db.Planes.Single(plane => plane.PlaneId == planeID),
                        DepartureTime = selectedTime
                    };
                    db.Flights.Add(flight);
                    db.SaveChanges();

                    OutputInfo.Message = $"Flight was successfully added!";

                    // may not need this stuff later
                    OutputInfo.Title = "Success!";
                    OutputInfo.Severity = InfoBarSeverity.Success;
                    OutputInfo.IsOpen = true;
                }
                else
                {
                    OutputInfo.Message = $"Talk to your manager if you really think we should add a brand new route.";

                    OutputInfo.Title = "Route Doesn't Exist!";
                    OutputInfo.Severity = InfoBarSeverity.Error;
                    OutputInfo.IsOpen = true;
                }




                
            }
            else
            {
                OutputInfo.Title = "Invalid Input!";
                OutputInfo.Severity = InfoBarSeverity.Error;
                OutputInfo.IsOpen = true;
            }

        }

        private bool ValidateSearchParameters()
        {
            // start with our return as true, it will get changed to false if either of our input parameters are bad
            bool valid = true;
            OutputInfoTop.Message = "Your search could not be processed due to invalid parameters: ";
            if (string.IsNullOrEmpty(originPicker.Text))
            {
                OutputInfoTop.Message += "\nYou must select an origin airport";
                valid = false;
            }
            if (!departurePicker.Date.HasValue)
            {
                OutputInfoTop.Message += "\nYou must select a valid date";
                valid = false;
            }
            return valid;
        }

        private static bool EnsureExistingRoute(Airport originAirport, Airport destinationAirport)
        {
            using var db = new AirContext();
            // This query grabs all direct flights, comments follow inline
            var direct = db.Flights // on the entire flights table of the db
                           .Include(flight => flight.Origin) // ensure that we have access to the origin airports info later
                           .Include(flight => flight.Destination) // ensure that we have access to the destination airports info later
                           .Where(flight => !flight.IsCanceled // only take flights that are not canceled (by staff member)
                           && flight.Origin == originAirport // the origin of the flight should match the origin airport passed in
                           && flight.Destination == destinationAirport) // and the destination airports should match
                           .ToList(); // then turn it into a list

            // If there are flights at this point, they are existing routes.
            // This makes them safe to add new flights.
            if (direct.Count > 0)
            {

                return true;
            }
            return false;
        }

        private bool ValidateAddParameters()
        {
            // start with our return as true, it will get changed to false if either of our input parameters are bad
            bool valid = true;
            OutputInfo.Message = "Your search could not be processed due to invalid parameters: ";
            if (string.IsNullOrEmpty(originPickerAdd.Text))
            {
                OutputInfo.Message += "\nYou must select an origin airport";
                valid = false;
            }
            if (string.IsNullOrEmpty(destPickerAdd.Text))
            {
                OutputInfo.Message += "\nYou must select a destination airport";
                valid = false;
            }
            // Don't want this displaying with both fields blank
            if (valid == true && originPickerAdd.Text == destPickerAdd.Text)
            {
                OutputInfo.Message += "\nOrigin and destination airports must be different";
                valid = false;
            }
            if (!timePickerAdd.SelectedTime.HasValue)
            {
                OutputInfo.Message += "\nYou must select a valid time";
                valid = false;
            }
            return valid;
        }

        private void ChangeAccountInfoButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ChangeAccountInfoPage));
        }

        private void LogoutNavigator_Click(object sender, RoutedEventArgs e)
        {
            UserSession.userId = 0;
            UserSession.userLoggedIn = false;
            Frame.Navigate(typeof(MainPage));
        }

        private static string StripAirportCode(string full)
        {
            return full.Substring(full.IndexOf("(") + 1, 3);
        }
    }
}
