using Air_3550.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Air_3550.Controls
{
    public sealed class AirportSuggest : Control
    {
        public AirportSuggest()
        {
            this.DefaultStyleKey = typeof(AirportSuggest);

            this.Loaded += FillSuggestions;
        }

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

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

        private void AirportSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var suitableItems = new List<string>();
            var splitText = sender.Text.ToLower().Split(" ");
            using var db = new AirContext();
            var airports = db.Airports;
            foreach (var airport in airports)
            {
                var found = splitText.All((key) =>
                {
                    return airport.City.ToLower().Contains(key) || airport.AirportCode.ToLower().Contains(key);
                });
                if (found)
                {
                    suitableItems.Add(airport.City + " (" + airport.AirportCode + ")");
                }
            }
            if (suitableItems.Count == 0)
            {
                suitableItems.Add("No Results Found");
            }
            suitableItems.Sort();
            sender.ItemsSource = suitableItems;
        }

        private async void FillSuggestions(object sender, RoutedEventArgs args)
        {
            using var db = new AirContext();
            var airports = await db.Airports.ToListAsync();
            AirportNames.AddRange(airports.Select(airport => airport.City + " (" + airport.AirportCode + ")"));
            AirportNames.Sort();
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var autoSuggestBox = GetTemplateChild("SuggestBox") as AutoSuggestBox;
            autoSuggestBox.TextChanged += AirportSuggestBox_TextChanged;
            autoSuggestBox.SuggestionChosen += AirportSuggestBox_SuggestionChosen;
        }

        // sadly suggestion chosen doesn't apply if you remove the suggestion,
        // so if you select an airport and then de-select it, we still think
        // you have it selected from our validation, we need to find a way to 
        // handle this at some point.
        private void AirportSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            Text = args.SelectedItem.ToString();
        }
    }
}
