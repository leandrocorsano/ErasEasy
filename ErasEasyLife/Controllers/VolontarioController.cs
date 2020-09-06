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

        [HttpGet]
        public ActionResult Dashboard()
        {
            var volclient = new Volunteer.VolunteerClient();
            var eventclient = new Event.EventClient();
            Volunteer.Volontario vol = (Volunteer.Volontario)Session["volontario"];
            DateTime oggi = DateTime.Today;
            string cond = " and data_i >= '" + oggi.ToString("yyyy-MM-dd") + "' and tipologia!='riunione' and idass=" + vol.ass.IdAss;
            List<Event.Svolgimento> futuri_eventi = eventclient.Show_events(cond);
            ViewData["n_prossimi_eventi"] = futuri_eventi.Count();           
            cond = " and data_i >= '" + oggi.ToString("yyyy-MM-dd") + "' and tipologia='riunione' and idass=" + vol.ass.IdAss;
            List<Event.Svolgimento> future_riunioni = eventclient.Show_events(cond);
            ViewData["n_prossime_riunioni"] = future_riunioni.Count();
            cond = " tipologia!='Riunione' and MONTH(data_i)='" + oggi.Month + "'";
            List<Volunteer.Svolgimento> my_events = volclient.Show_Event(vol.IdVolont, cond);
            cond = " tipologia='Riunione' and MONTH(data_i)='"+oggi.Month+"'"; //le mie riunioni di questo mese
            List<Volunteer.Svolgimento> my_meetings = volclient.Show_Event(vol.IdVolont, cond);
            ViewData["n_mie_riunioni"] = my_meetings.Count();
            ViewData["n_miei_eventi"] = my_events.Count();
            return View();
        }
        // GET: Volontario/Details/5
        public ActionResult Profilo()
        {
            

            return View("Profilo");


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
          
             List<Association.Associazione> associazioni= webclient.Show_associations("");
            var volclient = new Volunteer.VolunteerClient();
            int id = volclient.Generate_id();
            ViewData["ID"] = id;
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
                        ViewBag.risposta = "Successfully signed in";
                        Session["Volontario"] = vol; //passo il volontario che è entrato tra le varie pagine web


                        return View("Dashboard");
                    }
                    else
                    {
                        ViewBag.message = "Wrong user";
                        return View();
                    }


                }
                catch
                {
                    ViewBag.message = "Wrong user";
                    return View();
                }
            }
            else
            {
                ViewBag.message = "Wrong user";
                return View();
            }
        }
        [HttpPost]
        public ActionResult Elimina_partecipazione(FormCollection form)
        {
            try
            {
                int evento = Int32.Parse(form["idev"]);
                var webclient = new Event.EventClient();
                Event.Svolgimento e = webclient.Get_event_by_id(evento);
                Volunteer.Volontario vol = (Volunteer.Volontario)Session["volontario"];
                var webclient1 = new Volunteer.VolunteerClient();
                bool r = webclient1.CancelBooking(vol.IdVolont, evento);
                if (r == true)
                {
                    if (e.evento.tipologia == "Riunione")
                    {
                        ViewBag.risposta = "Meeting successfully cancelled";
                        ViewBag.url = "../Evento/Lista_riunioni";
                        ViewBag.link = "Torna alle riunioni";
                    }
                    else
                    {
                        ViewBag.risposta = "Event successfully cancelled";
                        ViewBag.url = "../Volontario/Lista_Eventi";
                        ViewBag.link = "Torna agli eventi";
                    }
                    return View("Successo");
                }
                else
                {
                    return View("Evento/lista_Eventi");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Evento/lista_Eventi");
            }
        }
        [HttpGet]
        public ActionResult MieiEventi()
        {
            var webclient = new Volunteer.VolunteerClient();
            Volunteer.Volontario vol = (Volunteer.Volontario)Session["Volontario"];
            string tipologia = " E.tipologia != 'riunione'";
            List<Volunteer.Svolgimento> listaeventi = webclient.Show_Event(vol.IdVolont, tipologia);
            ViewData["eventi"] = listaeventi;
            return View();

        }
        [HttpGet]
        public ActionResult MieRiunioni()
        {
            var webclient = new Volunteer.VolunteerClient();
            Volunteer.Volontario vol = (Volunteer.Volontario)Session["Volontario"];
            string tipologia = " E.tipologia = 'riunione'";
            List<Volunteer.Svolgimento> listariunioni = webclient.Show_Event(vol.IdVolont, tipologia);
            ViewData["riunioni"] = listariunioni;
            return View();

        }
        [HttpGet]
        public ActionResult Lista_Eventi()
        {
            try
            {
                DateTime oggi = DateTime.Today;
                Volunteer.Volontario vol = (Volunteer.Volontario)Session["Volontario"];

                string cond = " and data_i > '" + oggi.ToString("yyyy-MM-dd") + "' and tipologia!='riunione' and idass = " + vol.ass.IdAss + " order by data_i";
                var webclient = new Event.EventClient();
                List<Event.Svolgimento> eventi = webclient.Show_events(cond);

                ViewData["eventi"] = eventi;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.risposta = "Errore:" + ex.Message;
                ViewBag.url = "../Volontario/Lista_Eventi";
                ViewBag.link = "Back to events";
                return View("Errore");
            }
        }
        [HttpPost]
        public ActionResult Dettagli_Evento(FormCollection form)
        {
            try
            {
                int id = Int32.Parse(form["idev"]);
                var webclient = new Event.EventClient();

                Event.Svolgimento e = webclient.Get_event_by_id(id);
                List<Event.Volontario> volontari = webclient.Event_volunteers(e);
                ViewData["evento"] = e;
                ViewData["volontari"] = volontari;



                return View();
            }
            catch(Exception ex)
            {
                ViewBag.risposta = "Errore:"+ ex.Message;
                ViewBag.url = "../Volontario/Lista_Eventi";
                ViewBag.link = "Back to events";
                return View("Errore");
            }


        }
        [HttpPost]
        public ActionResult Partecipa(FormCollection form)
        {
            try
            {
                int evento = Int32.Parse(form["idev"]);
                var webclient= new Event.EventClient();
                Event.Svolgimento e = webclient.Get_event_by_id(evento);
                Volunteer.Volontario vol = (Volunteer.Volontario)Session["volontario"];
                var webclient1 = new Volunteer.VolunteerClient();
                bool r = webclient1.BookEvent(vol.IdVolont, evento);
                if (r == true)
                {
                    if (e.evento.tipologia == "Riunione")
                    {
                        ViewBag.risposta = "Meeting successfully booked";
                        ViewBag.url = "../Evento/Lista_riunioni";
                        ViewBag.link = "Back to meetings";
                    }
                    else
                    {
                        ViewBag.risposta = "Event successfully booked";
                        ViewBag.url = "../Volontario/Lista_Eventi";
                        ViewBag.link = "Back to events";
                    }
                    return View("Successo");
                }
                else
                {
                    ViewBag.risposta = "There was an errore, Try again!";
                    ViewBag.url = "../Volontario/Lista_Eventi";
                    ViewBag.link = "Back to list";
                    return View("Errore");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.risposta = "There was an errore, Try again!";
                ViewBag.url = "../Volontario/Lista_Eventi";
                ViewBag.link = "Back to list";
                return View("Errore");
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

                    ViewBag.risposta = "Successfully registered";
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

                    ViewBag.risposta = "Profile successfully updated";

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
                    ViewBag.risposta = "Password successfully updated";
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

        public ActionResult Logout()
        {

            Session.Abandon();
            return RedirectToAction("Login", "Volontario");

        }
        [HttpGet]
        // GET: Volontario/Create
        public ActionResult Elenco()
        {
            
                var webclient = new Volunteer.VolunteerClient();
                Volunteer.Volontario vol = (Volunteer.Volontario)Session["Volontario"];
                string cond = "idvolont!=" + vol.IdVolont + " and idass=" + vol.ass.IdAss + "";
                List<Volunteer.Volontario> volontari = webclient.Show_volontari(cond);

                ViewData["volontari"] = volontari;
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
