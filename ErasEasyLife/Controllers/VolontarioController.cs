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
        [HttpGet]
        public ActionResult Modifica_Profilo()
        {
            return View("Modifica_Profilo");
        }

        // POST: Volontario/Edit/5
        [HttpPost]
        public ActionResult Modifica_Profilo(Models.Volontario model)
        {
            try
            {
                if (ModelState.IsValid)
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
                    Session["Volontario"] = vol;

                    bool r = webclient.UpdateProfile(vol);

                    ViewBag.risposta = "Hai modificato i dati con successo";

                    return View("Modifica_Profilo");

                }
                // TODO: Add update logic here
                else
                {
                    /* ViewBag.risposta = "Non hai modificato i dati";
                     ViewBag.Url = "Profilo";
                     ViewBag.link = */
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
                    ViewBag.risposta = "Hai cambiato password con successo";
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
        [HttpGet]
        public ActionResult Elenco()
        {
            return View();
        }
        [HttpPost]
        // GET: Volontario/Create
        public ActionResult Elenco(Models.Volontario model)
        {
            if (ModelState.IsValidField("ass"))
            {
                var webclient = new Volunteer.VolunteerClient();
                Volunteer.Volontario vol = (Volunteer.Volontario)Session["Volontario"];
                string cond = "idvolont!" + vol.IdVolont + " and ass='" + model.ass + "'";
                //Volunteer.Associazione ass = webclient.GetAssociazione(vol.IdVolont);
                //ViewData["Associazione"] = ass;
                Volunteer.Volontario[] volontari = webclient.Show_volontari(cond);

                ViewData["volontari"] = volontari;
                return View();
            }
            return View();
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
