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
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "Student" nel codice e nel file di configurazione contemporaneamente.
    public class Student : IStudent
    {
        public bool Registration(Studente s)
        {
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
            /* N.B. è possibile modificare tutti i campi tranne la password e l'id*/
            //var wcfclient = new DBManager.DBManagerClient(); //mi connetto al server
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
            * ------------------------------------------------------*/


            var wcfclient = server_conn.getInstance();
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
            * ------------------------------------------------------*/

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
        public List<Evento> Show_Event(int idstud)
        {
            /* ---------------------------------------------------
             * Eventi a cui ha partecipato/parteciperà uno studente
             * ------------------------------------------------------*/
            string cond = "E.idev=P.idev and P.idstud=" + idstud + "";
            var wcfclient = server_conn.getInstance();
            Association ass = new Association();
            
            List<Evento> eventi = new List<Evento>();
            try
            {
                DataSet eventi_set = wcfclient.DBselect("*", "PARTECIPAZIONE as P, EVENTO as E", cond);
                foreach (DataTable table in eventi_set.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        Associazione a = ass.Profile(Convert.ToInt32(row["idass"]));
                        Evento e = new Evento(Convert.ToInt32(row["idev"]), row["nome"].ToString(), row["tipologia"].ToString(), Convert.ToInt32(row["min_p"]), Convert.ToInt32(row["max_p"]), Convert.ToInt32(row["min_v"]), Convert.ToInt32(row["max_v"]), Convert.ToInt32(row["costo"]), row["descrizione"].ToString(),a );
                        eventi.Add(e);
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
            * Funzione che permette di disdire un evento
            * ------------------------------------------------------*/

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
    }
    
}
