using Air_3550.Models;
using Air_3550.Repo;
using Database.Models;
using Microsoft.EntityFrameworkCore;
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
            ScheduledFlight sf = e.Parameter as ScheduledFlight;
            HeaderText.Text = $"Flight: {sf.Flight.FlightNumber} from {sf.Flight.Origin.City} to {sf.Flight.Destination.City} on {sf.DepartureTime.ToShortDateString()}";
            PassengersText.Text = "Passengers:";

            using (var db = new AirContext())
            {
                var trips = db.Trips.Include(trip => trip.Tickets).ThenInclude(ticket => ticket.Flight);
                foreach (Trip trip in trips)
                {
                    CustomerInfo customer = db.Users.Include(user => user.CustInfo).Single(user => user.CustInfo.CustomerInfoId == trip.CustomerInfoId).CustInfo;
                    foreach (Ticket ticket in trip.Tickets)
                    {
                        if (!ticket.isCanceled && ticket.Flight.ScheduledFlightId == sf.ScheduledFlightId)
                        {
                            PassengersText.Text += $"\n{customer.Name}";
                        }
                    }
                }
            }
        }
    }
}
