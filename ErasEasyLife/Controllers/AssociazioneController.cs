using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ErasEasyLife.Models;
using ErasEasyLife.Association;
#pragma warning disable CS0105 // La direttiva using per 'ErasEasyLife.Association' è già presente in questo spazio dei nomi
using ErasEasyLife.Association;
#pragma warning restore CS0105 // La direttiva using per 'ErasEasyLife.Association' è già presente in questo spazio dei nomi

namespace ErasEasyLife.Controllers
{
    public class AssociazioneController : Controller
    {
        // GET: Associazione
        public ActionResult Index()
        {
            return View();
        }

        // GET: Associazione/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Login()
        {
            return View("Login");
        }
       [HttpGet]
        public ActionResult Registra()
        {
            
            
                    return View();
                
        }

        [HttpPost]
        // POST: Associazione/Create
        public ActionResult Registra(Models.Associazione model)
        {
            try
            {
                if ((ModelState.IsValid))
                {// TODO: Add insert logic here

                    /*SERVER PER RESTITURIE I DATI A UNA PAGINA HTML*/
                    ViewData["Nome"] = model.nome;
                    ViewData["Citta"] = model.citta;
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
                    //ViewData["Address"] = smodel.Indirizzo;
                    //return RedirectToAction("Index");
                    ViewBag.risposta = "Sei stato registrato con successo.";
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

        // GET: Associazione/Edit/5
            public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Associazione/Edit/5
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

        // GET: Associazione/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Associazione/Delete/5
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
