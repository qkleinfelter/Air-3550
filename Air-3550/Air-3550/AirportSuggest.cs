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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550
{
    public sealed class AirportSuggest : Control
    {
        public AirportSuggest()
        {
            this.DefaultStyleKey = typeof(AirportSuggest);
        }

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        DependencyProperty LabelProperty = DependencyProperty.Register(
            nameof(Label),
            typeof(string),
            typeof(AirportSuggest),
            new PropertyMetadata(default(string), new PropertyChangedCallback(OnLabelChanged)));

        public bool HasLabelValue { get; set; }

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
    }
}
