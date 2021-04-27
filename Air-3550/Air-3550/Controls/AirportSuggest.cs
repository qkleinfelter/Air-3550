// AirportSuggest.cs - Air 3550 Project
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

using Air_3550.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Air_3550.Controls
{
    /**
     * This is a custom control that handles airport suggestion boxes
     * for the various places we use them in our app, such as on the main page
     * the theming for this control is located in Themes/Generic.xaml, as specified
     * for custom controls
     */
    public sealed class AirportSuggest : Control
    {
        public AirportSuggest()
        {
            this.DefaultStyleKey = typeof(AirportSuggest);

            this.Loaded += FillSuggestions;
        }

        // This allows us to provide a label to each individual box
        // when used in conjunction with the dependency property below
        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        // This allows us to access the text in each individual box
        // when used in conjunction with the dependency property below
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
            nameof(Label),
            typeof(string),
            typeof(AirportSuggest),
            new PropertyMetadata(default(string), new PropertyChangedCallback(OnLabelChanged)));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(AirportSuggest),
            new PropertyMetadata(default(string), new PropertyChangedCallback(OnTextChanged)));

        public bool HasLabelValue { get; set; }
        public bool HasTextValue { get; set; }

        public List<string> AirportNames = new();

        private static void OnLabelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // when the label is changed, we need to change has label value
            AirportSuggest labelControl = d as AirportSuggest; //null checks omitted
            String s = e.NewValue as String; //null checks omitted
            if (s == String.Empty)
            {
                labelControl.HasLabelValue = false;
            }
            else
            {
                labelControl.HasLabelValue = true;
            }
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // when the text is changed, we need to change has text value
            AirportSuggest textControl = d as AirportSuggest;
            String s = e.NewValue as String;
            if (s == string.Empty)
            {
                textControl.HasTextValue = false;
            }
            else
            {
                textControl.HasTextValue = true;
            }
        }

        /**
         * This method handles the text changing in the auto suggest box
         * that we use as our main piece of this control
         * allowing us to display appropriate items for the user to choose
         */
        private void AirportSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var suitableItems = new List<string>();
            var splitText = sender.Text.ToLower().Split(" "); // split the input from the user on spaces
            using var db = new AirContext();
            var airports = db.Airports; // grab all the airports in the db
            foreach (var airport in airports)
            {
                // check if any of the pieces in the splitText variable
                // are found in airports cities or airport code
                var found = splitText.All((key) =>
                {
                    return airport.City.ToLower().Contains(key) || airport.AirportCode.ToLower().Contains(key);
                });
                // if it is, add it to the suitableItems list, with the appropriate
                // formatting
                if (found)
                {
                    suitableItems.Add(airport.City + " (" + airport.AirportCode + ")");
                }
            }
            // if there aren't any suitable items, put no results found in the list
            if (suitableItems.Count == 0)
            {
                suitableItems.Add("No Results Found");
            }
            // sort the list
            suitableItems.Sort();
            // set the items source of the suggest box to our suitable items list
            sender.ItemsSource = suitableItems;
        }

        // quick method to fill the suggestions at the beginning
        private async void FillSuggestions(object sender, RoutedEventArgs args)
        {
            using var db = new AirContext();
            var airports = await db.Airports.ToListAsync();
            // fills airportnames with the appropriately formatted airport info
            AirportNames.AddRange(airports.Select(airport => airport.City + " (" + airport.AirportCode + ")"));
            AirportNames.Sort();
        }

        // this method gets run when our template theme is applied
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            // grab the autosuggestbox
            var autoSuggestBox = GetTemplateChild("SuggestBox") as AutoSuggestBox;
            // and add the methods to it that we want
            autoSuggestBox.TextChanged += AirportSuggestBox_TextChanged;
            autoSuggestBox.SuggestionChosen += AirportSuggestBox_SuggestionChosen;
        }

        // when the user selects an airport from the list
        private void AirportSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            // update our text variable so we can access it later
            Text = args.SelectedItem.ToString();
        }
    }
}
