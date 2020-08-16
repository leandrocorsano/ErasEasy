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
        public ActionResult Details(int id)
        {
            return View();
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
