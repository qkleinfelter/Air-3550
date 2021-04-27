using Air_3550.Repo;
using Database.Models;
using Database.Utiltities;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Air_3550.Pages
{
    public sealed partial class FlightManagerPage : Page
    {
        public FlightManagerPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var departedFlights = GenerateFlights();

            if (departedFlights.Count == 0)
            {
                ListHeader.Visibility = Visibility.Collapsed;
                AvailableFlights.Visibility = Visibility.Collapsed;
                OutputInfo.Title = "Sorry no flights to show!";
                OutputInfo.Message = "No flights have departed yet";
                OutputInfo.Severity = InfoBarSeverity.Error;
                OutputInfo.IsOpen = true;
            }
            AvailableFlights.ItemsSource = departedFlights;
        }

        private static List<ScheduledFlight> GenerateFlights()
        {
            using var db = new AirContext();
            List<ScheduledFlight> ScheduledFlights = db.ScheduledFlights.Include(flight => flight.Flight)
                                                                            .ThenInclude(fl => fl.Origin)
                                                                        .Include(flight => flight.Flight)
                                                                            .ThenInclude(fl => fl.Destination)
                                                                        .Include(flight => flight.Flight)
                                                                            .ThenInclude(fl => fl.PlaneType)
                                                                        .ToList();
            List<ScheduledFlight> toRemove = new();
            foreach (ScheduledFlight sf in ScheduledFlights)
            {
                if (sf.DepartureTime > DateTime.Now)
                {
                    toRemove.Add(sf);
                }
            }

            foreach (ScheduledFlight sf in toRemove)
            {
                ScheduledFlights.Remove(sf);
            }

            return ScheduledFlights;
        }

        // Manage Account & logout at top right of page
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

        private void ShowManifest_Click(object sender, RoutedEventArgs e)
        {
            if (AvailableFlights.SelectedItem == null)
            {
                return;
            }
            else
            {
                ScheduledFlight sf = AvailableFlights.SelectedItem as ScheduledFlight;
                Frame.Navigate(typeof(FlightManifestPage), sf);
            }
        }
    }
}
