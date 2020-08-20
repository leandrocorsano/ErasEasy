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
        // GET: Volontario
        public ActionResult Index()
        {
            return View();
        }

        // GET: Volontario/Details/5
        public ActionResult Profilo()
        {
            //try
            //{

            //    Volunteer.Volontario v = (Volunteer.Volontario)Session["Volontario"];
            //    var webclient = new Volunteer.VolunteerClient();
            //    Volunteer.Volontario vol = webclient.Profile(v.IdVolont);
            //    ViewData["Nome"] = vol.nome;
            //    ViewData["Cognome"] = vol.cognome;
            //     ViewData["Data di nascita"] = vol.data_n
            //    ViewData["Telefono"] = vol.telefono;
            //    ViewData["Email"] = vol.email;      
            //    ViewData["Data iscrizione"] = vol.data_iscr;
            //    ViewData["Ruolo"] = vol.ruolo;
            //    ViewData["Associazione"] = vol.ass;

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
        public ActionResult Registra()
        {
            var webclient = new Association.AssociationClient();
            //IList<Association.Associazione> associazioni = new List<Association.Associazione>();
            //List<Association.Associazione> associazioni = new List<Association.Associazione>();
             Association.Associazione [] associazioni= webclient.Show_associations("");
            //Association.Associazione [] associazioni = webclient.Show_associations("");
           /* foreach(var ass in a)
            {
                associazioni.Add(ass);
            }*/
            ViewData["associazioni"] = associazioni;
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Volontario model)
        {

            if (ModelState.IsValidField("email") && ModelState.IsValidField("password"))
            {
                /*non controllo se modelstase is valid perchè non uso tutti i membri del modello ma solo 2*/

                try
                {
                    var webclient = new Volunteer.VolunteerClient();
                    Volunteer.Volontario vol = webclient.Login(model.email, model.password);
                    if (vol != null)
                    {
                        ViewBag.risposta = "Hai effettuato il login";
                        Session["Volontario"] = vol; //passo il volontario che è entrato tra le varie pagine web


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
        // POST: Volontario/Create
        [HttpPost]
        public ActionResult Registra(Models.Volontario model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
        }

        // GET: Volontario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Volontario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Volontario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Volontario/Delete/5
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
