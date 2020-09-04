using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ErasEasyLife.Controllers
{
    public class StudenteController : Controller
    {
        // GET: Studente
        public ActionResult Index()
        {

            return View();
        }

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
           
                
                return View();
            }
         

        [HttpGet]
        // GET: Studente/Create
        public ActionResult Registra()
        {
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
                List<Student.Studente> mie_richieste = webclient.Show_Friends(stud, "Richiesta");
                List<Student.Studente> altri_richieste = webclient.My_Friendship_Request(stud);
                List<Student.Studente> conferme = webclient.Show_Friends(stud, "Conferma");
                ViewData["studenti"] = studenti;
                ViewData["altri_richieste"] = altri_richieste;
                ViewData["mie_richieste"] = mie_richieste;
                ViewData["conferme"] = conferme;
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


                        return View("Profilo");
                    }
                    else
                    {
                        ViewBag.risposta = "Wrong user";
                        ViewBag.url = "Login";
                        ViewBag.link = "Accedi";
                        return View("Errore");
                    }


                }
                catch
                {
                    ViewBag.risposta = "Wrong user";
                    ViewBag.url = "Login";
                    ViewBag.link = "Accedi";
                    return View("Errore");
                }
            }
            else
            {
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
        [HttpGet]
        public ActionResult Richieste_Amicizia()
        {
            try
            {
                
                Student.Studente stud = (Student.Studente)Session["studente"];
                var webclient = new Student.StudentClient();
                List<Student.Studente> richieste = webclient.My_Friendship_Request(stud);
                ViewData["richieste"] = richieste;
                return View("");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("");
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


        // GET: Studente/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Studente/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
