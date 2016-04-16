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
        public Dictionary<string, string> asemat = new Dictionary<string, string>();
        public Dictionary<int, Junavuoro> vuorot = new Dictionary<int, Junavuoro>();

        public MainPage()
        {
            this.InitializeComponent();

            asemat.Add("HKI", "Helsinki rautatieasema");
            asemat.Add("JKL", "Jyväskylä matkakeskus");
            asemat.Add("OUL", "Oulun rautatieasema");
            asemat.Add("TRE", "Tampereen rautatieasema");
            asemat.Add("TKU", "Turun rautatieasema");

            foreach (string value in asemat.Values)
            {
                comboBox.Items.Add(value);
                comboBox1.Items.Add(value);
            }


        }
        public string muuttuja = " ";





        private void button_Click(object sender, RoutedEventArgs e)
        {
            //ListTrains();
            Search();
        }
        private void ListTrains()
        {

            databaseMYSQL database = new databaseMYSQL("localhost", 3306, "root", "", "test");
            string kekke = database.GetTrains("SELECT * FROM Juna", ref junat);
            //varmistus juttuja vielä kun haetaan junat ei junavuoroja (tietokannan ongelmien selvitystä oli)
            if (kekke == "OK")
            {
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

        public void Results()
        {
            string lähtö = "'" + asemat.FirstOrDefault(x => x.Value.Contains(comboBox.SelectedValue.ToString())).Key + "'";
            string pääte = "'" + asemat.FirstOrDefault(x => x.Value.Contains(comboBox1.SelectedValue.ToString())).Key + "'";
            databaseMYSQL database = new databaseMYSQL("localhost", 3306, "root", "", "test");
            string kekke = database.GetTrains("SELECT Lahtoaika, Saapumisaika FROM Junavuoro WHERE Lahtoasema = " + lähtö + " AND Paateasema = " + pääte + "", ref junat);
            // jäi kesken, en edes tiedä meneekö noin :D
        }
        private void Search()
        {
            lahtoasemabox.Text = comboBox.SelectedValue.ToString();
            paateasemabox.Text = comboBox1.SelectedValue.ToString();
            string date = pvmvalinta.Date.ToString();
            date = date.Substring(0, 10);
            textBlock.Text = date;
        }

    }
}
