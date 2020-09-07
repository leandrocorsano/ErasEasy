//=============================================================================
// Authors: Francesca Rossi, Leandro Corsano
//=============================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using ErasEasyLife.Models;


namespace ErasEasyLife.Controllers
{
    public class AssociazioneController : Controller
    {
        /*
         *  CONTROLLER DELL'AREA ASSOCIAZIONE
         *  PRINCIPALI FUNZIONALITA':
         *  @Riepilogo
         *  @Registrazione
         *  @Visualizzazione e modifica dati personali
         *  @Gestione Eventi e volontari
         */
       
        //riepilogo
        [HttpGet]
        public ActionResult Dashboard()
        {
            /*Recupero dal server i vecchi e i nuovi volontari */
            var volclient = new Volunteer.VolunteerClient();
            Association.Associazione ass = (Association.Associazione)Session["associazione"];
            string condition = " idass=" + ass.IdAss + " and ruolo=''";
            List<Volunteer.Volontario> nuovi_volontari = volclient.Show_volontari(condition);
            condition = " idass=" + ass.IdAss + " and ruolo!=''";
            List<Volunteer.Volontario> vecchi_volontari = volclient.Show_volontari(condition);
            ViewData["n_nuovi_vol"] = nuovi_volontari.Count();
            ViewData["n_vecchi_vol"] = vecchi_volontari.Count();

            /*Recupero dal server gli eventi passati e futuri*/
            var eventclient = new Event.EventClient();
            DateTime oggi = DateTime.Today;
            string cond = " and data_i >= '" + oggi.ToString("yyyy-MM-dd") + "' and tipologia!='riunione' and idass=" + ass.IdAss;
            List<Event.Svolgimento> futuri_eventi = eventclient.Show_events(cond);
            string cond1 = " and data_i <'" + oggi.ToString("yyyy-MM-dd") + "' and tipologia!='riunione' and idass=" + ass.IdAss;
            List<Event.Svolgimento> eventi_passati = eventclient.Show_events(cond1);
            ViewData["n_prossimi_eventi"] = futuri_eventi.Count();
            ViewData["n_scorsi_eventi"] = eventi_passati.Count();

            /*Recupero dal server le riunioni passate e future*/
            cond = " and data_i >= '" + oggi.ToString("yyyy-MM-dd") + "' and tipologia='riunione' and idass=" + ass.IdAss;
            List<Event.Svolgimento> future_riunioni = eventclient.Show_events(cond);
            cond1 = " and data_i <'" + oggi.ToString("yyyy-MM-dd") + "' and tipologia='riunione' and idass=" + ass.IdAss;
            List<Event.Svolgimento> riunioni_passate = eventclient.Show_events(cond1);
            ViewData["n_prossime_riunioni"] = future_riunioni.Count();
            ViewData["n_scorse_riunioni"] = riunioni_passate.Count();

            return View();
        }

        /*==============================================================
         * ISCRIZIONE ASSOCIAZIONE
         * ============================================================
         */
        [HttpGet]
        public ActionResult Registra()
        {
            /*Recupero l'id successivo all'ultimo presente in database per registrare l'utente senza errori*/
            var webclient = new Association.AssociationClient();
            int id = webclient.Generate_id();
            ViewData["ID"] = id;
            return View();

        }

        [HttpPost]
        public ActionResult Registra(Models.Associazione model)
        {
            try
            {
                if ((ModelState.IsValid))
                {
                    /*passaggio al server dei dati dell'utente per la registrazione in database*/
                    var webclient = new Association.AssociationClient();

                    Association.Associazione ass = new Association.Associazione();
                    ass.IdAss = model.IdAss;
                    ass.nome = model.nome;
                    ass.citta = model.citta;
                    ass.stato = model.stato;
                    ass.tel = model.tel;
                    ass.via = model.via;
                    ass.password = model.password;
                    ass.email = model.email;
                    bool r = webclient.Registration(ass);

                    ViewBag.risposta = "Successfully registered";
                    ViewBag.url = "Login";
                    ViewBag.link = "Sign in";
                    return View("Successo");
                }

                else
                {
                    /*se c'è qualche incorrettezza nella compilazione del form */
                    /*Recupero l'id successivo all'ultimo presente in database per registrare l'utente senza errori*/
                    var webclient = new Association.AssociationClient();
                    int id = webclient.Generate_id();
                    ViewData["ID"] = id;
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.risposta = "Exception: "+ ex.Message;
                ViewBag.url = "Registra";
                ViewBag.link = "Sign out";
                return View("Errore");
                
            }
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
        public ActionResult Login(Models.Associazione model)
        {

            if (ModelState.IsValidField("email") && ModelState.IsValidField("password"))
            {
                /*non controllo se modelstate is valid perchè non uso tutti i membri del modello ma solo 2*/

                try
                {
                    /*Chiedo al server di restituirmi i dati dell'associazione che è entrata, se presente in db*/
                    var webclient = new Association.AssociationClient();
                    Association.Associazione ass = webclient.Login(model.email, model.password);
                    if (ass != null)
                    {
                        Session["associazione"] = ass; //passo l'associazione che è entrata tra le varie pagine web
                        return RedirectToAction("Dashboard", "Associazione");
                    }
                    else
                    {
                        ViewBag.message = "Wrong user";
                        return View();
                    }


                }
                catch
                {
                    ViewBag.message = "There was an error, Try Again";
                    return View();
                }
            }
            else
            {
                ViewBag.message = "The value of the form are wrong";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            /*Cancello la sessione corrente*/
            Session.Abandon();
            return RedirectToAction("Login", "Associazione");

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
        public ActionResult Modifica_Profilo(Models.Associazione model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    /*Invio i dati al server del profilo modificato e aggiono la sessione con i nuovi dati*/
                    var webclient = new Association.AssociationClient();
                    Association.Associazione ass = new Association.Associazione();
                    ass.IdAss = model.IdAss;
                    ass.password = model.password;
                    ass.nome = model.nome;
                    ass.citta = model.citta;
                    ass.stato = model.stato;
                    ass.tel = model.tel;
                    ass.email = model.email;
                    ass.via = model.via;
                    Session["Associazione"] = ass;
                    bool r = webclient.UpdateProfile(ass);
                    ViewBag.risposta = "Profile successfully updated";

                    return View("Profilo");

                }
                else
                {

                    return View();

                }

            }
            catch (Exception ex)
            {
                ViewBag.risposta = "Exception: " + ex.Message;
                ViewBag.url = "Modifica_Profilo";
                ViewBag.link = "Back";
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
                    /*Invio al server la nuova password inserita dall'utente*/
                    var webclient = new Association.AssociationClient();
                    Association.Associazione ass = (Association.Associazione)Session["Associazione"]; //recupero l'associazione che è entrata

                    ass.password = model.nuova_pass;

                    bool r = webclient.UpdatePassword(ass.IdAss, model.nuova_pass);
                    if (r == true)
                    {
                        Session["Associazione"] = ass; //creo la nuova session
                    }
                    ViewBag.risposta = "Password successfully updated";
                    return View("Successo");

                }
                else
                {

                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.risposta = "Exception: " + ex.Message;
                ViewBag.url = "Modifica_Pass";
                ViewBag.link = "Back";
                return View("Errore");
            }

        }


        /* ========================================================
         * FUNZIONE PER AREA STUDENTE
         * =======================================================
         */
        [HttpGet]
        public ActionResult Elenco()
        {
            /* Recupero dal server la lista delle citta dove sono presenti le associazioni iscritte*/
            var webclient = new Association.AssociationClient();
            List<string> listacitta = webclient.GetCitta("");
            ViewData["citta"] = listacitta;
            return View();
        }

        [HttpPost]
        public ActionResult Elenco(Models.Associazione model)
        {
            if (ModelState.IsValidField("citta"))
            {
                /*Recupero dal server la lista delle associazioni presenti nella città selezionata dallo studente */
                var webclient = new Association.AssociationClient();
               
                string cond = "citta='" + model.citta + "'";
                List<string> listacitta = webclient.GetCitta("");
                ViewData["citta"] = listacitta;
                List<Association.Associazione> associazioni = webclient.Show_associations(cond);

                ViewData["elenco_associazioni"] = associazioni;
                return View();
            }
            else
            {
                return View();
            }
            
        }

        /* ========================================================
         * FUNZIONI SUI VOLONTARI
         * =======================================================
         */

        [HttpGet]
        public ActionResult Elenco_Volontari()
        {
                /* Recupero dal server i volontari dell'associazione autenticata*/
                var webclient = new Volunteer.VolunteerClient();
                Association.Associazione ass = (Association.Associazione)Session["Associazione"];
                string cond = "idass=" + ass.IdAss;
                List<Volunteer.Volontario> volontari = webclient.Show_volontari(cond);

                ViewData["elenco_volontari"] = volontari;
                return View();

        }

        [HttpGet]
        public ActionResult Aggiungi_ruolo()
        {
            /*impedisco che l'utente inserisca l'indirizzo senza cliccare il pulsante corretto nell'area volontario*/
            ViewBag.risposta = "You don't have the privilegies to see this page";
            ViewBag.url = "Elenco_Volontari";
            ViewBag.link = "Back";
            return View("Errore");
        }

        [HttpPost]
        public ActionResult Aggiungi_ruolo(Models.Volontario model)
        {

            if (ModelState.IsValidField("IdVolont") && ModelState.IsValidField("ruolo"))
            {
                /*non controllo se modelstase is valid perchè non uso tutti i membri del modello ma solo 2*/

                try
                {
                    var webclient = new Association.AssociationClient();
                    bool r = webclient.add_ruolo(model.IdVolont, model.ruolo);
                    if (r == true)
                    {

                        ViewBag.risposta = "Role successfully added";
                        ViewBag.url = "Elenco_Volontari";
                        ViewBag.link = "Back";
                        return View("Successo");
                    }
                    else
                    {
                        ViewBag.risposta = "Something has gone wrong";
                        ViewBag.url = "Elenco_Volontari";
                        ViewBag.link = "Back";
                        return View("Errore");
                    }


                }
                catch(Exception ex)
                {
                    ViewBag.risposta = "Exception: " + ex.Message;
                    ViewBag.url = "Elenco_Volontari";
                    ViewBag.link = "Back";
                    return View("Errore");
                }
            }
            else
            {
                return View("Elenco_Volontari");
            }
        }

        /* ========================================================
         * GESTIONE EVENTI E RIUNIONI
         * =======================================================
         */
        [HttpGet]
        public ActionResult Crea_Evento()
        {
            /* Recupero in automatico l'id corretto per l'evento e per il luogo*/
            var webclient = new Event.EventClient();
            int id = webclient.Generate_id();
            int id1 = webclient.Generate_id_Luogo();
            ViewData["ID_lu"] = id1;
            ViewData["ID_ev"] = id;
            return View("Crea_Evento");
        }
        
        [HttpPost]
        public ActionResult Crea_Evento(Models.Evento model)
        {
            try
            {
                if ((ModelState.IsValid))
                {
                    /* converto la data, per usufruire di tutte le funzionalità */
                    DateTime Data1 = DateTime.Parse(model.data_i);
                    DateTime Data2 = DateTime.Parse(model.data_f);
                    if (model.tipologia == "Riunione") //controllo che l'evento non sia una riunione
                    {
                        ViewBag.risposta = "You can't create a meeting in this page";
                        ViewBag.url = "Crea_Evento";
                        ViewBag.link = "Back";
                        return View("Errore");
                    }
                    else if (Data1 > Data2) //controllo che la data di fine sia superiore alla data d'inizio
                    {
                        ViewBag.risposta = "The end date is before the start date";
                        ViewBag.url = "Crea_Evento";
                        ViewBag.link = "Back";
                        return View("Errore");
                    }
                    else
                    {
                        /* Invio al server i dati dell'evento creato dall'utente */
                        var webclient = new Association.AssociationClient();
                        Association.Associazione ass = (Association.Associazione)Session["Associazione"];
                        Association.Evento ev = new Association.Evento();
                        Association.Luogo l = new Association.Luogo();
                        Association.Svolgimento svo = new Association.Svolgimento();
                        ev.IdEv = model.IdEv;
                        ev.nome = model.nome;
                        ev.tipologia = model.tipologia;
                        ev.min_p = model.min_p;
                        ev.max_p = model.max_p;
                        ev.min_v = model.min_v;
                        ev.max_v = model.max_v;
                        ev.costo = model.costo;
                        ev.descrizione = model.descrizione;
                        ev.ass = (Association.Associazione)Session["Associazione"];
                        l.IdLuogo = model.IdLuogo;
                        l.via = model.via;
                        l.citta = model.citta;
                        l.stato = model.stato;
                        svo.evento = ev;
                        svo.luogo = l;
                        svo.data_i = model.data_i;
                        svo.data_f = model.data_f;
                        svo.ora_i = model.ora_i;
                        svo.ora_f = model.ora_f;
                        bool r = webclient.Create_events(svo);
                        ViewBag.risposta = "Event successfully created";
                        ViewBag.url = "../Associazione/Lista_Eventi";
                        ViewBag.link = "Go to events";
                        return View("Successo");
                    }

                }
                else
                {
                    /*se il form non è corretto*/
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.risposta = "Exception: " + ex.Message;
                ViewBag.url = "Crea_Evento";
                ViewBag.link = "Back";
                return View("Errore");

            }
        }

        [HttpGet]
        public ActionResult Crea_Riunione()
        {
            /* Recupero in automatico l'id corretto per la riunione e per il luogo*/
            var webclient = new Event.EventClient();
            int id = webclient.Generate_id();
            int id1 = webclient.Generate_id_Luogo();
            ViewData["ID_lu"] = id1;
            ViewData["ID_ev"] = id;
            return View("Crea_Riunione");
        }

        [HttpPost]
        public ActionResult Crea_Riunione(Models.Evento model)
        {
            try
            {
                if ((ModelState.IsValid))
                {
                    /* Invio i dati al server che provvederà a registrare la nuova riunione */ 
                    var webclient = new Association.AssociationClient();
                    Association.Associazione ass = (Association.Associazione)Session["Associazione"];
                    Association.Evento ev = new Association.Evento();
                    Association.Luogo l = new Association.Luogo();
                    Association.Svolgimento svo = new Association.Svolgimento();
                    ev.IdEv = model.IdEv;
                    ev.nome = model.nome;
                    ev.tipologia = model.tipologia;
                    ev.min_p = model.min_p;
                    ev.max_p = model.max_p;
                    ev.min_v = model.min_v;
                    ev.max_v = model.max_v;
                    ev.costo = model.costo;
                    ev.descrizione = model.descrizione;
                    ev.ass = (Association.Associazione)Session["Associazione"];
                    l.IdLuogo = model.IdLuogo;
                    l.via = ass.via;
                    l.citta = ass.citta;
                    l.stato = ass.stato;
                    svo.evento = ev;
                    svo.luogo = l;
                    svo.data_i = model.data_i;
                    svo.data_f = model.data_i;
                    svo.ora_i = model.ora_i;
                    svo.ora_f = model.ora_f;
                    bool r = webclient.Create_events(svo);
                    ViewBag.risposta = "Meeting successfully created";
                    ViewBag.url = "../Associazione/Lista_Riunioni";
                    ViewBag.link = "Go to meetings";
                    return View("Successo");

                }
                else
                {
                    return View();
                }
            }
               catch (Exception ex)
            {
                ViewBag.risposta = "Exception: " + ex.Message;
                ViewBag.url = "Crea_Riunione";
                ViewBag.link = "Back";
                return View("Errore");

            }
        }
       
        [HttpGet]
        public ActionResult Elenco_Eventi()
        {
            /*Recupero tutti gli eventi che ha creato l'associazione autenticata dal server*/
            var webclient = new Association.AssociationClient();
            Association.Associazione ass = (Association.Associazione)Session["Associazione"];
            string tipologia = " E.tipologia != 'riunione'  order by S.data_i desc";
            List<Association.Svolgimento> listaeventi = webclient.Show_Event(ass.IdAss, tipologia);
            ViewData["eventi"] = listaeventi;
            return View();
        }

        [HttpGet]
        public ActionResult Elenco_Riunioni()
        {
            /* Recupero tutti gli eventi che ha creato l'associazione autenticata dal server*/
            var webclient = new Association.AssociationClient();
            Association.Associazione ass = (Association.Associazione)Session["Associazione"];
            string tipologia = " E.tipologia = 'riunione' order by S.data_i desc";
            List<Association.Svolgimento> listariunioni = webclient.Show_Event(ass.IdAss, tipologia);
            ViewData["riunioni"] = listariunioni;
            return View();

        }

        [HttpGet]
        public ActionResult Dettagli_Evento()
        {
            /*impedisco che l'utente inserisca l'indirizzo senza cliccare il pulsante corretto nell'area volontario*/
            ViewBag.risposta = "You don't have the privilegies to see this page";
            ViewBag.url = "Elenco_Eventi";
            ViewBag.link = "Back";
            return View("Errore");
        }

        [HttpPost]
        public ActionResult Dettagli_Evento(FormCollection form)
        {
            /*Recupero le informazioni relative all'evento selezionato */
            int id = Int32.Parse(form["idev"]);
            var webclient = new Event.EventClient();
            Event.Svolgimento e = webclient.Get_event_by_id(id);
            List<Event.Studente> studenti = webclient.Event_partecipations(e); //lista studenti partecipanti
            List<Event.Volontario> volontari = webclient.Event_volunteers(e); //lista volontari partecipanti
            ViewData["evento"] = e;
            ViewData["studenti"] = studenti;
            ViewData["volontari"] = volontari;



            return View();


        }

        [HttpGet]
        public ActionResult Dettagli_Riunione()
        {
            /*impedisco che l'utente inserisca l'indirizzo senza cliccare il pulsante corretto nell'area volontario*/
            ViewBag.risposta = "You don't have the privilegies to see this page";
            ViewBag.url = "Elenco_Riunioni";
            ViewBag.link = "Back";
            return View("Errore");
        }

        [HttpPost]
        public ActionResult Dettagli_Riunione(FormCollection form)
        {
            /* Recupero le info della riunione selezionata */
            int id = Int32.Parse(form["idev"]);
            var webclient = new Event.EventClient();
            Event.Svolgimento e = webclient.Get_event_by_id(id);
            List<Event.Volontario> volontari = webclient.Event_volunteers(e); //elenco dei volontari partecipanti
            ViewData["evento"] = e;
            ViewData["volontari"] = volontari;



            return View();


        }

        [HttpGet]
        public ActionResult Edit_Evento()
        {
            /*impedisco che l'utente inserisca l'indirizzo senza cliccare il pulsante corretto nell'area volontario*/
            ViewBag.risposta = "You don't have the privilegies to see this page";
            ViewBag.url = "Elenco_Eventi";
            ViewBag.link = "Back";
            return View("Errore");
        }

        [HttpPost]
        public ActionResult Edit_Evento(FormCollection form)
        {
            /* form che permette la modifica di un evento */
            int id = Int32.Parse(form["idev"]);
            var webclient = new Event.EventClient();
            Event.Svolgimento e = webclient.Get_event_by_id(id);
            ViewData["evento"] = e;
            return View();
        }

        [HttpGet]
        public ActionResult Edit_Riunione()
        {
            /*impedisco che l'utente inserisca l'indirizzo senza cliccare il pulsante corretto nell'area volontario*/
            ViewBag.risposta = "You don't have the privilegies to see this page";
            ViewBag.url = "Elenco_Riunioni";
            ViewBag.link = "Back";
            return View("Errore");
        }

        [HttpPost]
        public ActionResult Edit_Riunione(FormCollection form)
        {
           /*form che permette la modifica di una riunione*/
            int id = Int32.Parse(form["idev"]);
            var webclient = new Event.EventClient();
            Event.Svolgimento e = webclient.Get_event_by_id(id);
            ViewData["evento"] = e;
            return View();
        }

        [HttpGet]
        public ActionResult Modifica_Evento()
        {
            /*impedisco che l'utente inserisca l'indirizzo senza cliccare il pulsante corretto nell'area volontario*/
            ViewBag.risposta = "You don't have the privilegies to see this page";
            ViewBag.url = "../Associazione/Elenco_Eventi";
            ViewBag.link = "Back";
            return View("Errore");
        }

        [HttpPost]
        public ActionResult Modifica_Evento(Models.Evento model)
        {
            try
            {
                if ((ModelState.IsValid))
                {
                    DateTime Data1 = DateTime.Parse(model.data_i);
                    DateTime Data2 = DateTime.Parse(model.data_f);
                    if (model.tipologia == "Riunione") //controllo che l'utente non abbia cambiato la tipologia in riunione
                    {
                        ViewBag.risposta = "You can't create a meeting in this page";
                        ViewBag.url = "Elenco_Eventi";
                        ViewBag.link = "Back to list event";
                        return View("Errore");
                    }
                    else if (Data1 > Data2)
                    {
                        ViewBag.risposta = "The end date is before the start date";
                        return View("Errore");
                    }
                    else
                    {
                        /* Invio al server le informazioni che gli servono per modificare l'evento */
                        var webclient = new Event.EventClient();
                        Association.Associazione ass = (Association.Associazione)Session["associazione"];
                        Console.WriteLine(ass.nome);
                        Event.Evento ev = new Event.Evento();
                        Event.Luogo l = new Event.Luogo();
                        Event.Svolgimento svo = new Event.Svolgimento();
                        Event.Associazione assoc = new Event.Associazione();
                        ev.IdEv = model.IdEv;
                        ev.nome = model.nome;
                        ev.tipologia = model.tipologia;
                        ev.min_p = model.min_p;
                        ev.max_p = model.max_p;
                        ev.min_v = model.min_v;
                        ev.max_v = model.max_v;
                        ev.costo = model.costo;
                        ev.descrizione = model.descrizione;
                        assoc.IdAss = ass.IdAss;
                        assoc.nome = ass.nome;
                        assoc.citta = ass.citta;
                        assoc.stato = ass.stato;
                        assoc.via = ass.via;
                        assoc.tel = ass.tel;
                        assoc.email = ass.email;
                        assoc.password = ass.password;
                        ev.ass = assoc;
                        l.IdLuogo = model.IdLuogo;
                        l.via = model.via;
                        l.citta = model.citta;
                        l.stato = model.stato;
                        svo.evento = ev;
                        svo.luogo = l;
                        svo.data_i = model.data_i;
                        svo.data_f = model.data_f;
                        svo.ora_i = model.ora_i;
                        svo.ora_f = model.ora_f;
                        List<Event.Studente> studenti = webclient.Event_partecipations(svo);
                        List<Event.Volontario> volontari = webclient.Event_volunteers(svo);
                        bool r = webclient.Edit_Event(svo);
                        if (r == true)
                        {
                            /* Invio l'email ai partecipanti */
                            string body = "The event name " + svo.evento.nome + " of " + Data1.ToString("dd/MM/yy") + " was updated by the " + svo.evento.ass.nome;
                            studenti.ForEach(stud =>
                            {
                                webclient.Send_Email(stud.nome, stud.email, body, "Event Modified");
                            });
                            volontari.ForEach(vol =>
                            {
                                webclient.Send_Email(vol.nome, vol.email, body, "Event Modified");
                            });

                            ViewBag.risposta = "Event successfully modified";
                            ViewBag.url = "../Associazione/Elenco_Eventi";
                            ViewBag.link = "Back to events";
                            return View("Successo");
                        }
                        else
                        {
                            ViewBag.risposta = "There is an error, try again";
                            ViewBag.url = "../Associazione/Elenco_Eventi";
                            ViewBag.link = "Back to events";
                            return View("Errore");
                        }
                    }

                }
                else
                {
                    ViewBag.risposta = "There is an error, try again";
                    ViewBag.url = "../Associazione/Elenco_Eventi";
                    ViewBag.link = "Back to events";
                    return View("Errore");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.risposta = "Exception: " + ex.Message;
                ViewBag.risposta = "There is an error, try again";
                ViewBag.url = "../Associazione/Elenco_Eventi";
                ViewBag.link = "Back to events";
                return View("Successo");


            }
        }

        [HttpGet]
        public ActionResult Modifica_Riunione()
        {
            /*impedisco che l'utente inserisca l'indirizzo senza cliccare il pulsante corretto nell'area volontario*/
            ViewBag.risposta = "You don't have the privilegies to see this page";
            ViewBag.url = "../Associazione/Elenco_Riunioni";
            ViewBag.link = "Back";
            return View("Errore");
        }

        [HttpPost]
        public ActionResult Modifica_Riunione(Models.Evento model)
        {
            try
            {
                if ((ModelState.IsValid))
                {
                    DateTime Data1 = DateTime.Parse(model.data_i);
                    DateTime Data2 = DateTime.Parse(model.data_f);

                    /*Invio al server le informazioni per la modifica della riunione */
                    var webclient = new Event.EventClient();
                    Association.Associazione ass = (Association.Associazione)Session["associazione"];
                    Console.WriteLine(ass.nome);
                    Event.Evento ev = new Event.Evento();
                    Event.Luogo l = new Event.Luogo();
                    Event.Svolgimento svo = new Event.Svolgimento();
                    Event.Associazione assoc = new Event.Associazione();
                    ev.IdEv = model.IdEv;
                    ev.nome = model.nome;
                    ev.tipologia = model.tipologia;
                    ev.min_p = model.min_p;
                    ev.max_p = model.max_p;
                    ev.min_v = model.min_v;
                    ev.max_v = model.max_v;
                    ev.costo = model.costo;
                    ev.descrizione = model.descrizione;
                    assoc.IdAss = ass.IdAss;
                    assoc.nome = ass.nome;
                    assoc.citta = ass.citta;
                    assoc.stato = ass.stato;
                    assoc.via = ass.via;
                    assoc.tel = ass.tel;
                    assoc.email = ass.email;
                    assoc.password = ass.password;
                    ev.ass = assoc;
                    l.IdLuogo = model.IdLuogo;
                    l.via = model.via;
                    l.citta = model.citta;
                    l.stato = model.stato;
                    svo.evento = ev;
                    svo.luogo = l;
                    svo.data_i = model.data_i;
                    svo.data_f = model.data_f;
                    svo.ora_i = model.ora_i;
                    svo.ora_f = model.ora_f;
                    List<Event.Volontario> volontari = webclient.Event_volunteers(svo);
                    bool r = webclient.Edit_Event(svo);
                    if (r == true)
                    {
                        /*invio l'email ai partecipanti*/
                        string body = "The meeting name " + svo.evento.nome + " of " + Data1.ToString("dd/MM/yy") + " was updated by the " + svo.evento.ass.nome;
                        volontari.ForEach(vol =>
                        {
                            
                            webclient.Send_Email(vol.nome, vol.email, body, "Meeting Modified");
                        });
                        ViewBag.risposta = "Event successfully modified";
                        ViewBag.url = "../Associazione/Elenco_Riunioni";
                        ViewBag.link = "Back to events";
                        return View("Successo");
                    }
                    else
                    {
                        ViewBag.risposta = "There is an error, try again";
                        ViewBag.url = "../Associazione/Elenco_Riunioni";
                        ViewBag.link = "Back to events";
                        return View("Errore");
                    }

                }
                else
                {
                    ViewBag.risposta = "There is an error, try again";
                    ViewBag.url = "../Associazione/Elenco_Riunioni";
                    ViewBag.link = "Back to events";
                    return View("Errore");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.risposta = "Exception: " + ex.Message;
                ViewBag.risposta = "There is an error, try again";
                ViewBag.url = "../Associazione/Elenco_Riunioni";
                ViewBag.link = "Back to events";
                return View("Successo");


            }
        }

        [HttpPost]
        public ActionResult Elimina_Evento(FormCollection form)
        {
            
                try
            {
                /*Invio al server le informazion sull'evento da cancellare*/
                int id = Int32.Parse(form["idev"]);
                var webclient = new Event.EventClient();

                Event.Svolgimento e = webclient.Get_event_by_id(id);
                List<Event.Studente> studenti= webclient.Event_partecipations(e);
                List<Event.Volontario> volontari = webclient.Event_volunteers(e);
                DateTime data = DateTime.Parse(e.data_i);
                bool r = webclient.Delete_Event(e);
                if (r == true)
                {
                    /*Messaggio di output + invio mail ai partecipanti*/
                    if (e.evento.tipologia == "Riunione")
                    {
                        string body = "The meeting name "+e.evento.nome+" of " + data.ToString("dd/MM/yy") + " was canceled by the " + e.evento.ass.nome;
                        volontari.ForEach(vol =>
                        {
                            
                            webclient.Send_Email(vol.nome, vol.email, body, "Meeting Canceled");
                        });
                        ViewBag.risposta = "Meeting successfully canceled";
                        ViewBag.url = "../Associazione/Elenco_riunioni";
                        ViewBag.link = "Back to meetings";
                    }
                    else
                    {
                        string body = "The event name " + e.evento.nome + " of " + data.ToString("dd/MM/yy") + " was canceled by the " + e.evento.ass.nome;
                        studenti.ForEach(stud =>
                        {
                            webclient.Send_Email(stud.nome, stud.email, body, "Event Canceled");
                        });
                        volontari.ForEach(vol =>
                        {
                            webclient.Send_Email(vol.nome, vol.email, body, "Event Canceled");
                        });
                        ViewBag.risposta = "Event successfully canceled";
                        ViewBag.url = "../Associazione/Elenco_Eventi";
                        ViewBag.link = "Back to events";
                    }
                    return View("Successo");
                }
                else
                {
                    ViewBag.risposta = "There was an errore, Try again!";
                    ViewBag.url = "../Associazione/Elenco_Eventi";
                    ViewBag.link = "Back to list";
                    return View("Errore");
                }
            }
            catch (Exception ex)
            {
                ViewBag.risposta = "Exception: "+ ex.Message;
                ViewBag.url = "../Associazione/Elenco_Eventi";
                ViewBag.link = "Back to list";
                return View("Errore");
            }


        }

        
    }
}
