using Air_3550.Models;
using Air_3550.Repo;
using Database.Models;
using Database.Utiltities;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System.Linq;

namespace Air_3550.Pages
{
    public sealed partial class FlightManifestPage : Page
    {
        public FlightManifestPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // grab the scheduled flight parameter
            ScheduledFlight sf = e.Parameter as ScheduledFlight;
            // display some header text
            HeaderText.Text = $"Flight: {sf.Flight.FlightNumber} from {sf.Flight.Origin.City} to {sf.Flight.Destination.City} on {sf.DepartureTime.ToShortDateString()}";
            // and a base for the passengers list
            PassengersText.Text = "Passengers:";

            using var db = new AirContext();
            // grab all the trips in the db, making sure to include their tickets and scheduled flights
            var trips = db.Trips.Include(trip => trip.Tickets)
                                    .ThenInclude(ticket => ticket.Flight);
            // loop through every trip
            foreach (Trip trip in trips)
            {
                // grab the user that is associated with this trip's customer info
                CustomerInfo customer = db.Users.Include(user => user.CustInfo)
                                                .Single(user => user.CustInfo.CustomerInfoId == trip.CustomerInfoId).CustInfo;
                foreach (Ticket ticket in trip.Tickets)
                {
                    // loop through every ticket in the trip
                    // if the ticket isn't canceled
                    // and the scheduled flight associated with it is
                    // the one we are looking to display the manifest for
                    if (!ticket.IsCanceled && ticket.Flight.ScheduledFlightId == sf.ScheduledFlightId)
                    {
                        // if they match, then add them to the passengers display
                        PassengersText.Text += $"\n{customer.Name}";
                    }
                }
            }
        }

        // Manage Account & logout at top right of page
        private void ChangeAccountInfoButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ChangeAccountInfoPage));
        }

        private void LogoutNavigator_Click(object sender, RoutedEventArgs e)
        {
            // reset the users session and send them to the main page
            UserSession.userId = 0;
            UserSession.userLoggedIn = false;
            Frame.Navigate(typeof(MainPage));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // send them back a page
            Frame.GoBack();
        }
    }
}
