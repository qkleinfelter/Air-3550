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
    public sealed partial class LEManageFlightsPage : Page
    {
        public class LEParameters
        {
            public Airport origin;
            public DateTime departDate;

            public LEParameters(Airport departPort, DateTime depart)
            {
                origin = departPort;
                departDate = depart;
            }
        }
        private LEParameters passedLEParams;

        public LEManageFlightsPage()
        {
            this.InitializeComponent();
        }

        override protected void OnNavigatedTo(NavigationEventArgs e)
        {
            passedLEParams = e.Parameter as LEParameters;

            DepartHeader.Text = $"{passedLEParams.origin.City} : {passedLEParams.departDate.ToShortDateString()}";

            DepartList.ItemsSource = generateFlights(passedLEParams.origin);
        }

        private List<FlightPath> generateFlights(Airport originAirport)
        {
            using (var db = new AirContext())
            {
                //query database for flights with correct origin and departure date
                var LEFlights = db.Flights
                                  .Include(flight => flight.Origin)
                                  .Include(flight => flight.Destination)
                                  .Include(flight => flight.PlaneType)
                                  .Where(flight => !flight.isCanceled
                                  && flight.Origin == originAirport)
                                  .ToList();
                return LEFlights.Select(flight => new FlightPath(flight)).ToList();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoadEngineerPage));
        }

        private void DeleteFlight_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AirContext())
            {
                var deleteFlight = DepartList.SelectedItem as FlightPath;
                // get plane from db to delete from list
                db.Remove(db.Flights.Single(flight => flight.FlightId == deleteFlight.flights[0].FlightId));
                db.SaveChanges();
            }
            Frame.Navigate(typeof(LEManageFlightsPage), passedLEParams);
        }
    }
}
