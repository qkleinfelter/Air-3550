﻿using Database.Utiltities;
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
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            if (UserSession.userLoggedIn)
            {
                loginNavigator.Visibility = Visibility.Collapsed;
                accountNavigator.Visibility = Visibility.Visible;
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            searchButton.Content = "Searched!";
        }

        private void loginNavigator_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }

        private void accountNavigator_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AccountPage));
        }
    }
}
