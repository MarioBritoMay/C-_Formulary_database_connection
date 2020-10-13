using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DataBase_Formulary
{
    class connectionDB
    {
        public static MySqlConnection connect()
        {
            String servidor = "localhost";
            String bd = "pruebas";
            String usuario = "mebrito";
            String password = "garumon1996";

            String connectionChain = "Database=" + bd + "; Data Source=" + servidor + "; User Id=" + 
                usuario + "; Password=" + password + "";

            try
            {
                MySqlConnection DBConnect = new MySqlConnection(connectionChain);
                return DBConnect;
            }
            catch(MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;

            }
        }
    }
}
