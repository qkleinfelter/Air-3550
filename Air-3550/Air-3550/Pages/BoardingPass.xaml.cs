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
    public sealed partial class BoardingPass : Page
    {
        public BoardingPass()
        {
            this.InitializeComponent();
            var db = new AirContext();
            var user = db.Users.Include(dbuser => dbuser.CustInfo)
                                .ThenInclude(custInfo => custInfo.Trips)
                                .Single(dbuser => dbuser.UserId == UserSession.userId);
            BPuser = user;
        }
        private User BPuser;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Trip Btrip = e.Parameter as Trip;
            FlightNumber.Text += Btrip.TripId;
            FullName.Text = BPuser.CustInfo.Name;
            AccountId.Text = BPuser.CustInfo.CustomerInfoId.ToString();
            Origin.Text = Btrip.Origin.AirportCode;
            Destination.Text = Btrip.Destination.AirportCode;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AccountPage));
        }
    }
}
