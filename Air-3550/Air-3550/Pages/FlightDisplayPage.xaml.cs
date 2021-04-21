using Air_3550.Models;
using Air_3550.Repo;
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

namespace Air_3550.Pages
{
    public sealed partial class FlightDisplayPage : Page
    {
        private string origin;
        private string dest;
        private DateTime departDate;
        private DateTime returnDate;
        public FlightDisplayPage()
        {
            this.InitializeComponent();
        }

        override protected void OnNavigatedTo(NavigationEventArgs e)
        {
            // change this parameter to an object instead of a string i need to parse later
            var split = e.Parameter.ToString().Split(",");
            origin = split[0];
            dest = split[1];
            departDate = DateTime.Parse(split[2]);
            returnDate = DateTime.Parse(split[3]);
            DepartHeader.Text += " - " + departDate.Date.ToShortDateString();
            ReturnHeader.Text += " - " + returnDate.Date.ToShortDateString();

            // This flight list is only for the first leg of a one way, we'll need to add another list view for
            // the return trip at some point as well
            DepartList.ItemsSource = GenerateRoutes(origin, dest, departDate);
            // when we have our second list for return, we will be able to run GenerateRoutes(dest, origin, returnDate);
            ReturnList.ItemsSource = GenerateRoutes(dest, origin, returnDate);

        }

        private List<Flight> GenerateRoutes(string originAirportCode, string destinationAirportCode, DateTime date)
        {
            // Right now this just generates all the one shot flights and doesn't take into account
            // scheduled flights, this is where we need to handle that generation
            // the date passed in here is also not yet used, because that will only be used with
            // the scheduled flights, and we'll need to do a little bit of joining
            var db = new AirContext();
            var validFlights = db.Flights.Include(flight => flight.Origin)
                                         .Include(flight => flight.Destination)
                                         .Where(flight => (flight.Origin.AirportCode == originAirportCode
                                         && (flight.Destination.AirportCode == destinationAirportCode)))
                                         .ToList();
            return validFlights;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void AddFlight_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
