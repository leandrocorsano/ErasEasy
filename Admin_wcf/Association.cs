//=============================================================================
// Authors: Francesca Rossi, Leandro Corsano
//=============================================================================
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
    
    [ServiceBehavior]
    public class Association : IAssociation
    {
        /*
        *  PRINCIPALI FUNZIONALITA':
        *  @Visualizzazione e modifica dati personali
        *  @Creazione e visualizzazione dell'evento
        *  @Gestione ruolo volontario
        *  
        */
        public int Generate_id()
        {
           /* 
           * --------------------------------------------------------------------------------
           * Funzione che recupera l'ultimo id dell'associazione e restituisce l'id successivo
           * --------------------------------------------------------------------------------
           */
            var wcfclient = server_conn.getInstance();
            DataSet ass_set = wcfclient.DBselect("idass", "ASSOCIAZIONE", "idass>=all(select idass from ASSOCIAZIONE)");
            foreach (DataTable table in ass_set.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    int id = Convert.ToInt32(row["idass"]);
                    if (id <= 0)
                    {
                        Console.WriteLine("[OK] Id  Associazione restituito con successo!!");
                        return 1;
                    }
                    else
                    {
                        Console.WriteLine("[OK] Id  Associazione restituito con successo!!");
                        return id+1;
                    }
                   
                   
                }
            }
            return 1; /*se non ci sono righe*/
        }

        public bool Registration(Associazione a)
        {
            /* 
            * --------------------------------------------------------------------------------
            * Funzione che registra l'associazione in database
            * --------------------------------------------------------------------------------
            */
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
           /* 
           * --------------------------------------------------------------------------------
           * Funzione che controlla la presenza dell'associazione in database e ne restituisce i dati
           * --------------------------------------------------------------------------------
           */
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
                        /*conversione dei dati recuperati dal database*/
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
           /* 
           * --------------------------------------------------------------------------------
           * Funzione che restituisce il profilo di una determinata associazione
           * --------------------------------------------------------------------------------
           */
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
           /* 
           * --------------------------------------------------------------------------------
           * Funzione che aggiorna la password di una determinata associazione
           * --------------------------------------------------------------------------------
           */
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
            /* 
           * --------------------------------------------------------------------------------
           * Funzione che aggiorna i dati di una determinata associazione
           * --------------------------------------------------------------------------------
           */
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

        public bool Create_events(Svolgimento s)
        {
           /* 
           * --------------------------------------------------------------------------------
           * Funzione per l'inserimento di un nuovo evento nel database
           * --------------------------------------------------------------------------------
           */
            var wcfclient = server_conn.getInstance(); //connessione a DB_Manager
            string valori_evento = "" + s.evento.IdEv + ",'" + s.evento.nome + "','" + s.evento.tipologia + "', '" + s.evento.min_p + "','" + s.evento.max_p + "','" + s.evento.min_v + "','" + s.evento.max_v + "','" + s.evento.costo + "','"+ s.evento.descrizione + "','"+ s.evento.ass.IdAss + "'";
            string valori_luogo = "" + s.luogo.IdLuogo + ", '" + s.luogo.citta + "', '" + s.luogo.via + "','" + s.luogo.stato + "'";
            string valori_svolgim=""+s.evento.IdEv+", "+s.luogo.IdLuogo+", '"+s.ora_i+"', '"+s.ora_f+"', '"+s.data_i+"','"+s.data_f+"'";
            string insert_evento = "INSERT INTO EVENTO(`idev`, `nome`, `tipologia`, `min_p`, `max_p`, `min_v`, `max_v`, `costo`, `descrizione`, `idass`) VALUES (" + valori_evento + ")";
            string insert_luogo = "INSERT INTO LUOGO(`idluogo`, `citta`, `via`, `stato`) VALUES (" + valori_luogo + ")";
            string insert_svolgim = "INSERT INTO SVOLGIMENTO(`idev`, `idluogo`, `ora_i`, `ora_f`, `data_i`, `data_f`) VALUES (" + valori_svolgim + ")";
            List<string> query = new List<string> () { insert_evento, insert_luogo, insert_svolgim };
            
            try
            {
                bool risultato = wcfclient.DBtransaction(query);
                Console.WriteLine("[OK] Evento creato con successo!!");
                return risultato;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]"+ ex.Message);
                throw;
            }
           
        }

        public List<string> GetCitta(string cond = "")
        {
           /* 
          * --------------------------------------------------------------------------------
          * Funzione che recupera le città in cui operano le varie associazioni presenti in DB
          * --------------------------------------------------------------------------------
          */
            var wcfclient = server_conn.getInstance();
            List<string>citta = new List<string>();
            try
            {
                DataSet ass_set = wcfclient.DBselect("distinct citta", "ASSOCIAZIONE", cond);
                foreach (DataTable table in ass_set.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        string c = row["citta"].ToString();
                        citta.Add(c);
                    }
                }
                Console.WriteLine("[OK] Lista città recuparata con successo!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }
            return citta;

        }

        public List<Associazione> Show_associations(string cond="")
        {
          /* 
          * --------------------------------------------------------------------------------
          * Funzione che mostra una lista di tutte le associazioni presenti in DB
          * --------------------------------------------------------------------------------
          */
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

        public List<Svolgimento> Show_Event(int idass, string tipologia = "")
        {
            /* ---------------------------------------------------
             * Lista di Eventi che ha creato una determita associazione
             * ------------------------------------------------------*/
            string cond = "E.idass=" + idass + " and S.idev=E.idev and S.idluogo=L.idluogo and" + tipologia;
            var wcfclient = server_conn.getInstance();
            Association ass = new Association();

            List<Svolgimento> eventi = new List<Svolgimento>();
            try
            {
                DataSet eventi_set = wcfclient.DBselect("*", "EVENTO as E, SVOLGIMENTO as S, LUOGO as L", cond);
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
                Console.WriteLine("[OK] Lista eventi creati da un associazione!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }
            return eventi;

        }

        public bool Add_ruolo(int idvolont, string ruolo)
        {
         /* 
          * --------------------------------------------------------------------------------
          * Funzione per aggiungere o modificare il ruolo di un volontario
          * --------------------------------------------------------------------------------
          */
            var wcfclient = server_conn.getInstance();
            string cond = "idvolont=" + idvolont;
            string modify = "ruolo='" + ruolo + "'";

            try
            {
                bool risultato = wcfclient.DBupdate("VOLONTARIO", modify, cond);
                Console.WriteLine("[OK] Volontario aggiunto con successo!!");
                return risultato;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;
            }
        }

    }

   

    
}
