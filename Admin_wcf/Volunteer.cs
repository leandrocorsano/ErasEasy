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
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "Service1" nel codice e nel file di configurazione contemporaneamente.
    public class Volunteer : IVolunteer
    {
        public bool Registration(Volontario v)
        {
            var wcfclient = server_conn.getInstance();
            string valori = "" + v.IdVolont + ",'" + v.nome + "','" + v.cognome + "','" + v.data_n + "','" + v.email + "','" + v.telefono + "','" + v.data_iscr + "','" + v.password + "','" + v.ruolo + "','" + v.ass.IdAss+"'";
            bool risultato;
            try
            {
                risultato = wcfclient.DBinsert("VOLONTARIO", valori, "`idvolont`, `nome`, `cognome`, `data_n`, `email`, `tel`, `data_iscrizione`, `password`, `ruolo`, `idass`");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return risultato;
        }

        public Volontario Login(string email, string password)
        {
            var wcfclient = server_conn.getInstance();
            string cond = "email='" + email + "' and password='" + password + "'";
            Volontario v = null;
            try
            {
                DataSet ass_set = wcfclient.DBselect("*", "VOLONTARIO", cond);
                Association ass = new Association();
                foreach (DataTable table in ass_set.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        
                        Associazione a=ass.Profile(Convert.ToInt32(row["idass"])); //recupero l'associazione corrispondente al volontario
                        v = new Volontario(Convert.ToInt32(row["idvolont"]), row["nome"].ToString(), row["cognome"].ToString(), row["data_n"].ToString(), row["email"].ToString(), row["tel"].ToString(), row["data_iscrizione"].ToString(), row["password"].ToString(), a, row["ruolo"].ToString());
                        return v;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;

            }
            return v;

        }


        public Volontario Profile(int id)
        {
            var wcfclient = server_conn.getInstance();
            string cond = "idvolont=" + id.ToString();
            Volontario v = null;
            try
            {
                DataSet ass_set = wcfclient.DBselect("*", "VOLONTARIO", cond);
                Association ass = new Association();
                foreach (DataTable table in ass_set.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {

                        Associazione a = ass.Profile(Convert.ToInt32(row["idass"])); //recupero l'associazione corrispondente al volontario
                        v = new Volontario(Convert.ToInt32(row["idvolont"]), row["nome"].ToString(), row["cognome"].ToString(), row["data_n"].ToString(), row["email"].ToString(), row["tel"].ToString(), row["data_iscrizione"].ToString(), row["password"].ToString(), a, row["ruolo"].ToString());
                        return v;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;

            }
            return v;

        }



        public bool UpdatePassword(int id, string new_password)
        {
            var wcfclient = server_conn.getInstance();
            string set = "password='" + new_password + "'";
            string condizione = "idvolont=" + id.ToString();

            return wcfclient.DBupdate("VOLONTARIO", set, condizione);
        }

        public bool UpdateProfile(Volontario v)
        {
            /* N.B. è possibile modificare tutti i campi tranne la password e l'id*/
            //var wcfclient = new DBManager.DBManagerClient(); //mi connetto al server
            var wcfclient = server_conn.getInstance();
            string condizione = "idvolont=" + v.IdVolont;
            string set = "nome='" + v.nome + "', cognome='" + v.cognome + "', email='" + v.email + "',tel='" + v.telefono + "',ruolo='" + v.ruolo + "',data_iscrizione='" + v.data_iscr + "'";
            return wcfclient.DBupdate("VOLONTARIO", set, condizione);
        }
    }

}

