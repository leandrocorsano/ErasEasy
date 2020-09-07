//=============================================================================
// Authors: Francesca Rossi, Leandro Corsano
//=============================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using Admin_wcf.Classi;
using System.Net;
using System.Net.Mail;

namespace Admin_wcf
{
    public class Event : IEvent
    {
        /*
       *  PRINCIPALI FUNZIONALITA':
       *  @Creazione e gestione di eventi e riunioni
       *  @Recupero  partecipanti e informazione modifiche
       *  
       */
        public int Generate_id()
        {
            /* 
            * --------------------------------------------------------------------------------
            * Funzione che recupera l'ultimo id dell'evento  e restituisce l'id successivo
            * --------------------------------------------------------------------------------
            */
            var wcfclient = server_conn.getInstance(); //mi connetto a DB_Manager
            DataSet luo_set = wcfclient.DBselect("idev", "EVENTO", "idev>=all(select idev from EVENTO)");
            foreach (DataTable table in luo_set.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    int id = Convert.ToInt32(row["idev"]);
                    if (id <= 0)
                    {
                        Console.WriteLine("[OK] Id  Evento restituito con successo!!");
                        return 1;
                    }
                    else
                    {
                        Console.WriteLine("[OK] Id  Evento restituito con successo!!");
                        return id + 1;
                    }


                }
            }
            return 1; /*se non ci sono righe*/
        }
        public int Generate_id_Luogo()
        {
            /* 
            * --------------------------------------------------------------------------------
            * Funzione che recupera l'ultimo id del luogo del evento e restituisce l'id successivo
            * --------------------------------------------------------------------------------
            */
            var wcfclient = server_conn.getInstance();
            DataSet luo_set = wcfclient.DBselect("idluogo", "LUOGO", "idluogo>=all(select idluogo from LUOGO)");
            foreach (DataTable table in luo_set.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    int id = Convert.ToInt32(row["idluogo"]);
                    if (id <= 0)
                    {
                        Console.WriteLine("[OK] Id  luogo restituito con successo!!");
                        return 1;
                    }
                    else
                    {
                        Console.WriteLine("[OK] Id  luogo restituito con successo!!");
                        return id + 1;
                    }


                }
            }
            return 1; /*se non ci sono righe*/
        }
        public List<Svolgimento> Show_events(string cond="")
        {
            /* 
            * --------------------------------------------------------------------------------
            * Funzione che restituise la lista di determinati eventi o riunioni
            * --------------------------------------------------------------------------------
            */
            /*Nella condizione si può mettere la data/ora inizio, data/ora fine, oppure la citta, lo stato , l'id dell associazione, la tipologia*/
            /*Nel caso si voglia una lista di riunioni è necessario mettere nella codizione la tipologia="riunioni"*/
            var wcfclient = server_conn.getInstance();
            string condizione ="S.idev=E.idev and S.idluogo=L.idluogo"+cond;
            List<Svolgimento> eventi = new List<Svolgimento>();
            try
            {
                DataSet ev_set = wcfclient.DBselect("*", "SVOLGIMENTO as S, EVENTO as E, LUOGO as L", condizione);
                Association ass = new Association();
                foreach (DataTable table in ev_set.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        Associazione a = ass.Profile(Convert.ToInt32(row["idass"])); //recupero l'associazione che ha creato l'evento
                        Evento e = new Evento(Convert.ToInt32(row["idev"]), row["nome"].ToString(), row["tipologia"].ToString(), Convert.ToInt32(row["min_p"]), Convert.ToInt32(row["max_p"]), Convert.ToInt32(row["min_v"]), Convert.ToInt32(row["max_v"]), Convert.ToInt32(row["costo"]), row["descrizione"].ToString(), a);
          
                        Luogo l = new Luogo(Convert.ToInt32(row["idluogo"]),row["citta"].ToString(), row["via"].ToString(), row["stato"].ToString());
                        Svolgimento s = new Svolgimento(e, l, row["ora_i"].ToString(), row["ora_f"].ToString(), row["data_i"].ToString(), row["data_f"].ToString());
                        eventi.Add(s);
                    }
                }
                Console.WriteLine("[OK] Lista eventi recuparata con successo!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }
            return eventi;

        }

        public Svolgimento Get_event_by_id(int id)
        {
            /* 
            * --------------------------------------------------------------------------------
            * Funzione che restituisce le informazioni dell'evento corrispondente all'id passato come parametro
            * --------------------------------------------------------------------------------
            */
            var wcfclient = server_conn.getInstance();
            string condizione = "S.idev=E.idev and S.idluogo=L.idluogo and E.idev="+id;
            Svolgimento s=null;
            try
            {
                DataSet ev_set = wcfclient.DBselect("*", "SVOLGIMENTO as S, EVENTO as E, LUOGO as L", condizione);
                Association ass = new Association();
                foreach (DataTable table in ev_set.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        Associazione a = ass.Profile(Convert.ToInt32(row["idass"])); //recupero l'associazione che ha creato l'evento
                        Evento e = new Evento(Convert.ToInt32(row["idev"]), row["nome"].ToString(), row["tipologia"].ToString(), Convert.ToInt32(row["min_p"]), Convert.ToInt32(row["max_p"]), Convert.ToInt32(row["min_v"]), Convert.ToInt32(row["max_v"]), Convert.ToInt32(row["costo"]), row["descrizione"].ToString(), a);

                        Luogo l = new Luogo(Convert.ToInt32(row["idluogo"]), row["citta"].ToString(), row["via"].ToString(), row["stato"].ToString());
                        s = new Svolgimento(e, l, row["ora_i"].ToString(), row["ora_f"].ToString(), row["data_i"].ToString(), row["data_f"].ToString());
                        return s;
                    }
                }
                Console.WriteLine("[OK] Evento recuparato con successo!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }
            return s;

        }

        public List<Studente> Event_partecipations(Svolgimento e)
        {
            /* 
            * --------------------------------------------------------------------------------
            * Funzione che restituisce la lista degli studenti che partecipano ad un determinato evento
            * --------------------------------------------------------------------------------
            */
            var wcfclient = server_conn.getInstance();
            string condizione = "P.idev=" + e.evento.IdEv;

            List<Studente> studenti = new List<Studente>();
            try
            {
                DataSet ev_set = wcfclient.DBselect("*", "PARTECIPAZIONE as P", condizione);
                Student stud = new Student();
                foreach (DataTable table in ev_set.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        Studente s= stud.Profile(Convert.ToInt32(row["idstud"])); //recupero l'associazione che ha creato l'evento
                        studenti.Add(s);
                        
                    }
                }
                Console.WriteLine("[OK] Lista studenti che partecipano all'evento "+ e.evento.nome+" recuperata con successo!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }
            return studenti;

        }

        public List<Volontario> Event_volunteers(Svolgimento e)
        {
            /* 
            * --------------------------------------------------------------------------------
            * Funzione che restituisce la lista dei volontari che partecipano ad un evento o ad una riunione
            * --------------------------------------------------------------------------------
            */
            var wcfclient = server_conn.getInstance();
            string condizione = "G.idev=" + e.evento.IdEv;

            List<Volontario> volontari = new List<Volontario>();
            try
            {
                DataSet ev_set = wcfclient.DBselect("*", "GESTIONE AS G", condizione);
                Volunteer vol = new Volunteer();
                foreach (DataTable table in ev_set.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        Volontario v = vol.Profile(Convert.ToInt32(row["idvolont"])); //recupero l'associazione che ha creato l'evento
                        volontari.Add(v);

                    }
                }
                Console.WriteLine("[OK] Lista volontari  che partecipano all'evento " + e.evento.nome + " recuperata con successo!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;

            }
            return volontari;

        }

        public bool Edit_Event(Svolgimento s)
        {
            /* 
            * --------------------------------------------------------------------------------
            * Funzione che aggiorna un determinato evento o una riunione nel database
            * --------------------------------------------------------------------------------
            */
            var wcfclient = server_conn.getInstance();
            string valori_evento =  "nome='" + s.evento.nome + "', tipologia='" + s.evento.tipologia + "', min_p='" + s.evento.min_p + "', max_p='" + s.evento.max_p + "', min_v='" + s.evento.min_v + "', max_v='" + s.evento.max_v + "', costo='" + s.evento.costo + "', descrizione='" + s.evento.descrizione + "', idass='" + s.evento.ass.IdAss + "'";
            string valori_luogo = " citta='" + s.luogo.citta + "', via='" + s.luogo.via + "', stato='" + s.luogo.stato + "'";
            string valori_svolg = " data_i='" + s.data_i + "', data_f='" + s.data_f + "', ora_i='" + s.ora_i + "', ora_f='"+s.ora_f+"'";
            string query_evento = "UPDATE EVENTO SET "+ valori_evento + " WHERE idev=" + s.evento.IdEv;
            string query_luogo = "UPDATE LUOGO SET " + valori_luogo + " WHERE idluogo=" + s.luogo.IdLuogo;
            string query_svolgimento = "UPDATE SVOLGIMENTO SET " + valori_svolg + " WHERE idev=" + s.evento.IdEv;
            Console.WriteLine(query_evento);
            Console.WriteLine(query_luogo);
            Console.WriteLine(query_svolgimento);
            List<string> query = new List<string>() {query_evento, query_luogo, query_svolgimento };
            try
            {
                bool risultato = wcfclient.DBtransaction(query);
                Console.WriteLine("[OK] Evento modificato con successo!!");
                return risultato;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;
            }

        }

        public bool Delete_Event(Svolgimento s)
        {
            /* 
            * --------------------------------------------------------------------------------
            * Funzione che cancella un evento  o una riunione dal DB
            * --------------------------------------------------------------------------------
            */
            var wcfclient = server_conn.getInstance();
            string query_svolgimento = "DELETE FROM SVOLGIMENTO WHERE idev=" + s.evento.IdEv;
            string query_evento = "DELETE FROM EVENTO WHERE idev=" + s.evento.IdEv;
            string query_luogo = "DELETE FROM LUOGO WHERE idluogo=" + s.luogo.IdLuogo;
            string query_partecipaz = "DELETE FROM PARTECIPAZIONE WHERE idev=" + s.evento.IdEv;
            string query_gestione = "DELETE FROM GESTIONE WHERE idev=" + s.evento.IdEv;
            List<string> query =new List<string> () { query_svolgimento, query_evento, query_luogo, query_partecipaz, query_gestione };
            try
            {
                bool risultato = wcfclient.DBtransaction(query);
                Console.WriteLine("[OK] Evento cancellato con successo!!");
                return risultato;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR]" + ex.Message);
                throw;
            }

        }

        public void Send_Email(string nome, string email, string body_message, string sub)
        {
            /* 
            * --------------------------------------------------------------------------------
            * Funzione che si occupa di inviare un email
            * Utilizzata per avvisare i partecipanti di un evento/riunione quando tale avvenimento 
            * viene cancellato o modificato
            * --------------------------------------------------------------------------------
            */
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(email));  
            message.From = new MailAddress("progetto.erasmus2020@gmail.com"); 
            message.Subject = "ErasEasyLife: "+ sub;
            message.Body = string.Format(body, nome, email , body_message);
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "progetto.erasmus2020@gmail.com",  // replace with valid value
                    Password = "Eraseasylife2020"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 25;
                smtp.EnableSsl = true;
                try
                {
                    smtp.Send(message);
                    Console.WriteLine("[OK] Email inviata con successo!!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[ERROR]" + ex.Message);
                    throw;
                }
            }
        }

    }
}
