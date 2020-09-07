using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ErasEasyLife.Controllers
{
    public class StudenteController : Controller
    {
       

        // GET: Studente/Details/5
        public ActionResult Profilo()
        {
            //try
            //{

            //    Student.Studente s = (Student.Studente)Session["Studente"];
            //    var webclient = new Student.StudentClient();
            //    Student.Studente stud = webclient.Profile(s.IdStud);
            //    ViewData["Nome"] = stud.nome;
            //    ViewData["Cognome"] = stud.cognome;
            //     ViewData["Data di nascita"] = stud.data_n
            //    ViewData["Citta"] = stud.citta;
            //    ViewData["Stato"] = stud.stato;
            //    ViewData["Nazionalità"] = stud.nazionalita;
            //    ViewData["Telefono"] = stud.tel;
            //    ViewData["Email"] = stud.email;      
            //    ViewData["Instagram"] = stud.instagram;
            //    ViewData["Facebook"] = stud.facebook;

            return View("Profilo");



            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    return View();
            //}
        }

        public ActionResult Login()
        {
            return View("Login");
        }
        [HttpGet]
        // GET: Volontario/Create
        public ActionResult Elenco()
        {
            var webclient = new Student.StudentClient();
            Student.Studente stud = (Student.Studente)Session["Studente"];
            List<Student.Frequentazione> listauni = webclient.GetUniversity(stud);
            ViewBag.uni = listauni.Count();
            return View();
            }
         

        [HttpGet]
        // GET: Studente/Create
        public ActionResult Registra()
        {
            var webclient = new Student.StudentClient();
            int id = webclient.Generate_id();
            ViewData["ID"] = id;
            return View();
        }

        [HttpPost]
        // GET: Volontario/Create
        public ActionResult Elenco(Models.Studente model)
        {
            if (ModelState.IsValidField("nazionalita"))
            {
                
                Student.Studente stud = (Student.Studente)Session["Studente"];
                string cond = "idstud!=" + stud.IdStud+" and nazionalita='" + model.nazionalita + "'";
                var webclient = new Student.StudentClient();
                List<Student.Studente> studenti = webclient.Show_students(cond);
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
               
                List<Student.Frequentazione> listauni = webclient.GetUniversity(stud);
                ViewBag.uni = listauni.Count();
                return View();
            }
            return View();
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
                        ViewBag.risposta = "Successfully signed in";
                        Session["Studente"] = stud; //passo lo studente che è entrato tra le varie pagine web


                        return RedirectToAction("Dashboard", "Studente");
                    }
                    else
                    {
                        ViewBag.message = "Wrong user";
                        return View();
                    }


                }
                catch
                {
                    ViewBag.message = "There is an error, try again!";
                    return View();
                }
            }
            else
            {
                ViewBag.message = "There is an error, try again!";
                return View();
            }
        }



        // POST: Studente/Create
        [HttpPost]
        public ActionResult Registra(Models.Studente model)
        {
            try
            {
                if (ModelState.IsValid)
                {// TODO: Add insert logic here

                    ViewData["Nome"] = model.nome;
                    ViewData["Cognome"] = model.cognome;
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

                    bool r = webclient.Registration(stud);

                    ViewBag.risposta = "Successfully registered";
                    ViewBag.url = "Login";
                    ViewBag.link = "Accedi";

                    return View("Successo");

                }

                else
                {
                    return View();
                }

                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
        }
        [HttpGet]

        public ActionResult Logout()
        {

            Session.Abandon();
            return RedirectToAction("Login", "Studente");

        }
        [HttpGet]
        public ActionResult Registra_Uni()
        {
            var webclient = new Student.StudentClient();
            int id = webclient.Generate_id_universita();
            ViewData["ID"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult Registra_Uni(Models.Universita model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var webclient = new Student.StudentClient();
                    Student.Studente stud = (Student.Studente)Session["Studente"];
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

                    ViewBag.risposta = "Successfully registered";

                    return View("Successo");
            
                }
                else
                {
                    return View();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
        }

        [HttpGet]
        public ActionResult Lista_Uni()
        {
            var webclient = new Student.StudentClient();
            Student.Studente stud = (Student.Studente)Session["Studente"];
            List<Student.Frequentazione> listauni = webclient.GetUniversity(stud);
            ViewData["universita"] = listauni;
            return View();

        }
        // GET: Studente/Edit/5
        [HttpGet]
        public ActionResult Modifica_Profilo()
        {
            return View("Modifica_Profilo");
        }

        // POST: Studente/Edit/5
        [HttpPost]
        public ActionResult Modifica_Profilo(Models.Studente model)
        {
            try
            {
                if (ModelState.IsValid)
                {// TODO: Add insert logic here

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
                    Session["Studente"] = stud;

                    bool r = webclient.UpdateProfile(stud);

                    ViewBag.risposta = "Profile successfully updated";
                    return View("Modifica_Profilo");

                }
                else
                {
                    return View();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
        }
        [HttpGet]
        public ActionResult MieiEventi()
        {
            var webclient = new Student.StudentClient();
            Student.Studente stud = (Student.Studente)Session["Studente"];
            List<Student.Svolgimento> listaeventi = webclient.Show_Event(stud.IdStud);
            ViewData["eventi"] = listaeventi;
            List<Student.Frequentazione> listauni = webclient.GetUniversity(stud);
            ViewBag.uni = listauni.Count();
            return View();
        }
        [HttpPost]
        public ActionResult Partecipa(FormCollection form)
        {
            try
            {
                int evento = Int32.Parse(form["idev"]);
                Student.Studente stud = (Student.Studente)Session["studente"];
                var webclient = new Student.StudentClient();
                bool r = webclient.BookEvent(stud.IdStud, evento );
                if(r==true)
                {
                    ViewBag.risposta = "Event successfully booked";
                    ViewBag.url = "../Evento/Lista_eventi";
                    ViewBag.link = "Torna agli eventi";
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
        [HttpPost]
        public ActionResult Elimina_partecipazione(FormCollection form)
        {
            try
            {
                int evento = Int32.Parse(form["idev"]);
                Student.Studente stud = (Student.Studente)Session["studente"];
                var webclient = new Student.StudentClient();
                bool r = webclient.CancelBooking(stud.IdStud, evento);
                if (r == true)
                {
                    ViewBag.risposta = "Booking successfully cancelled";
                    ViewBag.url = "../Evento/Lista_eventi";
                    ViewBag.link = "Torna agli eventi";
                    return View("Successo");
                }
                else
                {
                    return View("Evento/lista_Eventi");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Evento/lista_Eventi");
            }
        }
        [HttpPost]
        public ActionResult Richiesta_Amicizia(FormCollection form)
        {
            try
            {
                int stud2 = Int32.Parse(form["idstud"]);
                Student.Studente stud = (Student.Studente)Session["studente"];
                var webclient = new Student.StudentClient();
                bool r = webclient.Friendship_Request(stud.IdStud, stud2);
                if(r == true)
                {
                    ViewBag.risposta = "Friend request succesfully sent";
                    ViewBag.url = "../Studente/Elenco";
                    ViewBag.link = "Torna all'elenco studenti";
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
        [HttpPost]
        public ActionResult Annulla_Richiesta(FormCollection form)
        {
            try
            {
                int stud2= Int32.Parse(form["idstud"]);
                Student.Studente stud = (Student.Studente)Session["studente"];
                var webclient = new Student.StudentClient();
                bool r = webclient.Delete_Friendship(stud.IdStud, stud2);
                if (r == true)
                {
                    ViewBag.risposta = "Hai annullato la richiesta d'amicizia";
                    ViewBag.url = "../Studente/Elenco";
                    ViewBag.link = "Torna all'elenco";
                    return View("Successo");
                }
                else
                {
                    ViewBag.risposta = "Qualcosa è andato storto, riprova!";
                    ViewBag.url = "../Studente/Elenco";
                    ViewBag.link = "Torna all'elenco";
                    return View("Errore");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.risposta = "Qualcosa è andato storto, riprova!";
                ViewBag.url = "../Studente/Elenco";
                ViewBag.link = "Torna all'elenco";
                return View("Errore");
            }
        }
        [HttpPost]
        public ActionResult Conferma_Amicizia(FormCollection form)
        {
            try
            {
                int stud1 = Int32.Parse(form["idstud"]);
                Student.Studente stud = (Student.Studente)Session["studente"];
                var webclient = new Student.StudentClient();
                bool r = webclient.Friendship_State(stud1, stud.IdStud, "Conferma");
                if (r == true)
                {
                    ViewBag.risposta = "Friend request confirmed!";
                    ViewBag.url = "../Studente/Elenco";
                    ViewBag.link = "Torna all'elenco studenti";
                    return View("Successo");
                }
                else
                {
                    return View("Richieste_Amicizia");
                }
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Richieste_Amicizia");
            }
        }
        [HttpPost]
        public ActionResult Rifiuta_Amicizia(FormCollection form)
        {
            try
            {
                int stud1 = Int32.Parse(form["idstud"]);
                Student.Studente stud = (Student.Studente)Session["studente"];
                var webclient = new Student.StudentClient();
                bool r = webclient.Friendship_State(stud1, stud.IdStud, "Rifiutata");
                if (r == true)
                {
                    ViewBag.risposta = "Richiesta di amicizia rifiutata";
                    ViewBag.url = "../Studente/Elenco";
                    ViewBag.link = "Torna alle richieste";
                    return View("Successo");
                }
                else
                {
                    ViewBag.risposta = "Qualcosa è andato storto, riprova!";
                    ViewBag.url = "../Studente/Elenco";
                    ViewBag.link = "Torna all'elenco";
                    return View("Errore");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.risposta = "Qualcosa è andato storto, riprova!";
                ViewBag.url = "../Studente/Elenco";
                ViewBag.link = "Torna all'elenco";
                return View("Errore");
            }
        }

        [HttpGet]
        public ActionResult Richieste_Amicizia()
        {
            try
            {
                
                Student.Studente stud = (Student.Studente)Session["studente"];
                var webclient = new Student.StudentClient();
                List<Student.Studente> richieste = webclient.My_Friendship_Request(stud);
                ViewData["richieste"] = richieste;
                List<Student.Frequentazione> listauni = webclient.GetUniversity(stud);
                ViewBag.uni = listauni.Count();
                return View("");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("");
            }
        }
        [HttpGet]
        public ActionResult Elenco_Amici()
        {
            Student.Studente stud = (Student.Studente)Session["studente"];
            var webclient = new Student.StudentClient();
            List<Student.Studente> amici = webclient.Show_Friends(stud, "Conferma");
            ViewData["amici"] = amici;
            List<Student.Frequentazione> listauni = webclient.GetUniversity(stud);
            ViewBag.uni = listauni.Count();
            return View();
      
        }
        [HttpGet]
        public ActionResult Le_mie_richieste()
        {
            Student.Studente stud = (Student.Studente)Session["studente"];
            var webclient = new Student.StudentClient();
            List<Student.Frequentazione> listauni = webclient.GetUniversity(stud);
            ViewBag.uni = listauni.Count();
            List<Student.Studente> richieste_totali = webclient.Show_Friends(stud, "Richiesta");
            List<Student.Studente> richieste_ricevute = webclient.My_Friendship_Request(stud);
            richieste_ricevute.ForEach(x => { richieste_totali.Remove(x); }); //tengo solo le richieste che ho inviato
            

                ViewData["richieste"] = richieste_totali;

                return View();

            
        }
        [HttpGet]
        public ActionResult Modifica_Pass()
        {
            return View("Modifica_Pass");
        }
        [HttpGet]
        public ActionResult Dashboard()
        {
            var studclient = new Student.StudentClient();
            Student.Studente stud = (Student.Studente)Session["studente"];
            List<Student.Studente> richieste_totali = studclient.Show_Friends(stud, "Richiesta");
            List<Student.Studente> richieste_ricevute = studclient.My_Friendship_Request(stud);
            richieste_ricevute.ForEach(x => { richieste_totali.Remove(x); }); //tengo solo le richieste che ho inviato
            List<Student.Studente> conferme = studclient.Show_Friends(stud, "Conferma");
            List<Student.Svolgimento> miei_eventi = studclient.Show_Event(stud.IdStud);

            ViewData["n_altre_richieste"] = richieste_ricevute.Count();
            ViewData["n_mie_richieste"] = richieste_totali.Count();
            ViewData["n_conferme"] = conferme.Count();
            ViewData["n_miei_eventi"] = miei_eventi.Count();

            var eventclient = new Event.EventClient();
            DateTime oggi = DateTime.Today;
            string cond = " and data_i > '" + oggi.ToString("yyyy-MM-dd") + "' and tipologia!='riunione' ";
            List<Event.Svolgimento> eventi = eventclient.Show_events(cond);
            ViewData["n_prossimi_eventi"] = eventi.Count();
            return View();
        }
        [HttpPost]
        public ActionResult Modifica_Pass(Models.CambioPass model)
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
                Console.WriteLine(ex.Message);
                return View();
            }

        }


       
    }
}
