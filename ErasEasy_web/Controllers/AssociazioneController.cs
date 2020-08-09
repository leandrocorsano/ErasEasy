using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ErasEasy_web.Models;

namespace ErasEasy_web.Controllers
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

        // GET: Associazione/Registra
        public ActionResult Registra(int id)
        {
            return View();
        }
        // GET: Associazione/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Associazione/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]

        public ActionResult provaAssociazione(AssociazioneModel model)
        {

            ViewBag.Message = "Salve " + model.nome + " " + model.email + ";";
            return View("Result");
        }

        // GET: Associazione/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Associazione/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
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
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}