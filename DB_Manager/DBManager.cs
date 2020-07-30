using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DB_Manager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "DBManager" nel codice e nel file di configurazione contemporaneamente.
    public class DBManager : IDBManager
    {
        //private MySqlConnection connessione;

        //public DBManager()
        //{
        //    this.connessione = this.DBconnection();
        //}
        //public MySqlConnection DBconnection()
        //{

        //    //string connectionString = "Server=tcp:progett.database.windows.net,1433;Initial Catalog=DB_Ereasylife;Persist Security Info=False;User ID=francesca;Password=Borsa.cometa99;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        //    var builder = new MySqlConnectionStringBuilder
        //    {
        //        Server = "sql7.freesqldatabase.com",
        //        Database = "sql7353524",
        //        UserID = "sql7353524",
        //        Password = "R7ry1S5L1Z",
        //        //SslMode = MySqlSslMode.Required,
        //    };
        //    using (var conn = new MySqlConnection(builder.ConnectionString))
        //    {
        //        Console.WriteLine("Opening connection");
        //        //await conn.OpenAsync();
        //        try
        //        {
        //            conn.Open();
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //        }
        //        Console.WriteLine("State: {0}", conn.State);
        //        Console.WriteLine("ConnectionString: {0}",
        //            conn.ConnectionString);
        //        /*using (var command = conn.CreateCommand())
        //        {
        //            command.CommandText = @"INSERT INTO Studente (IdStud, Nome, Cognome, Data_n) VALUES (@id, @nome, @cognome, @data)";
        //            command.Parameters.AddWithValue("@id", 1);
        //            command.Parameters.AddWithValue("@nome", "Prova");
        //            command.Parameters.AddWithValue("@cognome", "Pippo");
        //            command.Parameters.AddWithValue("@data", "1999-10-20");
        //            try
        //            {
        //                command.ExecuteNonQuery();
        //            }
        //            catch (Exception ex1)
        //            {
        //                Console.WriteLine(ex1.Message);
        //            }
                    
        //            //Console.WriteLine(String.Format("Number of rows inserted={0}", rowCount));
        //        }*/



        //        return conn;
        //        }
        //    }

                public bool DBdelete(string table, string condition)
                {
                   using (var command = DB_conn.getInstance().CreateCommand())
                    {
                    command.CommandText = "DELETE FROM " + table + " WHERE " + condition + " ;";
                    Console.WriteLine(command.CommandText);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex1)
                    {
                        Console.WriteLine(ex1.Message);
                        throw;
                    }
                    return true; //rimozione andata a buon fine


                

                    }
                }
                
        public bool DBinsert(string table, string values, string field = "")
        {
            using (var command = DB_conn.getInstance().CreateCommand())
            {
                command.CommandText = "INSERT INTO " + table + " (" + field + ")  VALUES (" + values + ");";
                Console.WriteLine(command.CommandText);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex1)
                {
                    Console.WriteLine(ex1.Message);
                    throw;
                }
                return true; //inserimento andato a buon fine


                

            }
        }

        public MySqlDataReader DBselect( string campi, string tabella, string condizione="")
        {
                    /*funzione che implementa in maniera astratta una query di selezione nel DB 18/06*/
            
                    using (var command = DB_conn.getInstance().CreateCommand())
                    {
                        MySqlDataReader risultato;
                        if (condizione == "")
                        {
                            command.CommandText = "SELECT " + campi + " FROM " + tabella;
                        }
                        else
                        {
                            command.CommandText = "SELECT " + campi + " FROM " + tabella + " WHERE " + condizione;
                        }
                        Console.WriteLine(command.CommandText);
                        try
                        {
                             risultato = command.ExecuteReader();
                        
         
                        }
                        catch (Exception ex1)
                        {
                            Console.WriteLine(ex1.Message);
                            throw;
                        }
                        return risultato;


                

                    }
        }   
            
            
            
  

               public bool DBupdate(string table, string setter, string condition)
               {
                    using (var command = DB_conn.getInstance().CreateCommand())
                    {
                    command.CommandText = "UPDATE " + table + " SET " + setter + " WHERE " + condition;
                        Console.WriteLine(command.CommandText);
                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex1)
                        {
                            Console.WriteLine(ex1.Message);
                            throw;
                        }
                        return true; //aggiornamento andato a buon fine

                    }
               }    
            
            
                    
                

    }
}
