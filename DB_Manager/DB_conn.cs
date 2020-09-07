//=============================================================================
// Authors: Francesca Rossi, Leandro Corsano
//=============================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace DB_Manager
{
    class DB_conn
    {
        /*classe singleton che permette la connesione con il Database*/
        private static MySqlConnection instance = null;

        private static readonly object padlock = new object();

        private DB_conn() { }

        public static MySqlConnection getInstance()
        {
            lock (padlock)
            {
                bool conn=true;
                if (instance == null)
                {
                    instance = new MySqlConnection();
                    instance.ConnectionString =
                      "server=sql7.freesqldatabase.com;" +
                      "database=sql7353524;" +
                      "port=3306;" +
                      "user=sql7353524;" +
                      "password= R7ry1S5L1Z;";
                    while (conn==true)
                    {
                        try
                        {
                            instance.Open();
                            conn = false;
                        }
                        catch (Exception ex)
                        {
                            instance.Close();
                            
                        }
                    }
                }
                return instance;
            }
        }
    }
}
