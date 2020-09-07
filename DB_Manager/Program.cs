//=============================================================================
// Authors: Francesca Rossi, Leandro Corsano
//=============================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace DB_Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            
            try
            {
                /*Creo ed avvio il server del database*/
                ServiceHost DBmanager = new ServiceHost(typeof(DBManager));
                DBmanager.Open();
                Console.WriteLine("Server database attivo, premi un tasto per interrompere");
                Console.ReadLine();
                DBmanager.Close();
                Console.WriteLine("Server database chiuso");
                }
            catch(Exception ex)
            {
                Console.WriteLine("Errore" + ex.ToString());
            }
        }
    }
}
