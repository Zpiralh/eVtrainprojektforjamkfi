﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace evapp
{
    /// <summary>
    /// Ota-yhteyttä -sivu
    /// </summary>
    public sealed partial class Customerservice : Page
    {
        public Customerservice()
        {
            this.InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e) //takaisin etusivulle
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
