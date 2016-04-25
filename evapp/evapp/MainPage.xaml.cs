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
    /// Pääsivu
    /// </summary
    ///>
    
    public sealed partial class MainPage : Page
    {
        Dictionary<int, Junavuoro> vuorot = new Dictionary<int, Junavuoro>();
        public Dictionary<string, string> asemat = new Dictionary<string, string>();
        public databaseMYSQL database = new databaseMYSQL("sql7.freemysqlhosting.net", 3306, "sql7116678", "H1Fwg1G2Hl", "sql7116678"); //tietokannan tiedot. palvelin, username jne..
        double hinta = 8;
        string pv = DateTime.Now.ToString("dd.MM.yyyy");


        public MainPage()
        {
            this.InitializeComponent();
            vuorot.Clear();
            grid1.Visibility = Visibility.Collapsed;
            grid2.Visibility = Visibility.Collapsed;
            grid3.Visibility = Visibility.Collapsed;
            grid4.Visibility = Visibility.Collapsed;
            grid5.Visibility = Visibility.Collapsed;
            textBlock1.Text = "Seuraavaksi lähteviä junia, osta liput äkkilähtöhintaan!";
            NextTrains();
        }
        public void NextTrains() //Hakee tietokannasta max 5 seuraavaksi lähtevää junaa, ja listaa ne etusivulle, josta voi nopeasti ostaa niihin "äkkilähtö-lippuja"
        {
            string asemahaku = database.GetStations("SELECT * FROM Asema;", ref asemat);
            if (asemahaku == "OK")
            {
                string aika = "'" + DateTime.Now.ToString("HH:mm:00") + "'";
                Grid[] gridit = { grid1, grid2, grid3, grid4, grid5 };                             //taulukot elementeille, jotta niitä voidaan käyttää foreach-loopissa
                TextBlock[] lahtoblokit = { lahtoBlock1, lahtoBlock2, lahtoBlock3, lahtoBlock4, lahtoBlock5 };
                TextBlock[] paateblokit = { paateBlock1, paateBlock2, paateBlock3, paateBlock4, paateBlock5 };
                TextBlock[] aikablokit = { aikaBlock1, aikaBlock2, aikaBlock3, aikaBlock4, aikaBlock5 };
                TextBlock[] idblokit = { idBlock1, idBlock2, idBlock3, idBlock4, idBlock5 };
                Button[] napit = { button1, button2, button3, button4, button5 };
                gridit[4].Visibility = Visibility.Collapsed;
                napit[4].Visibility = Visibility.Collapsed;
                string kekke = database.GetRoutes("SELECT * FROM Junavuoro WHERE Lahtoaika > " + aika + " ORDER BY Lahtoaika LIMIT 5;", ref vuorot); //hakee 5 pian lähtevää junavuoroa
                if (kekke == "OK")
                {
                    int krt = 0;
                    foreach (Junavuoro vuoro in vuorot.Values) //tuo vain niin monta gridiä näkyviin kuin vuoroja löytyy (krt-muuttuja)
                    {
                        string lahtoasemanimi = asemat[vuoro.Lahtoasema];
                        string paateasemanimi = asemat[vuoro.Paateasema];
                        gridit[krt].Visibility = Visibility.Visible;
                        napit[krt].Visibility = Visibility.Visible;
                        idblokit[krt].Visibility = Visibility.Collapsed;
                        lahtoblokit[krt].Text = lahtoasemanimi;
                        paateblokit[krt].Text = paateasemanimi;
                        aikablokit[krt].Text = vuoro.Lahtoaika.Substring(0, 5) + " - " + vuoro.Saapumisaika.Substring(0, 5);
                        idblokit[krt].Text = vuoro.JunavuoroID;     
                        krt++;
                    }
                }

                else
                {
                    grid1.Visibility = Visibility.Visible;
                    lahtoBlock1.Text = "Yhteys tietokantaan epäonnistui";
                }
            }
            else
            {
                grid1.Visibility = Visibility.Visible;
                lahtoBlock1.Text = "Yhteys tietokantaan epäonnistui";
            }
            
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Search)); //hakusivulle
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Customerservice)); //ota yhteyttä-sivulle
        }

        private void button1_Click(object sender, RoutedEventArgs e) //siirtyy ja vie tietyn vuoron tiedot lipunostosivulle
        {
            Lipputiedot lippu = new Lipputiedot
            {
                JunavuoroID = int.Parse(idBlock1.Text),
                Lähtöasema = lahtoBlock1.Text,
                Pääteasema = paateBlock1.Text,
                Lähtöaika = aikaBlock1.Text.Substring(0, 5),
                Pääteaika = aikaBlock1.Text.Substring(8),
                hinta = hinta,
                pvm = pv
            };
            database.connection.Close();
            this.Frame.Navigate(typeof(Ticket), lippu);

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Lipputiedot lippu = new Lipputiedot
            {
                JunavuoroID = int.Parse(idBlock2.Text.ToString()),
                Lähtöasema = lahtoBlock2.Text,
                Pääteasema = paateBlock2.Text,
                Lähtöaika = aikaBlock2.Text.Substring(0, 5),
                Pääteaika = aikaBlock2.Text.Substring(8),
                hinta = hinta,
                pvm = pv
            };
            database.connection.Close();
            this.Frame.Navigate(typeof(Ticket), lippu);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Lipputiedot lippu = new Lipputiedot
            {
                JunavuoroID = int.Parse(idBlock3.Text.ToString()),
                Lähtöasema = lahtoBlock3.Text,
                Pääteasema = paateBlock3.Text,
                Lähtöaika = aikaBlock3.Text.Substring(0, 5),
                Pääteaika = aikaBlock3.Text.Substring(8),
                hinta = hinta,
                pvm = pv
            };
            database.connection.Close();
            this.Frame.Navigate(typeof(Ticket), lippu);
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            Lipputiedot lippu = new Lipputiedot
            {
                JunavuoroID = int.Parse(idBlock4.Text.ToString()),
                Lähtöasema = lahtoBlock4.Text,
                Pääteasema = paateBlock4.Text,
                Lähtöaika = aikaBlock4.Text.Substring(0, 5),
                Pääteaika = aikaBlock4.Text.Substring(8),
                hinta = hinta,
                pvm = pv
            };
            database.connection.Close();
            this.Frame.Navigate(typeof(Ticket), lippu);
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            Lipputiedot lippu = new Lipputiedot
            {
                JunavuoroID = int.Parse(idBlock5.Text.ToString()),
                Lähtöasema = lahtoBlock5.Text,
                Pääteasema = paateBlock5.Text,
                Lähtöaika = aikaBlock5.Text.Substring(0, 5),
                Pääteaika = aikaBlock5.Text.Substring(8),
                hinta = hinta,
                pvm = pv
            };
            database.connection.Close();
            this.Frame.Navigate(typeof(Ticket), lippu);
        }

        private void button_Click_2(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}