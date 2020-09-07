//=============================================================================
// Authors: Francesca Rossi, Leandro Corsano
//=============================================================================
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
    public class Volunteer : IVolunteer
    {
        /*
       *  PRINCIPALI FUNZIONALITA':
       *  @Visualizzazione e modifica dati personali
       *  @Partecipazione e disdetta a eventi e riunioni
       *  
       *  
       */
        public int Generate_id()
        {
           /* 
          * --------------------------------------------------------------------------------
          * Funzione che recupera l'ultimo id del volontario e restituisce l'id successivo
          * --------------------------------------------------------------------------------
          */
            var wcfclient = server_conn.getInstance();
            DataSet stud_set = wcfclient.DBselect("idvolont", "VOLONTARIO", "idvolont>=all(select idvolont from VOLONTARIO)");
            foreach (DataTable table in stud_set.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    int id = Convert.ToInt32(row["idvolont"]);
                    if (id <= 0)
                    {
                        Console.WriteLine("[OK] Id volontario restituito con successo!!");
                        return 1;
                    }
                    else
                    {
                        Console.WriteLine("[OK] Id volontario restituito con successo!!");
                        return id + 1;
                    }


                }
            }
            return 1; /*se non ci sono righe*/
        }

        public bool Registration(Volontario v)
        {
          /* 
          * --------------------------------------------------------------------------------
          * Funzione che inserisce un nuovo volontario in database
          * --------------------------------------------------------------------------------
          */
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
          /* 
          * --------------------------------------------------------------------------------
          * Funzione che controlla che un utente sia presente in db e se presente
          * ne restituisce i suoi dati
          * --------------------------------------------------------------------------------
          */
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
          /* 
          * --------------------------------------------------------------------------------
          * Funzione che restituisce i dati personali di un determinato volontario
          * --------------------------------------------------------------------------------
          */
            var wcfclient = server_conn.getInstance();
            string cond = "idvolont=" + id.ToString();
            Volontario v = null;
            try
            {
                DataSet ass_set = wcfclient.DBselect("*", "VOLONTARIO", cond);
                Association ass = new Association(); //Creo un oggetto association che mi permette di recuperare i dati dell'associazione del volontario
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
          /* 
          * --------------------------------------------------------------------------------
          * Funzione che aggiorna la password di un determinato volontario
          * --------------------------------------------------------------------------------
          */
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
          /* 
          * --------------------------------------------------------------------------------
          * Funzione che aggiorna i dati personali di un volontario
          * --------------------------------------------------------------------------------
          */
            /* N.B. è possibile modificare tutti i campi tranne la password e l'id*/
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
        { /* 
           * --------------------------------------------------------------------------------
           * Funzione che recupera un determinata associazione dato l'id del volontario
           * --------------------------------------------------------------------------------
           */
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
          /* 
          * --------------------------------------------------------------------------------
          * Funzione che recupera la lista dei valontari data una determinata condizione
          * --------------------------------------------------------------------------------
          */
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
           /* 
          * --------------------------------------------------------------------------------
          * Funzione che permette la prenotazione di un evento o di una riunione
          * --------------------------------------------------------------------------------
          */
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
             * ------------------------------------------------------
             */
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
            * ------------------------------------------------------
            */

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

}

