//using Admin_wcf.Classi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using Admin_wcf.Classi;

namespace Admin_wcf
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "Server_Admin" nel codice e nel file di configurazione contemporaneamente.
    [ServiceBehavior]
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
                Console.WriteLine("[OK] Registrazione Associazione avvenuta con successo!!");
            }
            catch(Exception ex)
            {
                Console.WriteLine("[ERROR]"+ ex.Message);
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
                        Console.WriteLine("[OK] Login  Associazione avvenuto con successo!!");
                        return a;
                    }
                }
                if (a == null)
                {
                    Console.WriteLine("[WARNING] Credenziali login associazione errate!!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
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
                    Console.WriteLine("[OK] Profilo Associazione restituito con successo!!");
                    return a;
                }
            }
            }
            catch(Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }
            return a;
            
        }

      

        public bool UpdatePassword(int id, string new_password)
        {
            var wcfclient = server_conn.getInstance();
            string set = "password='" + new_password+"'";
            string condizione = "IdAss=" + id.ToString();
            try
            {
                bool r= wcfclient.DBupdate("ASSOCIAZIONE", set, condizione);
                Console.WriteLine("[OK] Password Associazione aggiornata con successo!");
                return r;
            }
            catch(Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;
            }
        }

        public bool UpdateProfile(Associazione a)
        {
            /* N.B. è possibile modificare tutti i campi tranne la password e l'id*/
            var wcfclient = server_conn.getInstance();
            string condizione = "IdAss=" + a.IdAss;
            string set = "nome='" + a.nome + "', citta='" + a.citta + "', stato='" + a.stato + "', via='" + a.via + "',tel='" + a.tel + "', email='" + a.email + "'";
            try
            {
                bool r= wcfclient.DBupdate("ASSOCIAZIONE", set, condizione);
                Console.WriteLine("[OK] Profilo Associazione aggiornato con successo!!");
                return r;
            }
            catch(Exception ex)
            {
                Console.WriteLine("[ERROR]"+ ex.Message);
                throw;
            }
        }
        public bool Create_events(Evento e)
        {
            /* N.B. è possibile modificare tutti i campi tranne la password e l'id*/
            //var wcfclient = new DBManager.DBManagerClient(); //mi connetto al server
            var wcfclient = server_conn.getInstance();
            string valori = "" + e.IdEv + ",'" + e.nome + "','" + e.tipologia + "', '" + e.min_p + "','" + e.max_p + "','" + e.min_v + "','" + e.max_v + "','" + e.costo + "','"+ e.descrizione + "','"+ e.ass.IdAss + "'";
            try
            {
                bool risultato = wcfclient.DBinsert("EVENTO", valori, "`idev`, `nome`, `tipologia`, `min_p`, `max_p`, `min_v`, `max_v`, `costo`, `descrizione`, `idass`");
                Console.WriteLine("[OK] Evento creato con successo!!");
                return risultato;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]"+ ex.Message);
                throw;
            }
           
        }
        public List<Associazione> Show_associations(string cond="")
        {
            var wcfclient = server_conn.getInstance();
            List<Associazione> associazioni = new List<Associazione>();
            try
            {
                DataSet ass_set = wcfclient.DBselect("*", "ASSOCIAZIONE", cond);
                foreach (DataTable table in ass_set.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        Associazione a = new Associazione(Convert.ToInt32(row["IdAss"]), row["nome"].ToString(), row["citta"].ToString(), row["stato"].ToString(), row["via"].ToString(), row["tel"].ToString(), row["email"].ToString(), row["password"].ToString());
                        associazioni.Add(a);
                    }
                }
                Console.WriteLine("[OK] Lista associazioni recuparata con successo!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]"+ ex.Message);
                throw;

            }
            return associazioni;
        }
    }
    [DataContract]
    public class Associazione
    {

        public Associazione(int IdAss, string nome, string citta, string stato, string via, string tel, string email, string password)
        {
            this.IdAss = IdAss;
            this.nome = nome;
            this.citta = citta;
            this.stato = stato;
            this.via = via;
            this.tel = tel;
            this.email = email;
            this.password = password;
        }
        [DataMember]
        public int IdAss { get; set; }
        [DataMember]
        public string nome { get; set; }
        [DataMember]
        public string citta { get; set; }
        [DataMember]
        public string stato { get; set; }
        [DataMember]
        public string via { get; set; }
        [DataMember]
        public string tel { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string password { get; set; }


    }
}
