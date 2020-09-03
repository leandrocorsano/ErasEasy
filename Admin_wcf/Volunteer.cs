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
                Console.WriteLine("[OK] Registrazione volontario avvenuta con successo");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]"+ ex.Message);
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
                        Console.WriteLine("[OK] Login volontario avvenuto con successo!!");
                        return v;
                    }
                }
                if (v == null)
                {
                    Console.WriteLine("[WARNING] Credenziali login volntario errate!!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]"+ ex.Message);
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
                        Console.WriteLine("[OK] Profilo Associazione restituito con successo!!");
                        return v;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }
            return v;

        }



        public bool UpdatePassword(int id, string new_password)
        {
            var wcfclient = server_conn.getInstance();
            string set = "password='" + new_password + "'";
            string condizione = "idvolont=" + id.ToString();

            try
            {
                bool r= wcfclient.DBupdate("VOLONTARIO", set, condizione);
                Console.WriteLine("[OK] Password volontario aggiornata con successo!!");
                return r;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }
        }

        public bool UpdateProfile(Volontario v)
        {
            /* N.B. è possibile modificare tutti i campi tranne la password e l'id*/
            //var wcfclient = new DBManager.DBManagerClient(); //mi connetto al server
            var wcfclient = server_conn.getInstance();
            string condizione = "idvolont=" + v.IdVolont;
            string set = "nome='" + v.nome + "', cognome='" + v.cognome + "', email='" + v.email + "',tel='" + v.telefono + "',ruolo='" + v.ruolo + "',data_iscrizione='" + v.data_iscr + "'";
            try
            {
                bool r= wcfclient.DBupdate("VOLONTARIO", set, condizione);
                Console.WriteLine("[OK] Profilo volontario aggiornato con successo!!");
                return r;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }
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
                        Console.WriteLine("[OK] Associazione de volontario restituita con successo!!");
                        return a;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
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
                Console.WriteLine("[OK] Lista volontari restituita con successo!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
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
                risultato = wcfclient.DBinsert("GESTIONE", valori, "`idvolont`, `idev`");
                Console.WriteLine("[OK] Prenotazione da parte del volontario avvenuta con successo!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }
            return risultato;
        }
        public List<Svolgimento> Show_Event(int idvol, string tipologia = "")
        {
            /* ---------------------------------------------------
             * Eventi che ha gestisto/gestirà un volontario
             * ------------------------------------------------------*/
            string cond = "E.idev=G.idev and G.idvolont=" + idvol + " and S.idev=E.idev and S.idluogo=L.idluogo and" + tipologia;
            var wcfclient = server_conn.getInstance();
            Association ass = new Association();

            List<Svolgimento> eventi = new List<Svolgimento>();
            try
            {
                DataSet eventi_set = wcfclient.DBselect("*", "GESTIONE as G, EVENTO as E, SVOLGIMENTO as S, LUOGO as L", cond);
                foreach (DataTable table in eventi_set.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        Associazione a = ass.Profile(Convert.ToInt32(row["idass"]));
                        Evento e = new Evento(Convert.ToInt32(row["idev"]), row["nome"].ToString(), row["tipologia"].ToString(), Convert.ToInt32(row["min_p"]), Convert.ToInt32(row["max_p"]), Convert.ToInt32(row["min_v"]), Convert.ToInt32(row["max_v"]), Convert.ToInt32(row["costo"]), row["descrizione"].ToString(), a);
                        Luogo l = new Luogo(Convert.ToInt32(row["idluogo"]), row["citta"].ToString(), row["via"].ToString(), row["stato"].ToString());
                        Svolgimento s = new Svolgimento(e, l, row["ora_i"].ToString(), row["ora_f"].ToString(), row["data_i"].ToString(), row["data_f"].ToString());
                        eventi.Add(s);
                    }
                }
                Console.WriteLine("[OK] Lista eventi gestiti da un volontario!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }
            return eventi;

        }
        public bool CancelBooking(int volontario, int evento)
        {
            /* ---------------------------------------------------
            * Funzione che permette di disdire un evento
            * ------------------------------------------------------*/

            var wcfclient = server_conn.getInstance();
            string valori = "idvolont=" + volontario + " and idev=" + evento;
            bool risultato;
            try
            {
                risultato = wcfclient.DBdelete("GESTIONE", valori);
                Console.WriteLine("[OK] Cancellazione della  prenotazione da parte del volontario avvenuta con successo!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
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

