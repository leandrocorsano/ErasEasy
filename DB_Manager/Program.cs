using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Data.SqlClient;

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
                //prova.DBconnection();
                try
                {
                    bool inserisci = prova.DBinsert("Studente", "(2,'Francesca','Rossi', '1900-05-20')", "(`IdStud`, `Nome`, `Cognome`, `Data_n`)");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                    //SqlDataReader p = prova.DBselect("nome", "Prova");
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
