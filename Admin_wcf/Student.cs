//=============================================================================
// Authors: Francesca Rossi, Leandro Corsano
//=============================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Admin_wcf.Classi;
using MySql.Data.MySqlClient;
using System.Data;


namespace Admin_wcf
{
    public class Student : IStudent
    {
        /*
       *  PRINCIPALI FUNZIONALITA':
       *  @Visualizzazione e modifica dati personali
       *  @Visualizzazione, Partecipazione e disdetta di un evento
       *  @Registrazione e vista delle università
       *  @Gestione della community degli studenti
       *  
       */
        public int Generate_id()
        {
            /* 
           * --------------------------------------------------------------------------------
           * Funzione che recupera l'ultimo id dello studente e restituisce l'id successivo
           * --------------------------------------------------------------------------------
           */
            var wcfclient = server_conn.getInstance();
            DataSet stud_set = wcfclient.DBselect("idstud", "STUDENTE", "idstud>=all(select idstud from STUDENTE)");
            foreach (DataTable table in stud_set.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    int id = Convert.ToInt32(row["idstud"]);
                    if (id <= 0)
                    {
                        Console.WriteLine("[OK] Id  studente restituito con successo!!");
                        return 1;
                    }
                    else
                    {
                        Console.WriteLine("[OK] Id  studente restituito con successo!!");
                        return id + 1;
                    }


                }
            }
            return 1; /*se non ci sono righe*/
        }

        public int Generate_id_universita()
        {
            /* 
           * --------------------------------------------------------------------------------
           * Funzione che recupera l'ultimo id dell'universita e restituisce l'id successivo
           * --------------------------------------------------------------------------------
           */
            var wcfclient = server_conn.getInstance();
            DataSet stud_set = wcfclient.DBselect("iduni", "UNIVERSITA", "iduni>=all(select iduni from UNIVERSITA)");
            foreach (DataTable table in stud_set.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    int id = Convert.ToInt32(row["iduni"]);
                    if (id <= 0)
                    {
                        Console.WriteLine("[OK] Id  universita restituito con successo!!");
                        return 1;
                    }
                    else
                    {
                        Console.WriteLine("[OK] Id  universita restituito con successo!!");
                        return id + 1;
                    }


                }
            }
            return 1; /*se non ci sono righe*/
        }

        public bool University_Registration(Frequentazione f)
        {
            /* ---------------------------------------------------
            * Funzione che permette di registrare un'università
            * ------------------------------------------------------
            */
            var wcfclient = server_conn.getInstance();
            string valori_uni = "" + f.universita.IdUni.ToString() + ", '" + f.universita.nome + "','" + f.universita.citta + "', '" + f.universita.stato + "'";
            string valori_freq = "'" + f.tipo + "', " + f.studente.IdStud.ToString() + ", " + f.universita.IdUni.ToString();
            string query_uni = "INSERT INTO `UNIVERSITA`(`iduni`, `nome`, `citta`, `stato`) VALUES ( " + valori_uni + " )";
            string query_freq = "INSERT INTO `FREQUENTAZIONE`(`tipo`, `idstud`, `iduni`) VALUES ( " + valori_freq + " )";
            Console.WriteLine(query_uni);
            Console.WriteLine(query_freq);
            List<string> queries = new List<string>() { query_uni, query_freq };
            try
            {
                bool risultato = wcfclient.DBtransaction(queries);
                Console.WriteLine("[OK] Registrazione università avvenuta con successo");
                return risultato;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }

        }

        public List<Frequentazione> GetUniversity(Studente s)
        {
            /* ---------------------------------------------------
            * Funzione che permette di recuperare le università dello studente
            * ------------------------------------------------------
            */

            var wcfclient = server_conn.getInstance();
            string cond = "F.idstud=S.idstud and F.iduni=U.iduni and S.idstud=" + s.IdStud.ToString();
            List<Frequentazione> univesita = new List<Frequentazione>();
            try
            {
                DataSet uni_set = wcfclient.DBselect("U.iduni, S.idstud, U.nome as nomeuni, S.nome as nomestud, U.citta as unicitta, U.stato as unistato, S.cognome, S.cellulare, S.email, S.data_n,  F.tipo, S.citta as studcitta, S.stato as studstato, S.nazionalita, S.password, S.instagram, S.facebook", "FREQUENTAZIONE as F, STUDENTE as S, UNIVERSITA as U", cond);
                foreach (DataTable table in uni_set.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        Universita u = new Universita(Convert.ToInt32(row["iduni"]), row["nomeuni"].ToString(), row["unicitta"].ToString(), row["unistato"].ToString());
                        Studente stud = new Studente(Convert.ToInt32(row["IdStud"]), row["nomestud"].ToString(), row["cognome"].ToString(), row["email"].ToString(), row["cellulare"].ToString(), row["data_n"].ToString(), row["studcitta"].ToString(), row["studstato"].ToString(), row["nazionalita"].ToString(), row["password"].ToString(), row["instagram"].ToString(), row["facebook"].ToString());
                        Frequentazione f = new Frequentazione(stud, u, row["tipo"].ToString());
                        univesita.Add(f);
                    }
                }
                Console.WriteLine("[OK] Lista universita' restitutita con successo!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }
            return univesita;

        }

        public bool Registration(Studente s)
        {
            /* 
           * --------------------------------------------------------------------------------
           * Funzione inserisce lo studente in database
           * --------------------------------------------------------------------------------
           */
            var wcfclient = server_conn.getInstance();
            string valori = "" + s.IdStud + ",'" + s.nome + "','" + s.cognome + "', '" + s.email + "','" + s.tel + "','" + s.data_n + "','" + s.citta + "','" + s.stato +  "','" + s.nazionalita + "','" + s.password + "','" + s.instagram + "','" + s.facebook+"'";
            bool risultato;
            try
            {
                risultato = wcfclient.DBinsert("STUDENTE", valori, "`idstud`, `nome`, `cognome`, `email`, `cellulare`, `data_n`, `citta`, `stato`, `nazionalita`, `password`, `instagram`, `facebook`");
                Console.WriteLine("[OK] Registrazione Studente avvenuta con successo");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }
            return risultato;
        }

        public Studente Login(string email, string password)
        {
            /* 
           * --------------------------------------------------------------------------------
           * Funzione che controlla la presenza di un determinato studente in database  
           * e recupera i suoi dati se presente
           * --------------------------------------------------------------------------------
           */
            var wcfclient = server_conn.getInstance();
            string cond = "email='" + email + "' and password='" + password + "'";
            Studente s = null;
            try
            {
                DataSet ass_set = wcfclient.DBselect("*", "STUDENTE", cond);
                foreach (DataTable table in ass_set.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        s = new Studente(Convert.ToInt32(row["IdStud"]), row["nome"].ToString(), row["cognome"].ToString(), row["email"].ToString(), row["cellulare"].ToString(), row["data_n"].ToString(), row["citta"].ToString(), row["stato"].ToString(), row["nazionalita"].ToString(), row["password"].ToString(), row["instagram"].ToString(), row["facebook"].ToString());
                        Console.WriteLine("[OK] Login Studente avvenuto con succcesso!!");
                        return s;
                    }
                }
                if (s == null)
                {
                    Console.WriteLine("[WARNING] Credenziali login studente errate!!");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }
            return s;

        }

        public Studente Profile(int id)
        {
            /* 
          * --------------------------------------------------------------------------------
          * Funzione che restituisce il profilo di un determinato studente dato il suo id
          * --------------------------------------------------------------------------------
          */
            var wcfclient = server_conn.getInstance();
            string cond = "IdStud=" + id.ToString();
            Studente s = null;
            try
            {
                DataSet ass_set = wcfclient.DBselect("*", "STUDENTE", cond);
                foreach (DataTable table in ass_set.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        s = new Studente(Convert.ToInt32(row["IdStud"]), row["nome"].ToString(), row["cognome"].ToString(), row["email"].ToString(), row["cellulare"].ToString(), row["data_n"].ToString(), row["citta"].ToString(), row["stato"].ToString(), row["nazionalita"].ToString(), row["password"].ToString(), row["instagram"].ToString(), row["facebook"].ToString());
                        Console.WriteLine("[OK] Profilo Studente restituito con successo!!");
                        return s;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }
            return s;

        }

        public bool UpdatePassword(int id, string new_password)
        {
          /* 
          * --------------------------------------------------------------------------------
          * Funzione che aggiorna la password  di un determinato studente
          * --------------------------------------------------------------------------------
          */
            var wcfclient = server_conn.getInstance();
            string set = "password='" + new_password + "'";
            string condizione = "IdStud=" + id.ToString();
            try
            {
                bool r = wcfclient.DBupdate("STUDENTE", set, condizione);
                Console.WriteLine("[OK] Password studente aggiornata con successo!!");
                return r;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }
        }

        public bool UpdateProfile(Studente s)
        {
          /* 
          * --------------------------------------------------------------------------------
          * Funzione che aggiorna le informazioni relative a un determinato studente
          * --------------------------------------------------------------------------------
          */
            /* N.B. è possibile modificare tutti i campi tranne la password e l'id*/
            var wcfclient = server_conn.getInstance();
            string condizione = "IdStud=" + s.IdStud;
            string set = "nome='" + s.nome + "', cognome='" + s.cognome + "', email='" + s.email + "',cellulare='" + s.tel + "', data_n='" + s.data_n + "', citta='" + s.citta + "', stato='" + s.stato + "', nazionalita='" + s.nazionalita + "', password='" + s.password + "', instagram='" + s.instagram + "', facebook='" + s.facebook + "'";
            try
            {
                bool r = wcfclient.DBupdate("STUDENTE", set, condizione);
                Console.WriteLine("[OK] Profilo studente aggiornato con successo!!");
                return r;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }
        }

        public List<Studente> Show_students(string cond = "")
        {
            /* ---------------------------------------------------
            * Elenco di tutti gli studenti data una determinata condizione
            * ------------------------------------------------------
            */
            var wcfclient = server_conn.getInstance(); //mi connetto a DB_Manager
            List<Studente> studenti = new List<Studente>();
            try
            {
                DataSet stud_set = wcfclient.DBselect("*", "STUDENTE", cond);
                foreach (DataTable table in stud_set.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        Studente s = new Studente(Convert.ToInt32(row["IdStud"]), row["nome"].ToString(), row["cognome"].ToString(), row["email"].ToString(), row["cellulare"].ToString(), row["data_n"].ToString(), row["citta"].ToString(), row["stato"].ToString(), row["nazionalita"].ToString(), row["password"].ToString(), row["instagram"].ToString(), row["facebook"].ToString());
                        studenti.Add(s);

                    }
                }
                Console.WriteLine("[OK] Lista studenti restituita con successo!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }
            return studenti;
        }

        public bool BookEvent(int studente, int evento)
        {
            /* ---------------------------------------------------
            * Funzione che permette di prenotarsi a un evento
            * ------------------------------------------------------
            */

            var wcfclient = server_conn.getInstance();
            string valori = "" + studente + "," + evento + "";
            bool risultato;
            try
            {
                risultato = wcfclient.DBinsert("PARTECIPAZIONE", valori, "`idstud`, `idev`");
                Console.WriteLine("[OK] Prenotazione evento da parte dello studente avvenuta con successo!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }
            return risultato;
        }

        public List<Svolgimento> Show_Event(int idstud)
        {
            /* ---------------------------------------------------
             * Eventi a cui ha partecipato/parteciperà uno studente
             * ------------------------------------------------------
             */
            string cond = "E.idev=P.idev and P.idstud=" + idstud + " and S.idev=E.idev and S.idluogo=L.idluogo";
            var wcfclient = server_conn.getInstance();
            Association ass = new Association();
            
            List<Svolgimento> eventi = new List<Svolgimento>();
            try
            {
                DataSet eventi_set = wcfclient.DBselect("*", "PARTECIPAZIONE as P, EVENTO as E, SVOLGIMENTO as S, LUOGO as L", cond);
                foreach (DataTable table in eventi_set.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        Associazione a = ass.Profile(Convert.ToInt32(row["idass"]));
                        Evento e = new Evento(Convert.ToInt32(row["idev"]), row["nome"].ToString(), row["tipologia"].ToString(), Convert.ToInt32(row["min_p"]), Convert.ToInt32(row["max_p"]), Convert.ToInt32(row["min_v"]), Convert.ToInt32(row["max_v"]), Convert.ToInt32(row["costo"]), row["descrizione"].ToString(),a );
                        Luogo l = new Luogo(Convert.ToInt32(row["idluogo"]), row["citta"].ToString(), row["via"].ToString(), row["stato"].ToString());
                        Svolgimento s = new Svolgimento(e, l, row["ora_i"].ToString(), row["ora_f"].ToString(), row["data_i"].ToString(), row["data_f"].ToString());
                        eventi.Add(s);
                    }
                }
                Console.WriteLine("[OK] Lista eventi prenotati recuperata con successo!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }
            return eventi;

        }

        public bool CancelBooking(int studente, int evento)
        {
            /* ---------------------------------------------------
            * Funzione che permette di cancellare la partecipazione ad un evento
            * ----------------------------------------------------------
            */
            var wcfclient = server_conn.getInstance();
            string valori = "idstud=" + studente + " and idev=" + evento ;
            bool risultato;
            try
            {
                risultato = wcfclient.DBdelete("PARTECIPAZIONE", valori);
                Console.WriteLine("[OK] Cancellazione della  prenotazione da parte dello studente avvenuta con successo!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }
            return risultato;
        }

        public bool Friendship_Request(int stud1, int stud2)
        {
            /* ---------------------------------------------------
            * Funzione che permette di richiedere l'amicizia
            * ------------------------------------------------------
            */
            var wcfclient = server_conn.getInstance();
            string valori = "'Richiesta' , "+ stud1.ToString() + "," + stud2.ToString()+"";
            bool risultato;
            try
            {
                risultato = wcfclient.DBinsert("AMICIZIA", valori,"`Stato`, `idstud1`, `idstud2`");
                Console.WriteLine("[OK] Richiesta di amicizia avvenuta con successo!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }
            return risultato;

        }

        public bool Friendship_State(int stud1, int stud2, string state)
        {
            /* ---------------------------------------------------
            * Funzione che permette di Confermare/Annullare la richiesta d'amicizia
            * ------------------------------------------------------
            */
            var wcfclient = server_conn.getInstance();
            string cond = "idstud1=" + stud1.ToString() + " and idstud2=" + stud2.ToString() ;
            string modifica = "Stato='" + state + "'";
            bool risultato;
            try
            {
                risultato = wcfclient.DBupdate("AMICIZIA", modifica, cond);
                Console.WriteLine("[OK] Modifica stato richiesta amicizia avvenuta con successo !!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }
            return risultato;

        }

        public List<Studente> Show_Friends(Studente stud, string state)
        {
            /* ---------------------------------------------------
            * Funzione che permette di vedere l'elenco degli amici 
            * ------------------------------------------------------
            */

            var wcfclient = server_conn.getInstance();
            string cond = "A.idstud2=S.idstud and A.idstud1=" + stud.IdStud.ToString() + " and A.Stato='" +state+"'";
            List<Studente> studenti = new List<Studente>();
            try
            {
                DataSet stud_set = wcfclient.DBselect("*", "AMICIZIA as A, STUDENTE as S", cond);
                foreach (DataTable table in stud_set.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        Studente s = new Studente(Convert.ToInt32(row["IdStud"]), row["nome"].ToString(), row["cognome"].ToString(), row["email"].ToString(), row["cellulare"].ToString(), row["data_n"].ToString(), row["citta"].ToString(), row["stato"].ToString(), row["nazionalita"].ToString(), row["password"].ToString(), row["instagram"].ToString(), row["facebook"].ToString());
                        studenti.Add(s);

                    }
                }
                cond = "A.idstud1=S.idstud and A.idstud2=" + stud.IdStud.ToString() + " and A.Stato='" + state + "'";
                DataSet stud_set1 = wcfclient.DBselect("*", "AMICIZIA as A, STUDENTE as S", cond);
                foreach (DataTable table in stud_set1.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        Studente s = new Studente(Convert.ToInt32(row["IdStud"]), row["nome"].ToString(), row["cognome"].ToString(), row["email"].ToString(), row["cellulare"].ToString(), row["data_n"].ToString(), row["citta"].ToString(), row["stato"].ToString(), row["nazionalita"].ToString(), row["password"].ToString(), row["instagram"].ToString(), row["facebook"].ToString());
                        studenti.Add(s);

                    }
                }
                Console.WriteLine("[OK] Lista Amici/Richieste restitutita con successo!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }
            return studenti;

        }

        public List<Studente> My_Friendship_Request(Studente stud)
        {
            /* ---------------------------------------------------
            * Funzione che permette di vedere le richieste ricevute dallo studente loggato 
            * a cui può rispondere
            * ------------------------------------------------------
            */

            var wcfclient = server_conn.getInstance();
            string cond = "A.idstud1=S.idstud and A.idstud2=" + stud.IdStud.ToString() + " and A.Stato='Richiesta'";
            List<Studente> studenti = new List<Studente>();
            try
            {
                DataSet stud_set = wcfclient.DBselect("*", "AMICIZIA as A, STUDENTE as S", cond);
                foreach (DataTable table in stud_set.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        Studente s = new Studente(Convert.ToInt32(row["IdStud"]), row["nome"].ToString(), row["cognome"].ToString(), row["email"].ToString(), row["cellulare"].ToString(), row["data_n"].ToString(), row["citta"].ToString(), row["stato"].ToString(), row["nazionalita"].ToString(), row["password"].ToString(), row["instagram"].ToString(), row["facebook"].ToString());
                        studenti.Add(s);

                    }
                }
                Console.WriteLine("[OK] Lista Amici/Richieste restitutita con successo!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }
            return studenti;

        }

        public bool Delete_Friendship(int stud1, int stud2)
        {
            /* ---------------------------------------------------
            * Funzione che permette di annulare una richiesta 
            * oppure rifiutare un amicizia
            * ------------------------------------------------------
            */

            var wcfclient = server_conn.getInstance();
            string cond = "idstud1=" + stud1 + " and idstud2=" + stud2;
            bool result;
            try
            {
                result = wcfclient.DBdelete("AMICIZIA", cond);
                Console.WriteLine("[OK] Richiesta d'amicizia annullata con successo!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }
            return result;

        }

      
    }
    
}
