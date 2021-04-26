using Microsoft.UI.Xaml.Controls;
using System;
using Air_3550.Controls;

namespace Air_3550.Pages
{
    public sealed partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            this.InitializeComponent();
            UserInfoControl.OnNavigateParentReady += UserInfoControl_OnNavigateParentReady;
        }

        private void UserInfoControl_OnNavigateParentReady(object source, EventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
