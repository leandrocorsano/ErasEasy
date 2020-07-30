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
            DBManager prova = new DBManager();
            
            try
            {
                ServiceHost DBmanager = new ServiceHost(typeof(DBManager));
                DBmanager.Open();
                Console.WriteLine("Server database attivo, premi un tasto per interrompere");
               //bool result=prova.DBinsert("Studente","'pippo', 'paperino', '1999-05-24' ","Nome, Cognome, Data_n");
                 MySqlDataReader myReader= prova.DBselect("*","Studente");
                while (myReader.Read())
                    {
                        Console.WriteLine(myReader.GetString(0)+" "+myReader.GetString(1) );
                    }
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
