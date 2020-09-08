//=============================================================================
// Authors: Francesca Rossi, Leandro Corsano
//=============================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ErasEasyLife.Controllers
{
    public class StudenteController : Controller
    {

        //riepilogo
        [HttpGet]
        public ActionResult Dashboard()
        {
            Student.Studente stud = (Student.Studente)Session["studente"];
            if (stud != null) //controllo se l'utente è loggato
            {
                var studclient = new Student.StudentClient();

                /*recupero le richieste e le amicizie*/
                List<Student.Studente> richieste_totali = studclient.Show_Friends(stud, "Richiesta");
                List<Student.Studente> richieste_ricevute = studclient.My_Friendship_Request(stud);
                richieste_ricevute.ForEach(x => { richieste_totali.Remove(x); }); //tengo solo le richieste che ho inviato
                List<Student.Studente> conferme = studclient.Show_Friends(stud, "Conferma");


                /*conto il numero di richieste e di amicizie*/
                ViewData["n_altre_richieste"] = richieste_ricevute.Count();
                ViewData["n_mie_richieste"] = richieste_totali.Count();
                ViewData["n_conferme"] = conferme.Count();

                /*recupero i prossimi eventi*/
                List<Student.Svolgimento> miei_eventi = studclient.Show_Event(stud.IdStud);
                ViewData["n_miei_eventi"] = miei_eventi.Count();
                var eventclient = new Event.EventClient();
                DateTime oggi = DateTime.Today;
                string cond = " and data_i > '" + oggi.ToString("yyyy-MM-dd") + "' and tipologia!='riunione' ";
                List<Event.Svolgimento> eventi = eventclient.Show_events(cond);
                ViewData["n_prossimi_eventi"] = eventi.Count();
                return View();
            }
            else
            {
                return View("Errore_Sessione");
            }
        }

        /*==============================================================
         * ISCRIZIONE STUDENTE
         * ============================================================
         */

        [HttpGet]
        public ActionResult Registra()
        {
            
            var webclient = new Student.StudentClient();
            int id = webclient.Generate_id(); //recupero l'id corretto
            ViewData["ID"] = id;
            return View();
          
        }

        [HttpPost]
        public ActionResult Registra(Models.Studente model)
        {
           
                try
                {
                    if (ModelState.IsValid)
                    {
                        var webclient = new Student.StudentClient();
                        List<Student.Studente> studenti = webclient.Show_students("");
                        bool email_presente = false; //controllo che l'email non sia già presente
                        studenti.ForEach(s => {
                            if (s.email == model.email)
                            {
                                email_presente = true;
                            }
                        });
                        if (email_presente == false)
                        {
                            Student.Studente stud = new Student.Studente();
                            stud.IdStud = model.IdStud;
                            stud.nome = model.nome;
                            stud.cognome = model.cognome;
                            stud.email = model.email;
                            stud.tel = model.tel;
                            stud.data_n = model.data_n;
                            stud.citta = model.citta;
                            stud.stato = model.stato;
                            stud.nazionalita = model.nazionalita;
                            stud.password = model.password;
                            stud.instagram = model.instagram;
                            stud.facebook = model.facebook;
                            bool r = webclient.Registration(stud);

                            ViewBag.risposta = "Successfully registered";
                            ViewBag.url = "../Studente/Login";
                            ViewBag.link = "Sign in";
                            return View("Successo");
                        }
                        else
                        {
                            ViewBag.risposta = "Email is already present in database ";
                            ViewBag.url = "../Studente/Registra";
                            ViewBag.link = "Sign up";
                            return View("Errore");
                        }
                    }
                    else
                    {
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.risposta = "Errore: " + ex.Message;
                    ViewBag.url = "../Studente/Registra";
                    ViewBag.link = "Sign out";
                    return View("Successo");
                }
            
        }

        /*========================================================
        * FUNZIONI DI ACCESSO/USCITA
        * =======================================================
        */

        [HttpGet]
        public ActionResult Login()
        {            
                return View("Login");
        }

        [HttpPost]
        public ActionResult Login(Models.Studente model)
        {

            if (ModelState.IsValidField("email") && ModelState.IsValidField("password"))
            {
                /*non controllo se modelstase is valid perchè non uso tutti i membri del modello ma solo 2*/
                try
                {
                    var webclient = new Student.StudentClient();
                    Student.Studente stud = webclient.Login(model.email, model.password);
                    if (stud != null)
                    {
                        
                        Session["Studente"] = stud; //passo lo studente che è entrato tra le varie pagine web
                        return RedirectToAction("Dashboard", "Studente");
                    }
                    else
                    {
                        ViewBag.message = "Wrong user";
                        return View();
                    }


                }
                catch(Exception ex)
                {
                    ViewBag.message = "Error: "+ex.Message;
                    return View();
                }
            }
            else
            {
                ViewBag.message = "There is an error, try again!";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Student.Studente stud = (Student.Studente)Session["studente"];
            if (stud != null) //controllo se l'utente è loggato
            {
                Session.Abandon(); //cancello la sessione corrente
                return RedirectToAction("Login", "Studente");
            }
            else
            { return View("Errore_Sessione"); }

        }

        /*========================================================
        * AREA PERSONALE
        * =======================================================
        */

        [HttpGet]
        public ActionResult Profilo()
        {
            Student.Studente stud = (Student.Studente)Session["studente"];
            if (stud != null) //controllo se l'utente è loggato
            {
                return View("Profilo");
            }
            else
            { return View("Errore_Sessione"); }
        }

        [HttpGet]
        public ActionResult Modifica_Profilo()
        {
            Student.Studente stud = (Student.Studente)Session["studente"];
            if (stud != null) //controllo se l'utente è loggato
            {
                return View("Modifica_Profilo");
            }
            else
            { return View("Errore_Sessione"); }
        }

        [HttpPost]
        public ActionResult Modifica_Profilo(Models.Studente model)
        {
            Student.Studente studente = (Student.Studente)Session["studente"];
            if (studente != null) //controllo se l'utente è loggato
            {
                try
                {
                    if (ModelState.IsValid)
                    {

                        var webclient = new Student.StudentClient();
                        Student.Studente stud = new Student.Studente();
                        stud.IdStud = model.IdStud;
                        stud.nome = model.nome;
                        stud.cognome = model.cognome;
                        stud.email = model.email;
                        stud.tel = model.tel;
                        stud.data_n = model.data_n;
                        stud.citta = model.citta;
                        stud.stato = model.stato;
                        stud.nazionalita = model.nazionalita;
                        stud.password = model.password;
                        stud.instagram = model.instagram;
                        stud.facebook = model.facebook;
                        bool r = webclient.UpdateProfile(stud);
                        if (r == true)
                        {
                            Session["Studente"] = stud;
                        }
                        ViewBag.risposta = "Profile Successfully updated";
                        ViewBag.url = "../Studente/Profilo";
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
                    ViewBag.risposta = "Error: " + ex.Message;
                    ViewBag.url = "../Studente/Modifica_Profilo";
                    ViewBag.link = "Back";
                    return View("Errore");
                }
            }
            else
            {
                return View("Errore_Sessione");
            }
        }

        [HttpGet]
        public ActionResult Modifica_Pass()
        {
            Student.Studente stud = (Student.Studente)Session["studente"];
            if (stud != null) //controllo se l'utente è loggato
            {
                return View("Modifica_Pass");
            }
            else
            { return View("Errore_Sessione"); }
        }

        [HttpPost]
        public ActionResult Modifica_Pass(Models.CambioPass model)
        {
            Student.Studente studente = (Student.Studente)Session["studente"];
            if (studente != null) //controllo se l'utente è loggato
            {
                try
                {
                    if ((ModelState.IsValid))
                    {
                        var webclient = new Student.StudentClient();
                        Student.Studente stud = (Student.Studente)Session["Studente"]; //recupero lo studente che è entrato
                        stud.password = model.nuova_pass;
                        bool r = webclient.UpdatePassword(stud.IdStud, model.nuova_pass);
                        if (r == true)
                        {
                            Session["Studente"] = stud; //creo la nuova session
                        }
                        ViewBag.risposta = "Password Successfully updated";
                        ViewBag.url = "../Studente/Profilo";
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
                    ViewBag.risposta = "Error: " + ex.Message;
                    ViewBag.url = "../Studente/Modifica_Pass";
                    ViewBag.link = "Back";
                    return View("Errore");
                }
            }
            else
            {
                return View("Errore_Sessione");
            }

        }

       /*========================================================
       * AREA UNIVERSITA'
       * =======================================================
       */

        [HttpGet]
        public ActionResult Registra_Uni()
        {
            Student.Studente stud = (Student.Studente)Session["studente"];
            if (stud != null) //controllo se l'utente è loggato
            {
                var webclient = new Student.StudentClient();
                int id = webclient.Generate_id_universita(); //recupero l'id dell'uni corretto
                ViewData["ID"] = id;
                return View();
            }
            else
            { return View("Errore_Sessione"); }
        }

        [HttpPost]
        public ActionResult Registra_Uni(Models.Universita model)
        {
            Student.Studente stud = (Student.Studente)Session["studente"];
            if (stud != null) //controllo se l'utente è loggato
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var webclient = new Student.StudentClient();
                        Student.Frequentazione f = new Student.Frequentazione();
                        Student.Universita u = new Student.Universita
                        {
                            IdUni = model.IdUni,
                            nome = model.nome,
                            citta = model.citta,
                            stato = model.stato
                        };
                        f.universita = u;
                        f.studente = stud;
                        f.tipo = model.type;
                        bool r = webclient.University_Registration(f);
                        ViewBag.risposta = "University Successfully Registered";
                        ViewBag.url = "../Studente/Lista_Uni";
                        ViewBag.link = "Go to universities list";
                        return View("Successo");

                    }
                    else
                    {
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.risposta = "Error: " + ex.Message;
                    ViewBag.url = "../Studente/Registra_Uni";
                    ViewBag.link = "Back";
                    return View("Errore");
                }
            }
            else
            { return View("Errore_Sessione"); }
        }

        [HttpGet]
        public ActionResult Lista_Uni()
        {
            Student.Studente stud = (Student.Studente)Session["studente"];
            if (stud != null) //controllo se l'utente è loggato
            {
                /*restituisce l'elenco delle universià dello studente loggato*/
                var webclient = new Student.StudentClient();
                List<Student.Frequentazione> listauni = webclient.GetUniversity(stud);
                ViewData["universita"] = listauni;
                return View();
            }
            else
            {
                return View("Errore_Session");
            }

        }

        /*========================================================
       * AREA EVENTI
       * =======================================================
       */

        [HttpGet]
        public ActionResult MieiEventi()
        {
            Student.Studente stud = (Student.Studente)Session["studente"];
            if (stud != null) //controllo se l'utente è loggato
            {
                /* restituisce gli eventi a cui ha parteciptato/parteciperà uno studente */
                var webclient = new Student.StudentClient();
                List<Student.Svolgimento> listaeventi = webclient.Show_Event(stud.IdStud);
                ViewData["eventi"] = listaeventi;
                List<Student.Frequentazione> listauni = webclient.GetUniversity(stud);
                ViewBag.uni = listauni.Count(); //controllo che lo studente abbia inserito le università
                return View();
            }
            else
            {
                return View("Errore_Sessione");
            }
        }

        [HttpGet]
        public ActionResult Partecipa()
        {
            /*impedisco che l'utente inserisca l'indirizzo senza cliccare il pulsante corretto nel dettaglio evento*/
            ViewBag.risposta = "You don't have the privilegies to see this page";
            ViewBag.url = "../Evento/Lista_Eventi";
            ViewBag.link = "Back";
            return View("Errore");
        }

        [HttpPost]
        public ActionResult Partecipa(FormCollection form)
        {
            Student.Studente stud = (Student.Studente)Session["studente"];
            if (stud != null) //controllo se la sessione è scaduta
            {
                try
                {

                    int evento = Int32.Parse(form["idev"]);
                    var studclient = new Student.StudentClient();
                    var evclient = new Event.EventClient();
                    Event.Svolgimento ev = evclient.Get_event_by_id(evento);
                    string body = "You have successfully booked the event name " + ev.evento.nome;
                    bool r = studclient.BookEvent(stud.IdStud, evento);
                    if (r == true)
                    {
                        evclient.Send_Email(stud.nome, stud.email, body, "Event Booked"); //invio dell'email allo studente
                        ViewBag.risposta = "Event successfully booked, Check yuor email.";
                        ViewBag.url = "../Evento/Lista_eventi";
                        ViewBag.link = "Back to events";
                        return View("Successo");
                    }
                    else
                    {
                        return View("~/Evento/Lista_Eventi");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return View("~/Evento/Lista_Eventi");
                }
            }
            else
            {
                return View("Errore_Sessione");
            }
        }

        [HttpGet]
        public ActionResult Elimina_Partecipazione()
        {
            /*impedisco che l'utente inserisca l'indirizzo senza cliccare il pulsante corretto nel dettaglio evento*/
            ViewBag.risposta = "You don't have the privilegies to see this page";
            ViewBag.url = "../Evento/Lista_Eventi";
            ViewBag.link = "Back";
            return View("Errore");
        }

        [HttpPost]
        public ActionResult Elimina_partecipazione(FormCollection form)
        {
            Student.Studente stud = (Student.Studente)Session["studente"];
            if (stud != null) //controllo se la sessione è scaduta
            {
                try
                {
                    int evento = Int32.Parse(form["idev"]);
                    var webclient = new Student.StudentClient();
                    bool r = webclient.CancelBooking(stud.IdStud, evento);
                    if (r == true)
                    {
                        ViewBag.risposta = "Booking successfully cancelled";
                        ViewBag.url = "../Evento/Lista_eventi";
                        ViewBag.link = "Back to events";
                        return View("Successo");
                    }
                    else
                    {
                        ViewBag.risposta = "There was an error, try again";
                        ViewBag.url = "../Evento/Lista_eventi";
                        ViewBag.link = "Back to events";
                        return View("Errore");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.risposta = "Error :" + ex.Message;
                    ViewBag.url = "../Evento/Lista_eventi";
                    ViewBag.link = "Back to events";
                    return View("Successo");
                }
            }
            else
            {
                return View("Errore_Sessione");
            }
        }

       /*========================================================
       * GESTIONE COMMUNITY
       * =======================================================
       */

        [HttpGet]
        public ActionResult Elenco()
        {
            Student.Studente stud = (Student.Studente)Session["studente"];
            if (stud != null) //controllo se la sessione è scaduta
            {
                /*Pagina che permette di selezionare la nazionalità degli studenti da mostrare*/
                var webclient = new Student.StudentClient();
                List<Student.Frequentazione> listauni = webclient.GetUniversity(stud);
                ViewBag.uni = listauni.Count(); //controllo che l'utente abbia registrato le università
                return View();
            }
            else
            {
                return View("Errore_Sessione");
            }
        }

        [HttpPost]
        public ActionResult Elenco(Models.Studente model)
        {
            Student.Studente stud = (Student.Studente)Session["studente"];
            if (stud != null) //controllo se la sessione è scaduta
            {
                if (ModelState.IsValidField("nazionalita"))
                {
                    /* Funziona che mostra l'elenco delgi studenti di una certa nazionalità
                     * e le relative richieste inviate, ricevute o amicizie confermate*/
                    string cond = "idstud!=" + stud.IdStud + " and nazionalita='" + model.nazionalita + "'";
                    var webclient = new Student.StudentClient();
                    List<Student.Studente> studenti = webclient.Show_students(cond); //elenco di tutti gli studenti si una determinata nazionalita

                    List<Student.Studente> richieste_totali = webclient.Show_Friends(stud, "Richiesta");
                    List<Student.Studente> richieste_ricevute = webclient.My_Friendship_Request(stud);
                    richieste_ricevute.ForEach(x => { richieste_totali.Remove(x); }); //tengo solo le richieste che ho inviato

                    List<Student.Studente> conferme = webclient.Show_Friends(stud, "Conferma");
                    List<Student.Studente> amicizie_rifiutate = webclient.Show_Friends(stud, "Rifiutata");
                    ViewData["studenti"] = studenti;
                    ViewData["altri_richieste"] = richieste_ricevute;
                    ViewData["mie_richieste"] = richieste_totali;
                    ViewData["conferme"] = conferme;
                    ViewData["rifiutate"] = amicizie_rifiutate;
                    /*controllo che l'utente abbia inserito le università*/
                    List<Student.Frequentazione> listauni = webclient.GetUniversity(stud);
                    ViewBag.uni = listauni.Count();
                    return View();
                }
                return View(); //se il form non è compilato bene
            }
            else
            {
                return View("Errore_Sessione");
            }
            }

        [HttpGet]
        public ActionResult Richieste_Amicizia()
        {
            Student.Studente stud = (Student.Studente)Session["studente"];
            if (stud != null) //controllo se la sessione è scaduta
            {
                try
                {

                    var webclient = new Student.StudentClient();
                    List<Student.Studente> richieste = webclient.My_Friendship_Request(stud);
                    ViewData["richieste"] = richieste; //elenco richieste
                    List<Student.Frequentazione> listauni = webclient.GetUniversity(stud);
                    ViewBag.uni = listauni.Count();
                    return View();

                }
                catch (Exception ex)
                {
                    ViewBag.risposta = "Error: " + ex.Message;
                    ViewBag.url = "../Studente/Elenco";
                    ViewBag.link = "Back to students list";
                    return View("Errore");
                }
            }
            else
            { return View("Errore_Sessione"); }
        }

        [HttpGet]
        public ActionResult Richiesta_Amicizia()
        {
            /*impedisco che l'utente inserisca l'indirizzo senza cliccare il pulsante per la richiesta*/
            ViewBag.risposta = "You don't have the privilegies to see this page";
            ViewBag.url = "../Studente/Elenco";
            ViewBag.link = "Back to student list";
            return View("Errore");
        }

        [HttpPost]
        public ActionResult Richiesta_Amicizia(FormCollection form)
        {
            Student.Studente stud = (Student.Studente)Session["studente"];
            if (stud != null) //controllo se la sessione è scaduta
            {
                try
                {
                    /*funzione che si occupa di inviare una richiesta di amicizia*/
                    int stud2 = Int32.Parse(form["idstud"]);
                    var webclient = new Student.StudentClient();
                    bool r = webclient.Friendship_Request(stud.IdStud, stud2);
                    if (r == true)
                    {
                        ViewBag.risposta = "Friend request succesfully sent";
                        ViewBag.url = "../Studente/Elenco";
                        ViewBag.link = "Back to students list";
                        return View("Successo");
                    }
                    else
                    {
                        return View("Elenco");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return View("Elenco");

                }
            }
            else
            {
                return View("Errore_Sessione");
            }
        }

        [HttpGet]
        public ActionResult Annulla_Richiesta()
        {
            /*impedisco che l'utente inserisca l'indirizzo senza cliccare il pulsante corretto nell'area volontario*/
            ViewBag.risposta = "You don't have the privilegies to see this page";
            ViewBag.url = "../Studente/Elenco";
            ViewBag.link = "Back to student list";
            return View("Errore");
        }

        [HttpPost]
        public ActionResult Annulla_Richiesta(FormCollection form)
        {
            Student.Studente stud = (Student.Studente)Session["studente"];
            if (stud != null) //controllo se la sessione è scaduta
            {
                try
                {
                    int stud2 = Int32.Parse(form["idstud"]);
                    var webclient = new Student.StudentClient();
                    bool r = webclient.Delete_Friendship(stud.IdStud, stud2);
                    if (r == true)
                    {
                        ViewBag.risposta = "Friend Request cancelled!";
                        ViewBag.url = "../Studente/Elenco";
                        ViewBag.link = "Back to list";
                        return View("Successo");
                    }
                    else
                    {
                        ViewBag.risposta = "There was an error, try again";
                        ViewBag.url = "../Studente/Elenco";
                        ViewBag.link = "Back to list";
                        return View("Errore");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.risposta = "Error: " + ex.Message;
                    ViewBag.url = "../Studente/Elenco";
                    ViewBag.link = "Back to list";
                    return View("Errore");
                }
            }
            else
            {
                return View("Errore_Sessione");
            }
        }

        [HttpGet]
        public ActionResult Conferma_Amicizia()
        {
            /*impedisco che l'utente inserisca l'indirizzo senza cliccare il pulsante corretto nell'area volontario*/
            ViewBag.risposta = "You don't have the privilegies to see this page";
            ViewBag.url = "../Studente/Elenco";
            ViewBag.link = "Back to student list";
            return View("Errore");
        }

        [HttpPost]
        public ActionResult Conferma_Amicizia(FormCollection form)
        {
            Student.Studente stud = (Student.Studente)Session["studente"];
            if (stud != null) //controllo se la sessione è scaduta
            {
                try
                {
                    int stud1 = Int32.Parse(form["idstud"]);
                    var webclient = new Student.StudentClient();
                    bool r = webclient.Friendship_State(stud1, stud.IdStud, "Conferma");
                    if (r == true)
                    {
                        ViewBag.risposta = "Friend request confirmed!";
                        ViewBag.url = "../Studente/Elenco";
                        ViewBag.link = "Back to students list";
                        return View("Successo");
                    }
                    else
                    {
                        ViewBag.risposta = "There was an error, try again";
                        ViewBag.url = "../Studente/Richieste_Amicizia";
                        ViewBag.link = "Back to request list";
                        return View("Errore");
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    ViewBag.risposta = "Error: " + ex.Message;
                    ViewBag.url = "../Studente/Richieste_Amicizia";
                    ViewBag.link = "Back to request list";
                    return View("Errore");
                }
            }
            else
            {
                return View("Errore_Sessione");
            }
        }

        [HttpGet]
        public ActionResult Rifiuta_Amicizia()
        {
            /*impedisco che l'utente inserisca l'indirizzo senza cliccare il pulsante corretto nell'area volontario*/
            ViewBag.risposta = "You don't have the privilegies to see this page";
            ViewBag.url = "../Studente/Elenco";
            ViewBag.link = "Back to student list";
            return View("Errore");
        }

        [HttpPost]
        public ActionResult Rifiuta_Amicizia(FormCollection form)
        {
            Student.Studente stud = (Student.Studente)Session["studente"];
            if (stud != null) //controllo se la sessione è scaduta
            {
                try
                {
                    int stud1 = Int32.Parse(form["idstud"]);
                    var webclient = new Student.StudentClient();
                    bool r = webclient.Friendship_State(stud1, stud.IdStud, "Rifiutata");
                    if (r == true)
                    {
                        ViewBag.risposta = "Friend request denied";
                        ViewBag.url = "../Studente/Richieste_Amicizia";
                        ViewBag.link = "Back to requests";
                        return View("Successo");
                    }
                    else
                    {
                        ViewBag.risposta = "There was an error, try again!";
                        ViewBag.url = "../Studente/Richieste_Amicizia";
                        ViewBag.link = "Back to requests";
                        return View("Errore");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    ViewBag.risposta = "Error: " + ex.Message;
                    ViewBag.url = "../Studente/Richieste_Amicizia";
                    ViewBag.link = "Back to requests";
                    return View("Errore");
                }
            }
            else
            {
                return View("Errore_Sessione");
            }
        }
       
        [HttpGet]
        public ActionResult Elenco_Amici()
        {
            Student.Studente stud = (Student.Studente)Session["studente"];
            if (stud != null) //controllo se l'utente si è loggato
            {
                /*Elenco degli amici dello studente loggato*/
                var webclient = new Student.StudentClient();
                List<Student.Studente> amici = webclient.Show_Friends(stud, "Conferma");
                ViewData["amici"] = amici;
                List<Student.Frequentazione> listauni = webclient.GetUniversity(stud);
                ViewBag.uni = listauni.Count(); //controllo che abbia inserito 2 unversità
                return View();
            }
            else
            {
                return View("Errore_Sessione");
            }
      
        }

        [HttpGet]
        public ActionResult Le_mie_richieste()
        {
            Student.Studente stud = (Student.Studente)Session["studente"];
            if (stud != null) //controllo se la sessione è scaduta
            {
                /*Richieste ho inviato*/
                var webclient = new Student.StudentClient();
                List<Student.Frequentazione> listauni = webclient.GetUniversity(stud);
                ViewBag.uni = listauni.Count();
                List<Student.Studente> richieste_totali = webclient.Show_Friends(stud, "Richiesta");
                List<Student.Studente> richieste_ricevute = webclient.My_Friendship_Request(stud);
                richieste_ricevute.ForEach(x => { richieste_totali.Remove(x); }); //tengo solo le richieste che ho inviato
                ViewData["richieste"] = richieste_totali;
                return View();
            }
            else
            { return View("Errore_Sessione");  }
        }
    }
}
