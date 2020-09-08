//=============================================================================
// Authors: Francesca Rossi, Leandro Corsano
//=============================================================================
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
        /*
         *  CONTROLLER PER LA GESTIONE DI EVENTI NELL'AREA STUDENTE E RIUNIONI
         *  NELL'AREA VOLONTARIO
         */

        [HttpGet]
        public ActionResult Lista_Eventi()
        {

            DateTime oggi = DateTime.Today; // mi salvo la data attuale per prendere gli eventi successivi
            string cond = " and data_i > '" + oggi.ToString("yyyy-MM-dd") + "' and tipologia!='meeting' order by data_i";
            var webclient = new Event.EventClient();
            var webclient1 = new Association.AssociationClient();
            List<Association.Associazione> associazioni = webclient1.Show_associations("");
            List<Event.Svolgimento> eventi = webclient.Show_events(cond);
            Student.Studente stud = (Student.Studente)Session["studente"];
            var studclient = new Student.StudentClient();
            List<Student.Frequentazione> listauni = studclient.GetUniversity(stud);
            ViewBag.uni = listauni.Count(); //Controllo in numero di uni, per vedere se lo studente può entrare
            ViewData["eventi"] = eventi;
            ViewData["associazioni"] = associazioni; //lo metterò nel select
            return View();
        }
        
        [HttpPost]
        public ActionResult Lista_Eventi(Models.Associazione model)
        {
            if (ModelState.IsValidField("IdAss"))
            {
                DateTime oggi = DateTime.Today;
                string cond;
                if (model.IdAss == 0) //l'utente ha selezionato "tutte le associazioni"
                {
                    cond = " and data_i > '" + oggi.ToString("yyyy-MM-dd") + "' and tipologia!='meeting' order by data_i";
                }
                else
                {
                    
                    cond = " and data_i > '" + oggi.ToString("yyyy-MM-dd") + "' and tipologia!='meeting' and idass="+model.IdAss+" order by data_i";

                }
                var webclient = new Event.EventClient();           
                List<Event.Svolgimento> eventi = webclient.Show_events(cond);
                ViewData["eventi"] = eventi;
                var webclient1 = new Association.AssociationClient();
                List<Association.Associazione> associazioni = webclient1.Show_associations("");
                ViewData["associazioni"] = associazioni; //mi serve per ricaricare la select al ricaricamento della pagina
                Student.Studente stud = (Student.Studente)Session["studente"];
                var studclient = new Student.StudentClient();
                List<Student.Frequentazione> listauni = studclient.GetUniversity(stud);
                ViewBag.uni = listauni.Count(); //controllo che lo studente abbia il permesso per accedervi
                return View();
            }
            return View(); //ritorno la pagina del get con gli errori sul model
        }
        
        [HttpGet]
        public ActionResult Lista_Riunioni()
        {
            /* mostra le  riunioni dell'associazione di cui fa parte il volontario loggato*/
            DateTime oggi = DateTime.Today;
            Volunteer.Volontario vol = (Volunteer.Volontario)Session["Volontario"]; // dal volontario loggato poi recupero l'associazione in automatico
            string cond = " and data_i > '" + oggi.ToString("yyyy-MM-dd") + "' and tipologia ='meeting' and idass = " + vol.ass.IdAss + "  order by data_i";
            var webclient = new Event.EventClient();
            List<Event.Svolgimento> riunioni = webclient.Show_events(cond);
            ViewData["riunioni"] = riunioni;
            return View();
        }

        [HttpGet]
        public ActionResult Dettagli()
        {
            /*impedisco che l'utente inserisca l'indirizzo senza cliccare il pulsante corretto di dettaglio evento*/
            ViewBag.risposta = "You don't have the privilegies to see this page";
            ViewBag.url = "../Studente/Lista_Eventi";
            ViewBag.link = "Back";
            return View("Errore");
        }

        [HttpPost]
        public ActionResult Dettagli(FormCollection form)
        {
            /*Dato l'id restituisce i dettagli di un evento, nell'area dello studente */
            int id = Int32.Parse(form["idev"]);
            var webclient = new Event.EventClient();
            Event.Svolgimento e = webclient.Get_event_by_id(id);
            List<Event.Studente> studenti = webclient.Event_partecipations(e);
            ViewData["evento"] = e;
            ViewData["studenti"] = studenti; //tutti gli studenti partecipanti, compreso quello che è entrato
            return View();
            
           
        }

        [HttpGet]
        public ActionResult Dettagli_Riunione()
        {
            /*impedisco che l'utente inserisca l'indirizzo senza cliccare il pulsante corretto di dettaglio riunione*/
            ViewBag.risposta = "You don't have the privilegies to see this page";
            ViewBag.url = "../Volontario/Lista_Riunioni";
            ViewBag.link = "Back";
            return View("Errore");
        }

        [HttpPost]
        public ActionResult Dettagli_Riunione(FormCollection form)
        {
            /*Dato l'id restituisce i dettagli di una riunione, nell'area del volontario */
            int id = Int32.Parse(form["idev"]);
            var webclient = new Event.EventClient();
            Event.Svolgimento e = webclient.Get_event_by_id(id);
            List<Event.Volontario> volontari = webclient.Event_volunteers(e);
            ViewData["evento"] = e;
            ViewData["volontari"] = volontari; //lista dei volontari partecipanti, compreso il loggato
            return View();
       
        }
        
    }
}