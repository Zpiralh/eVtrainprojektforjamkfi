using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace evapp
{
    /*
    class Lippu
    {
        public string id;
        public string petunimi;
        public string psukunimi;
        public string plahtoasema;
        public string ppaateasema;
    }*/
    public class Junatiedot
    {
        //testausta vain
        public string Junaid { get; set; }
        public string JunaNimi { get; set; }
        public string JunaPVM { get; set; }
    }
    public class Asema
    {
        public string Asemaid { get; set; }
        public string Asemanimi { get; set; }
    }
    public class Junavuoro
    {
        public string JunavuoroID { get; set; }
        public string Lahtoaika { get; set; }
        public string Saapumisaika { get; set; }
        public string JunaID { get; set; }
        public string Lahtoasema { get; set; }
        public string Paateasema { get; set; }
    }

    public class databaseMYSQL
    {
        public MySqlConnection connection = new MySqlConnection();

        public databaseMYSQL(String hostname, int port, String username, String password, String database)
        {
            System.Text.EncodingProvider ppp;
            ppp = System.Text.CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(ppp);
            try
            {
                connection.ConnectionString = "server=" + hostname + ";" +
                    "database=" + database + ";" +
                        "uid=" + username + ";" +
                        "password=" + password + ";" +  
                        "SslMode = None;";
                connection.Open();
            }
            catch 
            {
   
            }

        }
        //esimerkki että miten haetaan kannasta Junat (EI JUNAVUOROJA)
        public string GetTrains(string dbquery, ref Dictionary<int, Junatiedot> junat)
        {
            MySqlCommand query = connection.CreateCommand();
            query.CommandText = dbquery;

            try
            {
                MySqlDataReader result = query.ExecuteReader();
                    while (result.Read())
                    {
                        junat.Add(int.Parse(result["JunaID"].ToString()), new Junatiedot { Junaid = result["JunaID"].ToString(), JunaNimi = result["Junatyyppi"].ToString(), JunaPVM = result["KayttoonottoPvm"].ToString() });
                    }
                    result.Close();
                    connection.Close();
                
                return "OK";
            }
            catch (Exception ex)
            {
                return (ex.Message + " ");
            }
        }

        public string GetRoutes(string dbquery, ref Dictionary<int, Junavuoro> vuorot) // Junavuorojen haku tietokannasta ja lisääminen dictionaryyn
        {
            MySqlCommand query = connection.CreateCommand();
            query.CommandText = dbquery;

            try
            {
                MySqlDataReader result = query.ExecuteReader();
                while (result.Read())
                {
                    vuorot.Add(int.Parse(result["JunavuoroID"].ToString()), new Junavuoro { JunavuoroID = result["JunavuoroID"].ToString(),
                        Lahtoaika = result["Lahtoaika"].ToString(), Saapumisaika = result["Saapumisaika"].ToString(), JunaID = result["JunaID"].ToString(),
                        Lahtoasema = result["Lahtoasema"].ToString(), Paateasema = result["Paateasema"].ToString() });
                }
                result.Close();
                connection.Close();

                return "OK";
            }
            catch (Exception ex)
            {
                return (ex.Message + " ");
            }
        }
        public string GetStations(string dbquery, ref Dictionary<string, string> asemat) // Junavuorojen haku tietokannasta ja lisääminen dictionaryyn
        {
            MySqlCommand query = connection.CreateCommand();
            query.CommandText = dbquery;

            try
            {
                MySqlDataReader result = query.ExecuteReader();
                while (result.Read())
                {
                    asemat.Add(result["Asematunnus"].ToString(), result["Asemanimi"].ToString());
                }
                result.Close();
                connection.Close();

                return "OK";
            }
            catch (Exception ex)
            {
                return (ex.Message + " ");
            }
        }
    }


    }
