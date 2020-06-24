using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DB_Manager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "DBManager" nel codice e nel file di configurazione contemporaneamente.
    public class DBManager : IDBManager
    {
        public SqlConnection DBconnection()
        {
            //https://docs.microsoft.com/it-it/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring?view=dotnet-plat-ext-3.1
            string connectionString = "Server=tcp:progett.database.windows.net,1433;Initial Catalog=DB_Ereasylife;Persist Security Info=False;User ID=francesca;Password=Borsa.cometa99;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                try
                {
                    connection.Open();
                }
                catch( Exception ex)
                {
                    Console.WriteLine( ex.Message);
                }
                Console.WriteLine("State: {0}", connection.State);
                Console.WriteLine("ConnectionString: {0}",
                    connection.ConnectionString);
                return connection;
            }
        }

        public bool DBdelate(string table, string condition)
        {
            using (SqlCommand command = this.DBconnection().CreateCommand())
            {

                command.CommandText = "DELATE FROM " + table + "WHERE " + condition + " ;";
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;//riporto l'errore 

                }

                return true; //Cancellazione andata a buon fine

            }
        }

        public bool DBinsert(string table,  string values, string field="" )
        {
            using (SqlCommand command = this.DBconnection().CreateCommand())
            {

                command.CommandText = "INSERT INTO " + table + field + " VALUES " + values + ";";
                //Console.WriteLine(command.CommandText);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;//riporto l'errore 
                    //return false; //inserimento non fatto
                    
                   
                }
                
                return true; //inserimento andato a buon fine

            }
        }

        public SqlDataReader DBselect( string campi, string tabella, string condizione="")
        {
            /*funzione che implementa in maniera astratta una query di selezione nel DB 18/06*/
            SqlCommand command = new SqlCommand();
            //conn.Open();
            using ( command.Connection=this.DBconnection())
            {

                if (condizione == "")
                {
                    command.CommandText = "SELECT " + campi + " FROM " + tabella;
                }
                else
                {
                    command.CommandText = "SELECT " + campi + " FROM " + tabella + " WHERE " + condizione;
                }
                SqlDataReader risultato = command.ExecuteReader();
                return risultato;

            }
        }

        public bool DBupdate(string table, string setter, string condition)
        {
            using (SqlCommand command = this.DBconnection().CreateCommand())
            {

                command.CommandText = "UPDATE " + table + " SET " + setter + " WHERE " + condition;
                //Console.WriteLine(command.CommandText);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;//riporto l'errore 
                          //return false; //inserimento non fatto


                }

                return true; //aggiornamneto andato a buon fine

            }
        }
    }
}
