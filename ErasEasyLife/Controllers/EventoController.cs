using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ErasEasyLife.Controllers
{
    public class EventoController : Controller
    {
        // GET: Evento
        [HttpGet]
        public ActionResult Lista_Eventi()
        {
            DateTime oggi = DateTime.Today;
            string cond = " and data_i > '" + oggi.ToString("yyyy-MM-dd") + "' and tipologia!='riunione' order by data_i";
            var webclient = new Event.EventClient();
            Event.Svolgimento[] eventi = webclient.Show_events(cond);

            ViewData["eventi"] = eventi;
            return View();
        }
        [HttpGet]
        public ActionResult Lista_Riunioni()
        {
            DateTime oggi = DateTime.Today;
            Volunteer.Volontario vol = (Volunteer.Volontario)Session["Volontario"];
            
            string cond = " and data_i > '" + oggi.ToString("yyyy-MM-dd") + "' and tipologia ='riunione' and idass = " + vol.ass.IdAss; //"order by data_i";
            var webclient = new Event.EventClient();
            Event.Svolgimento[] riunioni = webclient.Show_events(cond);

            ViewData["riunioni"] = riunioni;
            return View();
        }
    }
}