using Air_3550.Repo;
using Database.Utiltities;
using Database.Models;
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
    public sealed partial class AccountingManagerPage : Page
    {
        public AccountingManagerPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            List<ScheduledFlight> flights = GenerateFlights();
            FlightMoney.ItemsSource = flights;
            TotalFlights.Text += flights.Count;
            Earnings.Text += GetTotalMoney();
        }

        private static List<ScheduledFlight> GenerateFlights()
        {
            using var db = new AirContext();
            List<ScheduledFlight> SchedFLights = db.ScheduledFlights.Include(flight => flight.Flight)
                                                    .ThenInclude(fl => fl.Origin)
                                                  .Include(flight => flight.Flight)
                                                    .ThenInclude(fl => fl.Destination)
                                                  .Include(flight => flight.Flight)
                                                    .ThenInclude(fl => fl.PlaneType)
                                                  .Where(flight => flight.DepartureTime.CompareTo(DateTime.Now) < 0)
                                                  .ToList();
            return SchedFLights;
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

        private static string GetTotalMoney()
        {
            int Total = 0;
            using (var db = new AirContext())
            {
                var Trips = db.Trips.Where(trip => !trip.IsCanceled && (trip.Tickets.First().Flight.DepartureTime.CompareTo(DateTime.Now) < 0))
                                    .ToList();
                //var Trips = db.Trips.ToList();
                foreach (var trip in Trips)
                {
                    Total += trip.TotalCost;
                }
            }

            return (Total / 100).ToString();
        }
    }
}
