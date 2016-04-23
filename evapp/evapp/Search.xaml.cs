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
using MySql.Data.MySqlClient;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace evapp
{
    /// <summary>
    /// Junavuorojen hakusivu
    /// </summary>
    public sealed partial class Search : Page
    {
        public Dictionary<string, string> asemat = new Dictionary<string, string>();
        public Dictionary<int, Junavuoro> vuorot = new Dictionary<int, Junavuoro>();
        public databaseMYSQL database = new databaseMYSQL("localhost", 3306, "root", "", "test");
        int vuoroid;
        double price;

        public Search()
        {
            this.InitializeComponent();
            buyButton.Visibility = Visibility.Collapsed;


            string AsemaPalautus = database.GetStations("SELECT * FROM Asema;", ref asemat);             //haetaan asemat Tietokannasta
            if (AsemaPalautus == "OK")
            {
                foreach (string value in asemat.Values)
                {
                    comboBox.Items.Add(value);
                    comboBox1.Items.Add(value);
                }
            }
            else
            {
                lahtoasemabox.Text = "Yhteys tietokantaan epäonnistui";
            }
        }
        public string muuttuja = " ";


        private void button_Click(object sender, RoutedEventArgs e)
        {
            lahtoasemabox.Text = paateasemabox.Text = lahtoaikabox.Text = paateaikabox.Text = pvmboksi.Text = hintaboksi.Text = textBlock.Text = String.Empty;
            if (comboBox.SelectedIndex == -1 || comboBox1.SelectedIndex == -1 || comboBox.SelectedValue == comboBox1.SelectedValue || pvmvalinta.Date < DateTime.Today)
            {
                lahtoasemabox.Text = "Tarkista hakuehdot";
                buyButton.Visibility = Visibility.Visible; //MUISTA POISTAA, testausta varten
            }
            else
            {

                Searchinfo();
                Results();
                buyButton.Visibility = Visibility.Visible;
            }
        }


        public void Results()  //Aikojen haku dictionarysta
        {
            string lähtö = "'" + asemat.FirstOrDefault(x => x.Value.Contains(comboBox.SelectedValue.ToString())).Key + "'"; //Helsingin rautatieasema = 'HKI', haetaan tietokannasta asematunnuksella
            string pääte = "'" + asemat.FirstOrDefault(x => x.Value.Contains(comboBox1.SelectedValue.ToString())).Key + "'";
            vuorot.Clear();
            string kekke = database.GetRoutes("SELECT * FROM Junavuoro WHERE Lahtoasema = " + lähtö + " AND Paateasema = " + pääte + ";", ref vuorot); // tietokannalle lähetettävä query
            if (kekke == "OK")
            {
                foreach (Junavuoro vuoro in vuorot.Values)
                {
                    lahtoaikabox.Text = vuoro.Lahtoaika;
                    paateaikabox.Text = vuoro.Saapumisaika;
                }
            }
            else
            {
                textBlock.Text = "Haku epäonnistui." + kekke;
            }
        }

        /*
        Löysin miten saa luotua nappeja ohjelmallisesti mutta ongelma on nyt se että miten sitä saa painettua ja siirrettyä tietoa toiseen ikkunaan ja että miten saisi tekstiä ja buttonin samalle riville
        Alla löytämäni koodi joka luo buttonin stackpaneeliin:

                for (int i = 0; i < 10; i++)
            {
                //create button
                Button btn = new Button();
                btn.Content = "buttoniolen";
                btn.Name = "Juna " + i;
                btn.Margin = new Thickness(0, 0, 0, 12);
                stackPanel.Children.Add(btn);
            } 

            
            
            
        */
        private void Searchinfo() //Muiden tietojen haku ja tulosten sijoittelu
        {
            lahtoasemabox.Text = comboBox.SelectedValue.ToString();
            paateasemabox.Text = comboBox1.SelectedValue.ToString();
            int year = pvmvalinta.Date.Year;
            int month = pvmvalinta.Date.Month;
            int day = pvmvalinta.Date.Day;
            string lähtöasema = asemat.FirstOrDefault(x => x.Value.Contains(comboBox.SelectedValue.ToString())).Key;
            string pääteasema = asemat.FirstOrDefault(x => x.Value.Contains(comboBox1.SelectedValue.ToString())).Key;
            if ((lähtöasema == "HKI" && pääteasema == "OUL") || (lähtöasema == "OUL" && pääteasema == "HKI"))
            {
                price = 15;
            }
            else
            {
                price = 10;
            }
            if (pvmvalinta.Date.DayOfWeek == DayOfWeek.Sunday) //Kalliimmat liput sunnuntaina
            {
                price = price * 1.5;
            }
            pvmboksi.Text = day + "." + month + "." + year;
            hintaboksi.Text = price + "  €";

        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void buyButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (int id in vuorot.Keys)
            {
                vuoroid = id;
            }
            Lipputiedot lippu = new Lipputiedot
            {
                JunavuoroID = vuoroid,
                Lähtöasema = lahtoasemabox.Text,
                Pääteasema = paateasemabox.Text,
                Lähtöaika = lahtoaikabox.Text,
                Pääteaika = paateaikabox.Text,
                hinta = price,
                pvm = pvmboksi.Text
            };
            this.Frame.Navigate(typeof(Ticket), lippu);
        } 
    }
}
