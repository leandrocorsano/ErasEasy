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
                //prova.DBconnection();
                /*try
                {
                    string s1= "INSERT INTO `Studente`(`IdStud`, `Nome`, `Cognome`, `Data_n`) VALUES (3, 'Giacomo','Neri','2002-04-10')";
                    string s2 = "INSERT INTO `Studente`(`IdStud`, `Nome`, `Cognome`, `Data_n`) VALUES (4, 'Giorgia','Bianchi','1992-04-10')";
                    List<string> query= new List<string>();
                    query.Add(s1);
                    query.Add(s2);//
                    var transazione = prova.DBtransaction(query);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }*/
                /*try
                {
                    //bool inserisci = prova.DBinsert("Studente", "(2, 'Marco','Rossi','2000-03-20')", "(`IdStud`, `Nome`, `Cognome`, `Data_n`)");
                    MySqlDataReader selezione = prova.DBselect("*", "Studente", "Cognome='Rossi'");
                    while (selezione.Read())
                    {
                        Console.WriteLine(string.Format(
                            "Reading from table=({0}, {1}, {2})",
                            selezione.GetInt32(0),
                            selezione.GetString(1),
                            selezione.GetString(2),
                            selezione.GetString(3)));
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }*/
                

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
