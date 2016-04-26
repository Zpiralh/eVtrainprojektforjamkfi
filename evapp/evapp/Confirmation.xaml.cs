using System;
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
    /// Lipunvarauksen varmistussivu
    /// </summary>
    public sealed partial class Confirmation : Page
    {
        public Confirmation()
        {
            this.InitializeComponent();
            textBlock.Text = "Lippusi varaaminen onnistui, kiitos!";
        }

        private void button_Click(object sender, RoutedEventArgs e) //etusivulle
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
