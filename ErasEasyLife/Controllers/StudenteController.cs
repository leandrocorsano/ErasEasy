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
        public ActionResult Details(int id)
        {
            return View();
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

        // POST: Studente/Create
        [HttpPost]
        public ActionResult Registra(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Studente/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Studente/Edit/5
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
