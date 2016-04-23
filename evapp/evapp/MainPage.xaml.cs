 using MySql.Data.MySqlClient.RT;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace evapp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary
    ///>

    public sealed partial class MainPage : Page
    {
        Dictionary<int, string> asd = new Dictionary<int, string>();
        public MainPage()
        {
            this.InitializeComponent();
            asd.Add(1, "asdasd");
            asd.Add(2, "dsadsa");
            foreach (int id in asd.Keys)
            {
                string vuoroid = id.ToString();
                textBlock.Text = vuoroid;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Search));
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Customerservice));
        }
    }
}
