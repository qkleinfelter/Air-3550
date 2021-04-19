﻿using Air_3550.Controls;
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
            Frame.Navigate(typeof(MainPage));
        }
    }
}