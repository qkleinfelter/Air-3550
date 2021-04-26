using Air_3550.Controls;
using Air_3550.Repo;
using Database.Utiltities;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;

namespace Air_3550.Pages
{
    public sealed partial class ChangeAccountInfoPage : Page
    {
        public ChangeAccountInfoPage()
        {
            this.InitializeComponent();
            UserInfoControl.OnNavigateParentReady += UserInfoControl_OnNavigateParentReady;
        }

        // send the user back to their appropriate page
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
                Frame.Navigate(typeof(FlightManagerPage));
            }
        }
    }
}
