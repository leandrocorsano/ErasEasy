using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;





namespace Admin_wcf
{
    /* classe singleton che permette la connesione con il DBmanager*/
    class server_conn
    {
        private static DBManager.DBManagerClient instance = null;

        private static readonly object padlock = new object();

        private server_conn() { }

        public static DBManager.DBManagerClient getInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new DBManager.DBManagerClient();
                    
                }
                return instance;
            }
        }
    }
}
