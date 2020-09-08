//=============================================================================
// Authors: Francesca Rossi, Leandro Corsano
//=============================================================================
using ErasEasyLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 


namespace ErasEasyLife.Controllers
{
    public class VolontarioController : Controller
    {
        /*
       *  CONTROLLER DELL'AREA VOLONTARIO
       *  PRINCIPALI FUNZIONALITA':
       *  @Riepilogo
       *  @Registrazione
       *  @Visualizzazione e modifica dati personali
       *  @Visualizzazione e prenotazione eventi
       */



        //riepilogo
        [HttpGet]
        public ActionResult Dashboard()
        {
            var volclient = new Volunteer.VolunteerClient();
            var eventclient = new Event.EventClient();
            Volunteer.Volontario vol = (Volunteer.Volontario)Session["volontario"];
            DateTime oggi = DateTime.Today;
            /*recupero i prossimi eventi  e le prossime riunioni*/
            string cond = " and data_i >= '" + oggi.ToString("yyyy-MM-dd") + "' and tipologia!='meeting' and idass=" + vol.ass.IdAss;
            List<Event.Svolgimento> futuri_eventi = eventclient.Show_events(cond);
            ViewData["n_prossimi_eventi"] = futuri_eventi.Count();           
            cond = " and data_i >= '" + oggi.ToString("yyyy-MM-dd") + "' and tipologia='meeting' and idass=" + vol.ass.IdAss;
            List<Event.Svolgimento> future_riunioni = eventclient.Show_events(cond);
            ViewData["n_prossime_riunioni"] = future_riunioni.Count();
            /*recupero i miei eventi  e le miei riunioni di questo mese*/
            cond = " tipologia!='meeting' and MONTH(data_i)='" + oggi.Month + "'";
            List<Volunteer.Svolgimento> my_events = volclient.Show_Event(vol.IdVolont, cond);
            cond = " tipologia='meeting' and MONTH(data_i)='"+oggi.Month+"'"; //le mie riunioni di questo mese
            List<Volunteer.Svolgimento> my_meetings = volclient.Show_Event(vol.IdVolont, cond);
            ViewData["n_mie_riunioni"] = my_meetings.Count();
            ViewData["n_miei_eventi"] = my_events.Count();
            return View();
        }

      /*===============================================================
       * FUNZIONI ACCESSO E USCITA
       * ===============================================================
       */

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(Models.Volontario model)
        {

            if (ModelState.IsValidField("email") && ModelState.IsValidField("password"))
            {
                /*non controllo se modelstate is valid perchè non uso tutti i membri del modello ma solo 2*/

                try
                {
                    var webclient = new Volunteer.VolunteerClient();
                    Volunteer.Volontario vol = webclient.Login(model.email, model.password);
                    if (vol != null)
                    {
                        
                        Session["Volontario"] = vol; //passo il volontario che è entrato tra le varie pagine web
                        return RedirectToAction("Dashboard", "Volontario"); //riporto l'utente sulla dashboard
                    }
                    else
                    {
                        ViewBag.message = "Wrong user";
                        return View();
                    }
                }
                catch(Exception ex)
                {
                    ViewBag.message = "Error:"+ ex.Message;
                    return View();
                }
            }
            else
            {
                ViewBag.message = "The form is wrong";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {

            Session.Abandon(); //cancello la sessione corrente
            return RedirectToAction("Login", "Volontario");

        }

        /*========================================================
         * ISCRIZIONE VOLONTARIO
         * =======================================================
         */

        [HttpGet]
        public ActionResult Registra()
        {
            /*funzione che recupera l'id del volontario e le associazioni presenti in db*/
            var webclient = new Association.AssociationClient();         
            List<Association.Associazione> associazioni= webclient.Show_associations("");
            var volclient = new Volunteer.VolunteerClient();
            int id = volclient.Generate_id();
            ViewData["ID"] = id;
            ViewData["associazioni"] = associazioni; //passo le associazioni per il menu a tendina del form
            return View();
        }

        [HttpPost]
        public ActionResult Registra(Models.Volontario model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    ViewData["Nome"] = model.nome;
                    ViewData["Cognome"] = model.cognome;
                    var webclient = new Volunteer.VolunteerClient();
                    Volunteer.Volontario vol = new Volunteer.Volontario();
                    Volunteer.Associazione assoc = webclient.GetAssociazione(model.ass);
                    vol.IdVolont = model.IdVolont;
                    vol.nome = model.nome;
                    vol.cognome = model.cognome;
                    vol.data_n = model.data_n;
                    vol.telefono = model.telefono;
                    vol.email = model.email;
                    vol.data_iscr = model.data_iscr;
                    vol.password = model.password;
                    vol.ruolo = model.ruolo;
                    vol.ass = assoc;
                    bool r = webclient.Registration(vol);
                    ViewBag.risposta = "Successfully registered";
                    ViewBag.url = "../Volontario/Login";
                    ViewBag.link = "Sign in";
                    return View("Successo");

                }
                else
                {
                    return View(); //mi segnala gli errori nel model
                }
            }
            catch (Exception ex)
            {
                ViewBag.risposta = "ERROR:"+ ex.Message;
                ViewBag.url = "../Volontario/Registra";
                ViewBag.link = "Sign out";
                return View("Errore");
            }
        }

        /*========================================================
         * AREA PERSONALE
         * =======================================================
         */

        [HttpGet]
        public ActionResult Profilo()
        {
            return View("Profilo");
        }

        [HttpGet]
        public ActionResult Modifica_Profilo()
        {
            return View("Modifica_Profilo");
        }

        [HttpPost]
        public ActionResult Modifica_Profilo(Models.Volontario model)
        {
            try
            {
                if (ModelState.IsValidField("telefono")&& ModelState.IsValidField("email"))
                {
                    var webclient = new Volunteer.VolunteerClient();
                    Volunteer.Volontario vol = new Volunteer.Volontario();
                    Volunteer.Associazione assoc = webclient.GetAssociazione(model.ass);
                    vol.IdVolont = model.IdVolont;
                    vol.nome = model.nome;
                    vol.cognome = model.cognome;
                    vol.data_n = model.data_n;
                    vol.telefono = model.telefono;
                    vol.email = model.email;
                    vol.data_iscr = model.data_iscr;
                    vol.password = model.password;
                    vol.ruolo = model.ruolo;
                    vol.ass = assoc;
                    bool r = webclient.UpdateProfile(vol);
                    if(r==true)
                    {
                        Session["Volontario"] = vol; //aggiorno la sessione
                    }
                    ViewBag.risposta = "Profile Successfully updated";
                    ViewBag.url = "../Volontario/Profilo";
                    ViewBag.link = "Go to profile";
                    return View("Successo");
                }
                else
                {
                    return View();
                }

            }
            catch (Exception ex)
            {
                ViewBag.risposta = "error: "+ex.Message;
                ViewBag.url = "../Volontario/Modifica_profilo";
                ViewBag.link = "Back, try again";
                return View("Errore");
            }
        }

        [HttpGet]
        public ActionResult Modifica_Pass()
        {
            return View("Modifica_Pass");
        }

        [HttpPost]
        public ActionResult Modifica_Pass(Models.CambioPass model)
        {
            try
            {
                if ((ModelState.IsValid))
                {

                    var webclient = new Volunteer.VolunteerClient();
                    Volunteer.Volontario vol = (Volunteer.Volontario)Session["Volontario"]; //recupero il volontario che è entrato
                    vol.password = model.nuova_pass;
                    bool r = webclient.UpdatePassword(vol.IdVolont, model.nuova_pass);
                    if (r == true)
                    {
                        Session["Volontario"] = vol; //creo la nuova session
                    }
                    ViewBag.risposta = "Password successfully updated";
                    ViewBag.url = "../Volontario/Profilo";
                    ViewBag.link = "Go to profile";
                    return View("Successo");

                }
                else
                {

                    return View(); //mi fa vedere gli errori del model
                }
            }
            catch (Exception ex)
            {
                ViewBag.risposta = "Error: "+ex.Message;
                ViewBag.url = "../Volontario/Modifica_Pass";
                ViewBag.link = "Back, Try again";
                return View("Errore");
            }

        }

        /* ========================================================
         * GESTIONE EVENTI E RIUNIONI
         * =======================================================
         */

        [HttpGet]
        public ActionResult MieiEventi()
        {
            /* eventi a cui un volontario ha partecipato/parteciperà  */
            var webclient = new Volunteer.VolunteerClient();
            Volunteer.Volontario vol = (Volunteer.Volontario)Session["Volontario"];
            string tipologia = " E.tipologia != 'meeting'";
            List<Volunteer.Svolgimento> listaeventi = webclient.Show_Event(vol.IdVolont, tipologia);
            ViewData["eventi"] = listaeventi;
            return View();

        }

        [HttpGet]
        public ActionResult MieRiunioni()
        {
            /*Riunioni a cui un volontario ha partecipato/parteciperà */
            var webclient = new Volunteer.VolunteerClient();
            Volunteer.Volontario vol = (Volunteer.Volontario)Session["Volontario"];
            string tipologia = " E.tipologia = 'meeting'";
            List<Volunteer.Svolgimento> listariunioni = webclient.Show_Event(vol.IdVolont, tipologia);
            ViewData["riunioni"] = listariunioni;
            return View();

        }

        [HttpGet]
        public ActionResult Lista_Eventi()
        {
            /*Lista dei prossimi eventi ai quali un volontario può partecipare*/
            try
            {
                DateTime oggi = DateTime.Today;
                Volunteer.Volontario vol = (Volunteer.Volontario)Session["Volontario"];
                string cond = " and data_i > '" + oggi.ToString("yyyy-MM-dd") + "' and tipologia!='meeting' and idass = " + vol.ass.IdAss + " order by data_i";
                var webclient = new Event.EventClient();
                List<Event.Svolgimento> eventi = webclient.Show_events(cond);
                ViewData["eventi"] = eventi;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.risposta = "Errore:" + ex.Message;
                ViewBag.url = "../Volontario/Lista_Eventi";
                ViewBag.link = "Back to events";
                return View("Errore");
            }
        }

        [HttpGet]
        public ActionResult Dettagli_Evento()
        {
            /*impedisco che l'utente inserisca l'indirizzo senza cliccare il pulsante corretto nella lista evento*/
            ViewBag.risposta = "You don't have the privilegies to see this page";
            ViewBag.url = "../Volontario/Lista_Eventi";
            ViewBag.link = "Back";
            return View("Errore");
        }

        [HttpPost]
        public ActionResult Dettagli_Evento(FormCollection form)
        {
            /*dettaglio di un evento che mostra anche i volontari partecipanti*/
            try
            {
                int id = Int32.Parse(form["idev"]);
                var webclient = new Event.EventClient();
                Event.Svolgimento e = webclient.Get_event_by_id(id);
                List<Event.Volontario> volontari = webclient.Event_volunteers(e);
                ViewData["evento"] = e;
                ViewData["volontari"] = volontari; //lista di tutti i volontari partecipanti
                return View();
            }
            catch(Exception ex)
            {
                ViewBag.risposta = "Errore:"+ ex.Message;
                ViewBag.url = "../Volontario/Lista_Eventi";
                ViewBag.link = "Back to events";
                return View("Errore");
            }
        }

        [HttpGet]
        public ActionResult Partecipa()
        {
            /*impedisco che l'utente inserisca l'indirizzo senza cliccare il pulsante corretto nel dettaglio*/
            ViewBag.risposta = "You don't have the privilegies to see this page";
            ViewBag.url = "../Volontario/Lista_Eventi";
            ViewBag.link = "Back";
            return View("Errore");
        }

        [HttpPost]
        public ActionResult Partecipa(FormCollection form)
        {
            /*funzione che permette al volontario sia di partecipare ad un evento che ad una riunione*/
            try
            {
                int evento = Int32.Parse(form["idev"]);
                var evclient= new Event.EventClient();
                Event.Svolgimento e = evclient.Get_event_by_id(evento); //recupero l 'evento
                Volunteer.Volontario vol = (Volunteer.Volontario)Session["volontario"];
                var volclient = new Volunteer.VolunteerClient();
                bool r = volclient.BookEvent(vol.IdVolont, evento);
                if (r == true)
                {
                    if (e.evento.tipologia == "meeting")
                    {
                        string body = "You have successfully booked the meeting name " + e.evento.nome;
                        evclient.Send_Email(vol.nome, vol.email, body, "Meeting Booked");
                        ViewBag.risposta = "Meeting successfully booked. Check your mail.";
                        ViewBag.url = "../Evento/Lista_riunioni";
                        ViewBag.link = "Back to meetings";
                    }
                    else
                    {
                        string body = "You have successfully booked the event name " + e.evento.nome;
                        evclient.Send_Email(vol.nome, vol.email, body, "Event Booked");
                        ViewBag.risposta = "Event successfully booked. Check your mail.";
                        ViewBag.url = "../Volontario/Lista_Eventi";
                        ViewBag.link = "Back to events";
                    }
                    return View("Successo");
                }
                else
                {
                    ViewBag.risposta = "There was an errore, Try again!";
                    ViewBag.url = "../Volontario/Dashboard";
                    ViewBag.link = "Back to home";
                    return View("Errore");
                }
            }
            catch (Exception ex)
            {
                ViewBag.risposta = "Error: "+ ex.Message;
                ViewBag.url = "../Volontario/Lista_Eventi";
                ViewBag.link = "Back to list";
                return View("Errore");
            }
        }

        [HttpPost]
        public ActionResult Elimina_partecipazione(FormCollection form)
        {
            /*elimina la partecipazione sia per l'evento che per la riunione*/
            try
            {
                int evento = Int32.Parse(form["idev"]);
                var evclient = new Event.EventClient();
                Event.Svolgimento e = evclient.Get_event_by_id(evento); //recupero evento/riunione
                Volunteer.Volontario vol = (Volunteer.Volontario)Session["volontario"];
                var volclient = new Volunteer.VolunteerClient();
                bool r = volclient.CancelBooking(vol.IdVolont, evento);
                if (r == true)
                {
                    if (e.evento.tipologia == "meeting")
                    {
                        ViewBag.risposta = "Meeting successfully cancelled";
                        ViewBag.url = "../Evento/Lista_riunioni";
                        ViewBag.link = "Back to meetings";
                    }
                    else
                    {
                        ViewBag.risposta = "Event successfully cancelled";
                        ViewBag.url = "../Volontario/Lista_Eventi";
                        ViewBag.link = "Back to events";
                    }
                    return View("Successo");
                }
                else
                {
                    ViewBag.risposta = "There was an error, try again";
                    ViewBag.url = "../Volontario/Dashboard";
                    ViewBag.link = "Back to home";
                    return View("Errore");
                }
            }
            catch (Exception ex)
            {
                ViewBag.risposta = "Error: "+ ex.Message;
                ViewBag.url = "../Volontario/Dashboard";
                ViewBag.link = "Back to home";
                return View("Errore");
            }
        }

        //altri volontari
        [HttpGet]
        public ActionResult Elenco()
        {
            /*elenco di tutti i volontari di una determinata associazione*/
                var webclient = new Volunteer.VolunteerClient();
                Volunteer.Volontario vol = (Volunteer.Volontario)Session["Volontario"];
                string cond = "idvolont!=" + vol.IdVolont + " and idass=" + vol.ass.IdAss + "";
                List<Volunteer.Volontario> volontari = webclient.Show_volontari(cond);
                ViewData["volontari"] = volontari;
                return View();
            
        }

    }
}
