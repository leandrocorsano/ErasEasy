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
        // GET: Studente/Create
        public ActionResult Registra()
        {
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
                        ViewBag.risposta = "Hai effettuato il login";
                        Session["Studente"] = stud; //passo lo studente che è entrato tra le varie pagine web


                        return View("Profilo");
                    }
                    else
                    {
                        ViewBag.risposta = "Utente errato";
                        ViewBag.url = "Login";
                        ViewBag.link = "Accedi";
                        return View("Errore");
                    }


                }
                catch
                {
                    ViewBag.risposta = "Utente errato";
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

                    ViewBag.risposta = "Sei stato registrato con successo";
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

                    ViewBag.risposta = "Hai modificato i dati con successo";
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
