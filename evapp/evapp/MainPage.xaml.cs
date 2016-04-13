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
 
        public Dictionary<int, Junatiedot> junat = new Dictionary<int, Junatiedot>();
        public MainPage()
        {
            this.InitializeComponent();
  
        }
        public string muuttuja = " ";
     

       


        private void button_Click(object sender, RoutedEventArgs e)
        {
            ListTrains();

        }
        private void ListTrains()
        {
   
            databaseMYSQL database = new databaseMYSQL("localhost", 3306, "root", "", "test");
                string kekke = database.GetTrains("SELECT * FROM juna", ref junat);
            //varmistus juttuja vielä kun haetaan junat ei junavuoroja (tietokannan ongelmien selvitystä oli)
            if(kekke == "OK") { 
            foreach (Junatiedot club in junat.Values)
            {
                muuttuja += ("ID: " + club.Junaid + " Nimi: " + club.JunaNimi + " PVM: " + club.JunaPVM + "\n");
                   
                }
                textBlock.Text = "OK " + muuttuja;
            }
            else
            {
                textBlock.Text = muuttuja + " " + kekke;
            }

        }
    }
}
