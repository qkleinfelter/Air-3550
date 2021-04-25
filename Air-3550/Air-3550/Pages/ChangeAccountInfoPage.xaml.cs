using Air_3550.Controls;
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


namespace Air_3550.Pages
{
    public sealed partial class ChangeAccountInfoPage : Page
    {
        public ChangeAccountInfoPage()
        {
            this.InitializeComponent();
            UserInfoControl.OnNavigateParentReady += UserInfoControl_OnNavigateParentReady;
        }

        private void UserInfoControl_OnNavigateParentReady(object source, EventArgs e)
        {
            var db = new AirContext();
            var user = db.Users.Include(dbuser => dbuser.CustInfo).Single(dbuser => dbuser.UserId == UserSession.userId);
            if (user.UserRole == Role.CUSTOMER)
            {
                Frame.Navigate(typeof(MainPage));
            }
            else if (user.UserRole == Role.LOAD_ENGINEER)
            {
                Frame.Navigate(typeof(LoadEngineerPage));
            }
            else if (user.UserRole == Role.MARKETING_MANAGER)
            {
                Frame.Navigate(typeof(MarketingManagerPage));
            }
            else if (user.UserRole == Role.ACCOUNTING_MANAGER)
            {
                Frame.Navigate(typeof(AccountingManagerPage));
            }
            else
            {
                // Needs to be FlightManagerPage when it exists...
                Frame.Navigate(typeof(MainPage));
            }


        }
    }
}
