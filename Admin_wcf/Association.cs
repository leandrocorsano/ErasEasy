using Admin_wcf.Classi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace Admin_wcf
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "Server_Admin" nel codice e nel file di configurazione contemporaneamente.

    public class Association : IAssociation
    {
        public bool Registration(Associazione a)
        {
            var wcfclient = server_conn.getInstance();
            string valori = "" + a.IdAss + ",'" + a.nome + "','" + a.citta + "', '" + a.stato + "','" + a.via + "','" + a.tel + "','" + a.email + "','" + a.password + "'";
            bool risultato;
            try
            {
                risultato=wcfclient.DBinsert("ASSOCIAZIONE",valori,"`idass`, `nome`, `citta`, `stato`, `via`, `tel`, `email`, `password`");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return risultato;
        }

        public Associazione Login(string email, string password)
        {
            var wcfclient = server_conn.getInstance();
            string cond = "email='" + email+ "' and password='"+password+"'";
            Associazione a = null;
            try
            {
                DataSet ass_set = wcfclient.DBselect("*", "ASSOCIAZIONE", cond);
                foreach (DataTable table in ass_set.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        a = new Associazione(Convert.ToInt32(row["IdAss"]), row["nome"].ToString(), row["citta"].ToString(), row["stato"].ToString(), row["via"].ToString(), row["tel"].ToString(), row["email"].ToString(), row["password"].ToString());
                        return a;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;

            }
            return a;

        }

   
        public Associazione Profile(int id)
        {
            var wcfclient = server_conn.getInstance();
            string cond = "IdAss=" + id.ToString();
            Associazione a = null;
            try { 
            DataSet ass_set = wcfclient.DBselect("*", "ASSOCIAZIONE", cond); 
            foreach (DataTable table in ass_set.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    a = new Associazione(Convert.ToInt32(row["IdAss"]), row["nome"].ToString(), row["citta"].ToString(), row["stato"].ToString(), row["via"].ToString(), row["tel"].ToString(), row["email"].ToString(), row["password"].ToString());
                    return a;
                    /* foreach (DataColumn column in table.Columns)
                    {
                        string item = row[column].ToString();
                        Console.WriteLine(item);
                        Console.WriteLine();
                        // read column and item
                    }*/
                }
            }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;

            }
            return a;
            
        }

      

        public bool UpdatePassword(int id, string new_password)
        {
            var wcfclient = server_conn.getInstance();
            string set = "password='" + new_password+"'";
            string condizione = "IdAss=" + id.ToString();

            return wcfclient.DBupdate("ASSOCIAZIONE", set, condizione);
        }

        public bool UpdateProfile(Associazione a)
        {
            /* N.B. è possibile modificare tutti i campi tranne la password e l'id*/
            //var wcfclient = new DBManager.DBManagerClient(); //mi connetto al server
            var wcfclient = server_conn.getInstance();
            string condizione = "IdAss=" + a.IdAss;
            string set = "nome='" + a.nome + "', citta='" + a.citta + "', stato='" + a.stato + "', via='" + a.via + "',tel='" + a.tel + "', email='" + a.email + "'";
            return wcfclient.DBupdate("ASSOCIAZIONE", set, condizione);
        }
    }
}
