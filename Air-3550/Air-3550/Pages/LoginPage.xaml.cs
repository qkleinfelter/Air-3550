using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Text;
using System.Security.Cryptography;
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
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            //get user password
            string userInput = passwordBox.Password.ToString();

            // Using built-in Cryptography class to produce SHA512 hash of passwords.
            using (SHA512 sha512Hash = SHA512.Create())
            {
                // Have to go from String to byte array
                byte[] inputBytes = Encoding.UTF8.GetBytes(userInput);
                byte[] hashBytes = sha512Hash.ComputeHash(inputBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

                //For necessary output to communicate to user.
                outputBlock.Text = "Hash is: " + hash.ToLower();
            }

        }
    }
}
