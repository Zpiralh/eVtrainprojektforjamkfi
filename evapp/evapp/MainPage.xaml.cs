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
        Dictionary<int, Junavuoro> vuorot = new Dictionary<int, Junavuoro>();
        //public databaseMYSQL database = new databaseMYSQL("sql7.freemysqlhosting.net", 3306, "sql7116678", "H1Fwg1G2Hl", "sql7116678");

        public MainPage()
        {
            this.InitializeComponent();
            /*vuorot.Clear();
            grid1.Visibility = Visibility.Collapsed;
            grid2.Visibility = Visibility.Collapsed;
            grid3.Visibility = Visibility.Collapsed;
            grid4.Visibility = Visibility.Collapsed;
            grid5.Visibility = Visibility.Collapsed;*/
            




        }
        /*public void NextTrains()
        {

            string aika = "'" + DateTime.Now.ToString("hh:mm:00") + "'";
            //Grid[] gridit = { grid1, grid2, grid3, grid4, grid5 };
            //TextBlock[] lahtoblokit = { lahtoBlock1, lahtoBlock2, lahtoBlock3, lahtoBlock4, lahtoBlock5 };
            //TextBlock[] aikablokit = { aikaBlock1, aikaBlock2, aikaBlock3, aikaBlock4, aikaBlock5 };
            string kekke = database.GetRoutes("SELECT * FROM Junavuoro WHERE Lahtoaika > " + aika + " ORDER BY Lahtoaika LIMIT 5;", ref vuorot);
            if (kekke == "OK")
            {
                int krt = 0;
                foreach (Junavuoro vuoro in vuorot.Values)
                {
                    //gridit[krt].Visibility = Visibility.Visible;
                    //asemablokit[krt].Text = vuoro.Lahtoasema;
                    krt++;
                }
            }
            else
            {
                //grid1.Visibility = Visibility.Visible;
            }
            
        }*/
        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Search));
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            //this.Frame.Navigate(typeof(Customerservice));
            //NextTrains();
        }

    }
}
