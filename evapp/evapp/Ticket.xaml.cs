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
    /// Lipunvaraussivu
    /// </summary>
    public sealed partial class Ticket : Page
    {
        databaseMYSQL database = new databaseMYSQL("sql7.freemysqlhosting.net", 3306, "sql7116678", "H1Fwg1G2Hl", "sql7116678");
        int vuoroid;
        double price;
        List<int> IDlist = new List<int>();


        public Ticket()
        {
            this.InitializeComponent();
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

        private void backButton_Click(object sender, RoutedEventArgs e)
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
            if (kplBox.SelectedIndex == -1 || lippuluokkaBox.SelectedIndex == -1 || enimiBox.Text == "Etunimi" || snimiBox.Text == "Sukunimi")
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
                    price = price * kpl;
                }
                else
                {
                    price = price * kpl * 0.75;
                }
                loppuhintaBox.Text = "Total: " + price.ToString() + " €";
            }


        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
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
                price = lippu.hinta;
            }
            else
            {
                lahtoasemabox.Text = "Jotain meni pieleen, lataus epäonnistui";
            }
            base.OnNavigatedTo(e);
        }

        private void confirmationButton_Click(object sender, RoutedEventArgs e)
        {
            int kpl = int.Parse(kplBox.SelectedValue.ToString());
            string enimi = "'" + enimiBox.Text + "'";
            string snimi = "'" + snimiBox.Text + "'";
            string lippuluokka = "'" + lippuluokkaBox.SelectedValue.ToString() + "'";
            database.InsertData("INSERT INTO Asiakas (Etunimi, Sukunimi) VALUES (" + enimi + ", " + snimi + ");");
            string kekke = database.GetCustomerid("SELECT AsiakasID FROM Asiakas WHERE Etunimi = " + enimi + " AND Sukunimi = " + snimi + ";", ref IDlist);
            int customerid = IDlist.Max();
            for (int i = 0; i < kpl; i++)
            {
                database.InsertData("INSERT INTO Lippu (Junavuoro_JunavuoroID, Asiakas_AsiakasID, Lippuluokka) VALUES (" + vuoroid + ", " + customerid + ", " + lippuluokka + ");");
            }
            this.Frame.Navigate(typeof(Confirmation));
        }
    }
}
