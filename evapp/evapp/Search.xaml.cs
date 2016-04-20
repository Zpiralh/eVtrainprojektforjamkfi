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
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Search : Page
    {
        public Dictionary<int, Junatiedot> junat = new Dictionary<int, Junatiedot>();
        public Dictionary<string, string> asemat = new Dictionary<string, string>();
        public Dictionary<int, Junavuoro> vuorot = new Dictionary<int, Junavuoro>();
        databaseMYSQL database = new databaseMYSQL("localhost", 3306, "root", "", "test");

        public Search()
        {
            this.InitializeComponent();
            buyButton.Visibility = Visibility.Collapsed;
            //haetaan asemat Tietokannasta

            string AsemaPalautus = database.GetStations("SELECT * FROM Asema;", ref asemat);
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
                //Virheilmoitus Toteutus tapa kehitetään jossain kohtaa
            }
        }
        public string muuttuja = " ";


        private void button_Click(object sender, RoutedEventArgs e)
        {
            lahtoasemabox.Text = paateasemabox.Text = lahtoaikabox.Text = paateaikabox.Text = pvmboksi.Text = hintaboksi.Text = textBlock.Text = String.Empty;
            if (comboBox.SelectedValue == comboBox1.SelectedValue || pvmvalinta.Date < DateTime.Today) // Ei matkata menneisyydessä eikä saman kaupungin sisällä
            {
                lahtoasemabox.Text = "Tarkista hakuehdot";
                buyButton.Visibility = Visibility.Visible; //MUISTA POISTAA, testausta varten
            }
            else
            {
                //ListTrains();
                Searchinfo();
                //Results();
                buyButton.Visibility = Visibility.Visible;
            }

        }

        private void ListTrains()
        {
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

        public void Results()  //Aikojen haku dictionarysta
        {
            string lähtö = "'" + asemat.FirstOrDefault(x => x.Value.Contains(comboBox.SelectedValue.ToString())).Key + "'"; //Helsingin rautatieasema = 'HKI', haetaan tietokannasta asematunnuksella
            string pääte = "'" + asemat.FirstOrDefault(x => x.Value.Contains(comboBox1.SelectedValue.ToString())).Key + "'";
            string kekke = database.GetRoutes("SELECT * FROM Junavuoro WHERE Lahtoasema = " + lähtö + " AND Paateasema = " + pääte + ";", ref vuorot); // tietokannalle lähetettävä query
            if (kekke == "OK")
            {
                vuorot.Clear();
                foreach (Junavuoro vuoro in vuorot.Values)
                {
                    lahtoaikabox.Text = vuoro.Lahtoaika;
                    paateasemabox.Text = vuoro.Saapumisaika;
                }
            }
            else
            {
                textBlock.Text = "Haku epäonnistui.";
            }
        }

        private void Searchinfo() //Muiden tietojen haku ja tulsoten sijoittelu
        {
            lahtoasemabox.Text = comboBox.SelectedValue.ToString();
            paateasemabox.Text = comboBox1.SelectedValue.ToString();
            int year = pvmvalinta.Date.Year;
            int month = pvmvalinta.Date.Month;
            int day = pvmvalinta.Date.Day;
            string lähtöasema = asemat.FirstOrDefault(x => x.Value.Contains(comboBox.SelectedValue.ToString())).Key;
            string pääteasema = asemat.FirstOrDefault(x => x.Value.Contains(comboBox1.SelectedValue.ToString())).Key;
            double price;
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
            hintaboksi.Text = price + " €";


        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void buyButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}