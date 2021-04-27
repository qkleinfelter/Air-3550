// ChangeAccountInfoPage.xaml.cs - Air 3550 Project
//
// This program acts as a flight reservation system for a new airline - called Air-3550,
// which allows customers to book and manage trips, which contain many flights, as well as enabling various staff members
// to update the available flights and view statistics about them individually or as a whole.
//
// Authors:     Quinn Kleinfelter, James Golden, & Edward Walsh
// Class:       EECS 3550-001 Software Engineering, Spring 2021
// Instructor:  Dr. Thomas
// Date:        April 28, 2021
// Copyright:   Copyright 2021 by Quinn Kleinfelter, James Golden, & Edward Walsh. All rights reserved.

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
