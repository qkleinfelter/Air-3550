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
using Windows.Networking.Connectivity;
using Database.Utiltities;
using System.ComponentModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MMSelectPlanePage : Page //INotifyPropertyChanged
    {
        public class MMParameters
        {
            public Airport origin;
            public DateTime departDate;

            public MMParameters(Airport departPort, DateTime depart)
            {
                origin = departPort;
                departDate = depart;
            }
        }
        private MMParameters passedMMParams;
        
        public MMSelectPlanePage()
        {
            this.InitializeComponent();
        }

        override protected void OnNavigatedTo(NavigationEventArgs e)
        {
            passedMMParams = e.Parameter as MMParameters;

            DepartHeader.Text = $"{passedMMParams.origin.City} : {passedMMParams.departDate.ToShortDateString()}";

            DepartList.ItemsSource = generateFlights(passedMMParams.origin);
        }

        //private  MMFlights;
        private List<FlightPath> generateFlights(Airport originAirport)
        {
            using( var db = new AirContext())
            {
                //query database for flights with correct origin and departure date
                var MMFlights = db.Flights
                                  .Include(flight => flight.Origin)
                                  .Include(flight => flight.Destination)
                                  .Include(flight => flight.PlaneType)
                                  .Where(flight => !flight.isCanceled
                                  && flight.Origin == originAirport)
                                  .ToList();
                return MMFlights.Select(flight => new FlightPath(flight)).ToList();
            }
        }

        private void B737_Click(object sender, RoutedEventArgs e)
        {
            
            using (var db = new AirContext())
            {
                var planeChangeflight = DepartList.SelectedItem as FlightPath;
                
                // check that a flight was selected
                if (planeChangeflight != null)
                {
                    // get plane from db to change to change plane
                    var NewPlane = db.Planes.Single(plane => plane.PlaneId == 1);
                    db.Flights.Single(flight => flight.FlightId == planeChangeflight.flights[0].FlightId).PlaneType = NewPlane;
                    db.SaveChanges();
                    
                    // refresh list
                    Frame.Navigate(typeof(MMSelectPlanePage), passedMMParams);
                }
                else
                {
                    OutputInfo.Message = "You must select a flight first.";

                    OutputInfo.Title = "Invalid Input!";
                    OutputInfo.Severity = InfoBarSeverity.Error;
                    OutputInfo.IsOpen = true;
                }

            }
        }

        private void B747_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AirContext())
            {
                var planeChangeflight = DepartList.SelectedItem as FlightPath;
                
                // check that a flight was selected
                if (planeChangeflight != null)
                {
                    // get plane from db to change to change plane
                    var NewPlane = db.Planes.Single(plane => plane.PlaneId == 2);
                    db.Flights.Single(flight => flight.FlightId == planeChangeflight.flights[0].FlightId).PlaneType = NewPlane;
                    db.SaveChanges();

                    // refresh list
                    Frame.Navigate(typeof(MMSelectPlanePage), passedMMParams);
                }
                else
                {
                    OutputInfo.Message = "You must select a flight first.";

                    OutputInfo.Title = "Invalid Input!";
                    OutputInfo.Severity = InfoBarSeverity.Error;
                    OutputInfo.IsOpen = true;
                }
                
            }

            
        }

        private void B757_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AirContext())
            {
                var planeChangeflight = DepartList.SelectedItem as FlightPath;
                
               // check that a flight was selected
               if (planeChangeflight != null)
                {
                    // get plane from db to change to change plane
                    var NewPlane = db.Planes.Single(plane => plane.PlaneId == 3);
                    db.Flights.Single(flight => flight.FlightId == planeChangeflight.flights[0].FlightId).PlaneType = NewPlane;
                    db.SaveChanges();

                    // refresh list
                    Frame.Navigate(typeof(MMSelectPlanePage), passedMMParams);
                }
                else
                {
                    OutputInfo.Message = "You must select a flight first.";

                    OutputInfo.Title = "Invalid Input!";
                    OutputInfo.Severity = InfoBarSeverity.Error;
                    OutputInfo.IsOpen = true;
                }

            }
        }

        private void B777_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AirContext())
            {
                var planeChangeflight = DepartList.SelectedItem as FlightPath;
                
                // check that a flight was selected
                if (planeChangeflight != null)
                {
                    // get plane from db to change to change plane
                    var NewPlane = db.Planes.Single(plane => plane.PlaneId == 4);
                    db.Flights.Single(flight => flight.FlightId == planeChangeflight.flights[0].FlightId).PlaneType = NewPlane;
                    db.SaveChanges();

                    // refresh list
                    Frame.Navigate(typeof(MMSelectPlanePage), passedMMParams);
                }
                else
                {
                    OutputInfo.Message = "You must select a flight first.";

                    OutputInfo.Title = "Invalid Input!";
                    OutputInfo.Severity = InfoBarSeverity.Error;
                    OutputInfo.IsOpen = true;
                }
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MarketingManagerPage));
        }
    }
}
