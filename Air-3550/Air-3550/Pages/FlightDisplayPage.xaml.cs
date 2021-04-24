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

namespace Air_3550.Pages
{
    public sealed partial class FlightDisplayPage : Page
    {
        public class Parameters
        {
            public Airport origin;
            public Airport destination;
            public DateTime departingDate;
            public DateTime? returningDate;
            public Parameters(Airport originAirport, Airport destinationAirport, DateTime depart, DateTime returnD)
            {
                origin = originAirport;
                destination = destinationAirport;
                departingDate = depart;
                returningDate = returnD;
            }
            public Parameters(Airport originAirport, Airport destinationAirport, DateTime depart)
            {
                origin = originAirport;
                destination = destinationAirport;
                departingDate = depart;
                returningDate = null;
            }
        }
        private Parameters passedInParams;
        public FlightDisplayPage()
        {
            this.InitializeComponent();
        }

        override protected void OnNavigatedTo(NavigationEventArgs e)
        {
            // change this parameter to an object instead of a string i need to parse later
            passedInParams = e.Parameter as Parameters;
            var returningDate = passedInParams.returningDate;
            DepartHeader.Text = $"{passedInParams.origin.City} to {passedInParams.destination.City} - {passedInParams.departingDate.ToShortDateString()}";

            // This flight list is only for the first leg of a one way, we'll need to add another list view for
            // the return trip at some point as well
            var possibleDepartingPaths = FlightAlgorithm.GenerateRoutes(passedInParams.origin, passedInParams.destination, passedInParams.departingDate);

            if (possibleDepartingPaths.Count == 0)
            {
                outputInfo.Title = "No flights available!";
                outputInfo.Message = "Unfortunately, we don't have any flights available on this route at this time, sorry.";
                outputInfo.Severity = InfoBarSeverity.Error;
                outputInfo.IsOpen = true;
            }

            DepartList.ItemsSource = possibleDepartingPaths;

            if (returningDate != null)
            {
                var nonNullable = (DateTime)returningDate;
                var possibleReturningPaths = FlightAlgorithm.GenerateRoutes(passedInParams.destination, passedInParams.origin, nonNullable);

                if (possibleReturningPaths.Count == 0)
                {
                    outputInfo.Title = "No flights available!";
                    outputInfo.Message = "Unfortunately, we don't have any flights available on this route at this time, sorry.";
                    outputInfo.Severity = InfoBarSeverity.Error;
                    outputInfo.IsOpen = true;
                }

                ReturnList.ItemsSource = possibleReturningPaths;
                ReturnHeader.Text = $"{passedInParams.destination.City} to {passedInParams.origin.City} - {nonNullable.ToShortDateString()}";
                PurchaseButton.Content += "s"; // "Purchase Flight" -> "Purchase Flights" if its round trip
            }
            else
            {
                ReturnHeader.Visibility = Visibility.Collapsed;
                ReturnList.Visibility = Visibility.Collapsed;
            }

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void PurchaseButton_Click(object sender, RoutedEventArgs e)
        {
            var departRoute = DepartList.SelectedItem as FlightPath;
            FlightPath returnRoute = null;
            if (passedInParams.returningDate != null)
            {
                returnRoute = ReturnList.SelectedItem as FlightPath;
            }
            Frame.Navigate(typeof(CheckoutPage), new CheckoutPage.Parameters(departRoute, returnRoute));
        }
    }
}
