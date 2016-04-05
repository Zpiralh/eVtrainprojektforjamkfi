using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace evapp
{
    class Lippu
    {
        public string id;
        public string petunimi;
        public string psukunimi;
        public string plahtoasema;
        public string ppaateasema;
    }
    class Junat
    {
        //host, port, user, passwd, kanta
        databaseMYSQL database = new databaseMYSQL("localhost", 3306, "root","", "test");
        public void GetTrains(ref string muuttuja)
        {

            muuttuja = "Tähä sit vitusti junia";


            for (int tekstiaa = 0; tekstiaa < 10; tekstiaa++)
            {
                muuttuja = muuttuja + "\nTähä sit vitusti junia";
            }
        }
    }

    public class databaseMYSQL
    {
        public MySqlConnection connection = new MySqlConnection();


        public databaseMYSQL(String hostname, int port, String username, String password, String database)
        {
            try
            {
                connection.ConnectionString = "server=" + hostname + ";" +
                    "database=" + database + ";" +
                        "uid=" + username + ";" +
                        "password=" + password + ";";
                connection.Open();
            }
            catch
            {

            }

        }

        public void FindFromDB(string dbquery, Dictionary<string, string> lista)
        {
            MySqlCommand query = connection.CreateCommand();
            query.CommandText = dbquery;
            try
            {
                MySqlDataReader result = query.ExecuteReader();
                result.Close();
                connection.Close();
            }
            catch
            {
                // Virheilmoitus
            }
        }

     
    }   
}
