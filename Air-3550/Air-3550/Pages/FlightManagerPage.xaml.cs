using Air_3550.Repo;
using Database.Models;
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
    public sealed partial class FlightManagerPage : Page
    {
        public FlightManagerPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Manifest.ItemsSource = GenerateFlights();
        }

        private List<ScheduledFlight> GenerateFlights()
        {
            using (var db = new AirContext())
            {
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
        }

        // Manage Account & logout at top right of page
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
