using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using Admin_wcf.Classi;

namespace Admin_wcf
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "Event" nel codice e nel file di configurazione contemporaneamente.
    public class Event : IEvent
    {
        public List<Svolgimento> Show_events(string cond="")
        {
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
            /*Nella condizione si può mettere la data/ora inizio, data/ora fine, oppure la citta, lo stato , l'id dell associazione, la tipologia*/
            /*Nel caso si voglia una lista di riunioni è necessario mettere nella codizione la tipologia="riunioni"*/
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
            /*Nella condizione si può mettere la data/ora inizio, data/ora fine, oppure la citta, lo stato , l'id dell associazione, la tipologia*/
            /*Nel caso si voglia una lista di riunioni è necessario mettere nella codizione la tipologia="riunioni"*/
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

    }
}
