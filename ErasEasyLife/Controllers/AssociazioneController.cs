using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ErasEasyLife.Models;
using ErasEasyLife.Association;

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
            return View();
        }


        // POST: Associazione/Registra
        [HttpPost]
        public ActionResult Registra(Association.Associazione model)
        {
            try
            {

                if (ModelState.IsValid)
                {// TODO: Add insert logic here

                    /*SERVER PER RESTITURIE I DATI A UNA PAGINA HTML*/
                    ViewData["Nome"] = model.nome;
                    ViewData["Citta"] = model.citta;
                    var webclient = new Association.AssociationClient();
                    //Associazione ass = new Associazione(model.IdAss, model.nome, model.citta, model.stato, model.via, model.tel, model.email, model.password);
                    bool r = webclient.Registration(model);
                    //ViewData["Address"] = smodel.Indirizzo;
                    //return RedirectToAction("Index");
                    return View("Index");
                }
                else
                {
                    return View("Registra");
                }
            }
            catch
            {
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
