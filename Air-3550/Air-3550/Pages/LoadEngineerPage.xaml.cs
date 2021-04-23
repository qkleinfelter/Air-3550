using Air_3550.Repo;
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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoadEngineerPage : Page
    {
        public LoadEngineerPage()
        {
            this.InitializeComponent();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateAddParameters())
            {
                // may not need this stuff later
                OutputInfo.Title = "Valid Input!";
                OutputInfo.Severity = InfoBarSeverity.Success;

                OutputInfo.Message = "We can work with your data!";
            }
            else
            {
                OutputInfo.Title = "Invalid Input!";
                OutputInfo.Severity = InfoBarSeverity.Error;
                OutputInfo.IsOpen = true;
            }

        }

        private bool ValidateAddParameters()
        {
            // start with our return as true, it will get changed to false if either of our input parameters are bad
            bool valid = true;
            OutputInfo.Message = "Your search could not be processed due to invalid parameters: ";
            if (string.IsNullOrEmpty(originPickerAdd.Text))
            {
                OutputInfo.Message += "\nYou must select an origin airport";
                valid = false;
            }
            if (string.IsNullOrEmpty(destPickerAdd.Text))
            {
                OutputInfo.Message += "\nYou must select a destination airport";
                valid = false;
            }
            if (originPickerAdd.Text == destPickerAdd.Text)
            {
                OutputInfo.Message += "\nOrigin and destination airports must be different";
                valid = false;
            }
            if (!timePickerAdd.SelectedTime.HasValue)
            {
                OutputInfo.Message += "\nYou must select a valid time";
                valid = false;
            }
            return valid;
        }
    }
}
