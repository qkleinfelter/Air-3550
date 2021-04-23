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
    public sealed partial class CheckoutPage : Page
    {
        public class Parameters
        {
            public FlightPath leavingPath;
            public FlightPath? returningPath;
            public Parameters(FlightPath depart, FlightPath returning)
            {
                leavingPath = depart;
                returningPath = returning;
            }
        }
        private Parameters passedInParams;
        public CheckoutPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            passedInParams = e.Parameter as Parameters;
            outputInfo.Title = "Flight Path debug";
            outputInfo.Message = $"{passedInParams.leavingPath.DepartureTime} -> {passedInParams.leavingPath.ArrivalTime} & {passedInParams.returningPath.DepartureTime} -> {passedInParams.returningPath.ArrivalTime}";
            outputInfo.Severity = InfoBarSeverity.Informational;
            outputInfo.IsOpen = true;
        }
    }
}
