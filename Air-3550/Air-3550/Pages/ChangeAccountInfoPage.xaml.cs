using Air_3550.Controls;
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
    public sealed partial class ChangeAccountInfoPage : Page
    {
        public ChangeAccountInfoPage()
        {
            this.InitializeComponent();
            UserInfoControl.OnNavigateParentReady += UserInfoControl_OnNavigateParentReady;
        }

        private void UserInfoControl_OnNavigateParentReady(object source, EventArgs e)
        {
            if (UserSession.user.UserRole == Role.CUSTOMER)
            {
                Frame.Navigate(typeof(MainPage));
            }
            else if (UserSession.user.UserRole == Role.LOAD_ENGINEER)
            {
                Frame.Navigate(typeof(LoadEngineerPage));
            }
            else if (UserSession.user.UserRole == Role.MARKETING_MANAGER)
            {
                Frame.Navigate(typeof(MarketingManagerPage));
            }
            else if (UserSession.user.UserRole == Role.ACCOUNTING_MANAGER)
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
