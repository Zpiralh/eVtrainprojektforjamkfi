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
    /// Lipunvaraussivu
    /// </summary>
    public sealed partial class Ticket : Page
    {
        databaseMYSQL database = new databaseMYSQL("sql7.freemysqlhosting.net", 3306, "sql7116678", "H1Fwg1G2Hl", "sql7116678"); //tietokannan tiedot. palvelin, username jne..
        int vuoroid;
        double hinta;
        List<int> IDlist = new List<int>();
      
        public Ticket()
        {
            this.InitializeComponent();
            confirmationButton.Visibility = Visibility.Collapsed;
            IDlist.Clear();
            kplBox.Items.Add(1);
            kplBox.Items.Add(2); 
            kplBox.Items.Add(3); 
            kplBox.Items.Add(4);
            kplBox.Items.Add(5); 
            kplBox.Items.Add(6); 
            kplBox.Items.Add(7);
            kplBox.Items.Add(8);
            kplBox.Items.Add(9);
            lippuluokkaBox.Items.Add("Aikuinen");
            lippuluokkaBox.Items.Add("Opiskelija");
            lippuluokkaBox.Items.Add("Varusmies");
            lippuluokkaBox.Items.Add("Eläkeläinen");
            lippuluokkaBox.Items.Add("Lapsi");
        }

        private void backButton_Click(object sender, RoutedEventArgs e) //takasin edelliselle sivulle
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null) return;
            if (rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
            }
        }

        private void buyButton_Click(object sender, RoutedEventArgs e)
        {
            double hinta1;
            huomBox.Text = String.Empty;
            if (kplBox.SelectedIndex == -1 || lippuluokkaBox.SelectedIndex == -1 || enimiBox.Text == "Etunimi" || snimiBox.Text == "Sukunimi") //kaikki kohdat pitää olla täytetty
            {
                huomBox.Text = "Täytä kaikki kohdat";
                if (string.IsNullOrEmpty(enimiBox.Text) || string.IsNullOrEmpty(snimiBox.Text))
                {
                    huomBox.Text = "Täytä kaikki kohdat";
                }
            }
            else
            {
                int kpl = int.Parse(kplBox.SelectedValue.ToString());
                if (lippuluokkaBox.SelectedValue.ToString() == "Aikuinen")
                {
                    hinta1 = hinta * kpl;
                }
                else 
                {
                    hinta1 = hinta * kpl * 0.75;        //opiskelijavarusmies yms alennukset
                }
                hinta = Math.Round(hinta, 2);
                if (kpl > 1)
                {
                    loppuhintaBox.Text = kpl.ToString() + " lippua, yhteensä " + hinta1.ToString() + " €";
                }
                else
                {
                    loppuhintaBox.Text = "1 lippu, hinta " + hinta1.ToString() + " €";
                }
                confirmationButton.Visibility = Visibility.Visible;
            }


        }
        protected override void OnNavigatedTo(NavigationEventArgs e) //tuo junavuoron tiedot edelliseltä sivulta
        {
            if (e.Parameter is Lipputiedot)
            {
                Lipputiedot lippu = (Lipputiedot)e.Parameter;
                lahtoasemabox.Text = lippu.Lähtöasema;
                paateasemabox.Text = lippu.Pääteasema;
                lahtoaikabox.Text = lippu.Lähtöaika;
                paateaikabox.Text = lippu.Pääteaika;
                pvmboksi.Text = lippu.pvm;
                vuoroid = lippu.JunavuoroID;
                hinta = lippu.hinta;
            }
            else
            {
                lahtoasemabox.Text = "Jotain meni pieleen, lataus epäonnistui";
            }
            base.OnNavigatedTo(e);
        }

        private void confirmationButton_Click(object sender, RoutedEventArgs e) //Varaa liput, lisää tietokantaan
        {
            IDlist.Clear();
            int kpl = int.Parse(kplBox.SelectedValue.ToString());
            string enimi = "'" + enimiBox.Text + "'";
            string snimi = "'" + snimiBox.Text + "'";
            string lippuluokka = "'" + lippuluokkaBox.SelectedValue.ToString() + "'";
            database.InsertData("INSERT INTO Asiakas (Etunimi, Sukunimi) VALUES (" + enimi + ", " + snimi + ");"); //luo uuden asiakkaan tietokantaan
            string IDhaku = database.GetCustomerid("SELECT AsiakasID FROM Asiakas WHERE Etunimi = " + enimi + " AND Sukunimi = " + snimi + ";", ref IDlist); //hakee asiakasid:n, jota tarvitaan kun lisätään uutta lippua
            int customerid = IDlist.Max();
            string pvm = "'" + pvmboksi.Text + "'";
            for (int i = 0; i < kpl; i++)
            {
                database.InsertData("INSERT INTO Lippu (Junavuoro_JunavuoroID, Asiakas_AsiakasID, Paivamaara, Lippuluokka) VALUES (" + vuoroid + ", " + customerid + ", " + pvm + ", " + lippuluokka + ");"); //luo databaseen niin monta lippua kuin käyttäjä valitsee
            }
            database.connection.Close();
            this.Frame.Navigate(typeof(Confirmation));
        }
    }
}
