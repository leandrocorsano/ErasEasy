using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Common;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.Geocoding.Response;
using GoogleMapsApi.StaticMaps;
using GoogleMapsApi.StaticMaps.Entities;



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
            var webclient1 = new Association.AssociationClient();
            Association.Associazione[] associazioni = webclient1.Show_associations("");
            List<Event.Svolgimento> eventi = webclient.Show_events(cond);

            ViewData["eventi"] = eventi;
            ViewData["associazioni"] = associazioni;
            return View();
        }
        [HttpPost]
        public ActionResult Lista_Eventi(Models.Associazione model)
        {
            if (ModelState.IsValidField("IdAss"))
            {
                DateTime oggi = DateTime.Today;
                string cond;
                if (model.IdAss == 0)
                {
                    cond = " and data_i > '" + oggi.ToString("yyyy-MM-dd") + "' and tipologia!='riunione' order by data_i";
                }
                else
                {
                    
                    cond = " and data_i > '" + oggi.ToString("yyyy-MM-dd") + "' and tipologia!='riunione' and idass="+model.IdAss+" order by data_i";

                }
                var webclient = new Event.EventClient();
               
                List<Event.Svolgimento> eventi = webclient.Show_events(cond);
                ViewData["eventi"] = eventi;
                var webclient1 = new Association.AssociationClient();
                Association.Associazione[] associazioni = webclient1.Show_associations("");
                ViewData["associazioni"] = associazioni;
                return View();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Dettagli(FormCollection form)
        {

            int id = Int32.Parse(form["idev"]);
                var webclient = new Event.EventClient();

              Event.Svolgimento e = webclient.Get_event_by_id(id);
              List<Event.Studente> studenti = webclient.Event_partecipations(e);
              ViewData["evento"] = e;
              ViewData["studenti"] = studenti;


           
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