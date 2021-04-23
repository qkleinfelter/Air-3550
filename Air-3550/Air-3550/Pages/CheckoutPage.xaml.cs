using Air_3550.Models;
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
    public sealed partial class CheckoutPage : Page
    {
        public class Parameters
        {
            public FlightPath leavingPath;
            public FlightPath? returningPath;
            public Parameters(FlightPath depart, FlightPath returning)
            {
                leavingPath = depart;
                returningPath = returning;
            }
        }
        private Parameters passedInParams;
        private int leavingPathCost = 0;
        private int returningPathCost = 0;
        public CheckoutPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            passedInParams = e.Parameter as Parameters;
            outputInfo.Title = "Flight Path debug";
            outputInfo.Message = $"{passedInParams.leavingPath.DepartureTime} -> {passedInParams.leavingPath.ArrivalTime} & {passedInParams.returningPath.DepartureTime} -> {passedInParams.returningPath.ArrivalTime}";
            outputInfo.Severity = InfoBarSeverity.Informational;
            outputInfo.IsOpen = true;
            leavingPathCost = int.Parse(passedInParams.leavingPath.Price.Substring(1));
            returningPathCost = int.Parse(passedInParams.returningPath.Price.Substring(1));
            DisplayInfo();
        }
        
        private void DisplayInfo()
        {
            int numFlights = passedInParams.leavingPath.flights.Count;
            Airport origin = passedInParams.leavingPath.flights[0].Origin;
            Airport dest = passedInParams.leavingPath.flights[numFlights - 1].Destination;
            departInfo.Text = "Departing Flight Information:";
            departInfo.Text += $"\nFrom {origin.City} to {dest.City}";
            departInfo.Text += $"\nTotal Flight Duration: {passedInParams.leavingPath.FormattedDuration}";
            departInfo.Text += $"\nWith stops: {passedInParams.leavingPath.FormattedStops}";
            departInfo.Text += $"\nPrice: ${leavingPathCost}";

            returnInfo.Text = "Returning Flight Information:";
            returnInfo.Text += $"\nFrom {dest.City} to {origin.City}";
            returnInfo.Text += $"\nTotal Flight Duration: {passedInParams.returningPath.FormattedDuration}";
            returnInfo.Text += $"\nWith stops: {passedInParams.returningPath.FormattedStops}";
            returnInfo.Text += $"\nPrice: ${returningPathCost}";

            summaryInfo.Text = "Summary:";
            summaryInfo.Text += $"Total Cost: {leavingPathCost + returningPathCost}";
        }

        private void useCredit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void usePoints_Click(object sender, RoutedEventArgs e)
        {

        }

        private void useCreditCard_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
