//=============================================================================
// Authors: Francesca Rossi, Leandro Corsano
//=============================================================================
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace DB_Manager
{
    public class DBManager : IDBManager
    {

        public bool DBdelete(string table, string condition)
                {
                  /* 
                 * --------------------------------------------------------------------------------
                 * Funzione che genera ed esegue una query di delete in database
                 * --------------------------------------------------------------------------------
                 */
            using (var command = DB_conn.getInstance().CreateCommand())
                    {
                    if (condition != "")
                    {
                        command.CommandText = "DELETE FROM " + table + " WHERE " + condition + " ;";
                    }
                    else
                    {
                    command.CommandText = "DELETE FROM " + table +  " ;";

                    }
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
                /* 
                 * --------------------------------------------------------------------------------
                 * Funzione che genera ed esegue una query di insert in database
                 * --------------------------------------------------------------------------------
                 */
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

        public DataSet DBselect( string campi, string tabella, string condizione="")
        {
            /* 
            * --------------------------------------------------------------------------------
            * Funzione che genera ed esegue una query di select in database
            * --------------------------------------------------------------------------------
            */
            DataSet ds1 = new DataSet();
            using (var command = DB_conn.getInstance().CreateCommand())
                    {
                        
                        if (condizione == "")
                        {
                            command.CommandText = "SELECT " + campi + " FROM " + tabella;
                        }
                        else
                        {
                            command.CommandText = "SELECT " + campi + " FROM " + tabella + " WHERE " + condizione;
                        }
                        Console.WriteLine(command.CommandText);

                         MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        adapter.Fill(ds1, "Tabella");
                        return ds1;


                

                    }
        }   

        public bool DBupdate(string table, string setter, string condition)
        {
            /* 
            * --------------------------------------------------------------------------------
            * Funzione che genera ed esegue una query di update in database
            * --------------------------------------------------------------------------------
            */
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
            
        public bool DBtransaction(List<string> query)
         {
            /* 
            * --------------------------------------------------------------------------------
            * Funzione che genera  una transazione in database
            * --------------------------------------------------------------------------------
            */
            using (var command = DB_conn.getInstance().CreateCommand())
                    {
                        MySqlTransaction transazione;
                        transazione=DB_conn.getInstance().BeginTransaction();
                        command.Transaction=transazione;
                        int l=query.Count();
                        try
                        {
                            for( int i=0; i<l; i++)
                            {
                              command.CommandText=query[i];
                              
                              command.ExecuteNonQuery();

                            }
                            transazione.Commit();
                          
                            Console.WriteLine("La transazione è andata a buon fine");

                        }
                        catch(Exception e)
                        {
                         Console.WriteLine(e.Message);
                            transazione.Rollback();
                            throw;
                        }
                    return true; //transazione andata a buon fine    

                    }
               } 
                    
                

    }
}
