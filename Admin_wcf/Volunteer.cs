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
                Console.WriteLine("problema con la registrazione");
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
        public Associazione GetAssociazione(int id)
        {
            var wcfclient = server_conn.getInstance();
            string cond = "idass=" + id.ToString();
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
        public List<Volontario> Show_volontari(string cond = "")
        {
            var wcfclient = server_conn.getInstance();
            List<Volontario> volontari = new List<Volontario>();
            try
            {
                DataSet vol_set = wcfclient.DBselect("*", "VOLONTARIO", cond);
                Association ass = new Association();
                foreach (DataTable table in vol_set.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        Associazione a = ass.Profile(Convert.ToInt32(row["idass"])); //recupero l'associazione corrispondente al volontario
                        Volontario v = new Volontario(Convert.ToInt32(row["idvolont"]), row["nome"].ToString(), row["cognome"].ToString(), row["data_n"].ToString(), row["email"].ToString(), row["tel"].ToString(), row["data_iscrizione"].ToString(), row["password"].ToString(), a, row["ruolo"].ToString());
                        
                        volontari.Add(v);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;

            }
            return volontari;
        }
        public bool BookEvent(int volontario, int evento)
        {
            var wcfclient = server_conn.getInstance();
            string valori = "" + volontario + "," + evento + "";
            bool risultato;
            try
            {
                risultato = wcfclient.DBinsert("PARTECIPAZIONE", valori, "`idstud`, `idev`");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return risultato;
        }
    }
    
    [DataContract]
    public class Volontario
    {
        public Volontario(int IdVolont, string nome, string cognome, string data_n, string email, string telefono, string data_iscr, string password, Associazione ass, string ruolo = "")
        {
            this.IdVolont = IdVolont;
            this.nome = nome;
            this.cognome = cognome;
            this.data_n = data_n;
            this.email = email;
            this.telefono = telefono;
            this.data_iscr = data_iscr;
            this.password = password;
            this.ruolo = ruolo;
            this.ass = ass; //associazione

        }
        [DataMember]
        public int IdVolont { get; set; }
        [DataMember]
        public string nome { get; set; }
        [DataMember]
        public string cognome { get; set; }
        [DataMember]
        public string data_n { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string data_iscr { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public string ruolo { get; set; }
        [DataMember]
        public Associazione ass { get; set; }

    }

}

