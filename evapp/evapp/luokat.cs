using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace evapp { 

    public class Junavuoro // junavuoron luokka joka talletetaan dictionaryyn
    {
        public string JunavuoroID { get; set; }
        public string Lahtoaika { get; set; }
        public string Saapumisaika { get; set; }
        public string JunaID { get; set; }
        public string Lahtoasema { get; set; }
        public string Paateasema { get; set; }
    }
    public class Lipputiedot // Lipputietojen luokka jota käytetään yhdessä dictionaryn kanssa
    {
        public int JunavuoroID { get; set; }
        public string Lähtöasema { get; set; }
        public string Pääteasema { get; set; }
        public string Lähtöaika { get; set; }
        public string Pääteaika { get; set; }
        public double hinta { get; set; }
        public string pvm { get; set; }
    }

    public class databaseMYSQL // Mysql Luokka joka hoitaa yhteydet mysql databasen kanssa (käytetään MySQL.Data.RT.dll referenssinä ei ole valmiina C#:ssä)
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
                        "SslMode = None;"; //MySQL.Data.RT ei tue SSL yhteyksiä joten joudutaan ottamaan ssl pois käytöstä..
                connection.Open(); //Avataan mysql yhteydet
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
                while (result.Read()) //otetaan kaikki data talteen mitä kannasta löytyy kyselyllä
                {
                    vuorot.Add(int.Parse(result["JunavuoroID"].ToString()), new Junavuoro { JunavuoroID = result["JunavuoroID"].ToString(),
                        Lahtoaika = result["Lahtoaika"].ToString(), Saapumisaika = result["Saapumisaika"].ToString(), JunaID = result["JunaID"].ToString(),
                        Lahtoasema = result["Lahtoasema"].ToString(), Paateasema = result["Paateasema"].ToString() }); // syötetään kannasta saadut tiedot Dictionaryyn
                }
                result.Close(); // suljetaan tulokset

                return "OK"; // Palautetaan Arvo "OK" kun kaikki menee ok
            }
            catch (Exception ex)
            {

                connection.Close(); //Suljetaan database yhteydet kun tapahtuu virhe
                return (" "+ ex.Message + " "); // Palautetaan virheilmoitus
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
                    asemat.Add(result["Asematunnus"].ToString(), result["Asemanimi"].ToString()); //tulokset Dictionaryyn
                }
                result.Close();


                return "OK"; // Palautetaan Arvo "OK" kun kaikki menee ok
            }
            catch (Exception ex)
            {

                connection.Close(); //Suljetaan database yhteydet kun tapahtuu virhe
                return (" " + ex.Message + " "); // Palautetaan virheilmoitus
            }
        }
        public void InsertData(string dbquery) //Metodi tiedon lisäämiseen tietokantaan, käytetään uuden asiakkaan ja lipun lisäyksessä

        {
            MySqlCommand query = connection.CreateCommand();
            query.CommandText = dbquery; //lähetettävä kysely tietokannalle
            try { 
            MySqlDataReader result = query.ExecuteReader(); // suoritetaan sql kysely
           result.Close(); // suljetaan kysely
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
                MySqlDataReader result = query.ExecuteReader(); //suoritetaan kysely 
                while (result.Read())
                {
                    IDlist.Add(int.Parse(result["AsiakasID"].ToString())); //syötetään asiakasidt listaan
                }
                result.Close(); // suljetaan tulokset

                return "OK"; // Palautetaan Arvo "OK" kun kaikki menee ok
            }
            catch (Exception ex)
            {
                connection.Close(); //Suljetaan database yhteydet kun tapahtuu virhe
                return (" " + ex.Message + " "); // Palautetaan virheilmoitus
            }
        }
    }


    }
