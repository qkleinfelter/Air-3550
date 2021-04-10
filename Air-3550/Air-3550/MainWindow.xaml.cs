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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            string text = "";
            using (var db = new AirContext())
            {
                db.Airports.ForEachAsync(airport =>
                {
                    text += (airport.AirportCode + ", " + airport.City + "\n");
                });

                db.Planes.ForEachAsync(plane =>
                {
                    text += (plane.Model + " has max seats " + plane.MaxSeats + " and max distance " + plane.MaxDistance + "\n");
                });

                db.Users.ForEachAsync(user =>
                {
                    text += (user.LoginId + " has role " + user.UserRole + "\n");
                });
            }
            testTextBlock.Text = text;
        }
    }
}
