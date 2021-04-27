using Air_3550.Models;
using Air_3550.Repo;
using Database.Utiltities;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System.Linq;

namespace Air_3550.Pages
{
    public sealed partial class BoardingPass : Page
    {
        public BoardingPass()
        {
            this.InitializeComponent();
            // grab the user for the current session to display boarding passes for
            var db = new AirContext();
            var user = db.Users.Include(dbuser => dbuser.CustInfo)
                                    .ThenInclude(custInfo => custInfo.Trips)
                                .Single(dbuser => dbuser.UserId == UserSession.userId);
            BPuser = user;
        }
        private User BPuser;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // turn the parameter we passed in into a Trip variable so we can use it properly
            Trip Btrip = e.Parameter as Trip;
            // Fill out the appropriate information from the trip & user data
            FlightNumber.Text += Btrip.TripId;
            FullName.Text += BPuser.CustInfo.Name;
            AccountId.Text += BPuser.LoginId;
            Origin.Text = Btrip.Origin.AirportCode;
            Destination.Text = Btrip.Destination.AirportCode;
            // Determine whether or not the trip is one way or two way
            // by checking if the origin is equivalent to the final flights destination
            if(Btrip.OriginAirportId != Btrip.Tickets.Last().Flight.Flight.Destination.AirportId)
            {
                RoundTrip.Text = "One Way";
            }
            else
            {
                RoundTrip.Text = "Two Way";
            }
            // set the items source to the list of tickets
            flightList.ItemsSource = Btrip.Tickets;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AccountPage));
        }
    }
}
