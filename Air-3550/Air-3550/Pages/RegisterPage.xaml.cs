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
using Database.Utiltities;
using Database.Models;
using Air_3550.Models;
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
