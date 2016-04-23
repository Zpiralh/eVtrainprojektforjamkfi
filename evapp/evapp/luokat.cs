using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace evapp
{
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
    public class Lipputiedot
    {
        public int JunavuoroID { get; set; }
        public string Lähtöasema { get; set; }
        public string Pääteasema { get; set; }
        public string Lähtöaika { get; set; }
        public string Pääteaika { get; set; }
        public double hinta { get; set; }
        public string pvm { get; set; }
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

                return "OK";
            }
            catch (Exception ex)
            {

                return (ex.Message + " ");
            }
        }
        public string GetStations(string dbquery, ref Dictionary<string, string> asemat) // Asemien haku tietokannasta ja lisääminen dictionaryyn
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


                return "OK";
            }
            catch (Exception ex)
            {
                connection.Close();
                return (ex.Message + " ");
            }
        }
        public void InsertData(string dbquery)

        {
            MySqlCommand query = connection.CreateCommand();
            query.CommandText = dbquery;
            try { 
            MySqlDataReader result = query.ExecuteReader();
           result.Close();
            } catch
            {

            }

        }
        public string GetCustomerid(string dbquery, ref List<int> IDlist) // AsiakasID:n haku
        {
            MySqlCommand query = connection.CreateCommand();
            query.CommandText = dbquery;

            try
            {
                MySqlDataReader result = query.ExecuteReader();
                while (result.Read())
                {
                    IDlist.Add(int.Parse(result["AsiakasID"].ToString()));
                }
                result.Close();

                return "OK";
            }
            catch (Exception ex)
            {
                return (ex.Message + " ");
            }
        }
    }


    }
