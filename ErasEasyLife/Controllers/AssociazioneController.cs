using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ErasEasyLife.Models;
using ErasEasyLife.Association;
#pragma warning disable CS0105 // La direttiva using per 'ErasEasyLife.Association' è già presente in questo spazio dei nomi
using ErasEasyLife.Association;
using System.Linq.Expressions;
using System.Net;
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

        // GET: Associazione/Profile/5

        public ActionResult Profilo()
        {
           return View("Profilo");
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
        [HttpGet]
        public ActionResult Modifica_Pass()
        {
            return View("Modifica_Pass");
        }

        

        
        [HttpPost]
        public ActionResult Login(Models.Associazione model)
        {

            if (ModelState.IsValidField("email") && ModelState.IsValidField("password"))
            {
                /*non controllo se modelstase is valid perchè non uso tutti i membri del modello ma solo 2*/

                try
                {
                    var webclient = new Association.AssociationClient();
                    Association.Associazione ass = webclient.Login(model.email, model.password);
                    if (ass != null)
                    {
                        //ViewBag.risposta = "Hai effettuato il login";
                        Session["associazione"] = ass; //passo l'associazione che è entrata tra le varie pagine web

                        //Profilo();
                       
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

        [HttpPost]
        public ActionResult Aggiungi_ruolo(Models.Volontario model)
        {

            if (ModelState.IsValidField("IdVolont") && ModelState.IsValidField("ruolo"))
            {
                /*non controllo se modelstase is valid perchè non uso tutti i membri del modello ma solo 2*/

                try
                {
                    var webclient = new Association.AssociationClient();
                    bool r =  webclient.add_ruolo(model.IdVolont, model.ruolo);
                    if (r==true)
                    {

                        ViewBag.risposta = "Ruolo aggiunto con successo";
                        ViewBag.url = "Elenco_Volontari";
                        ViewBag.link = "Torna all'elenco";
                        return View("Successo");
                    }
                    else
                    {
                        ViewBag.risposta = "C'è stato un problema";
                        ViewBag.url = "Elenco_Volontari";
                        ViewBag.link = "Torna all'elenco";
                        return View("Errore");
                    }


                }
                catch
                {
                    ViewBag.risposta = "C'è stato un problema";
                    ViewBag.url = "Elenco_Volontari";
                    ViewBag.link = "Torna all'elenco";
                    return View("Errore");
                }
            }
            else
            {
                return View("Elenco_Volontari");
            }
        }

        [HttpPost]
        // POST: Associazione/Create
        public ActionResult Registra(Models.Associazione model)
        {
            try
            {
                if ((ModelState.IsValid))
                {// TODO: Add insert logic here

                   
                  
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
        [HttpGet]
            public ActionResult Modifica_Profilo()
        {
            return View("Modifica_Profilo");
        }

        // POST: Associazione/Edit/5
        [HttpPost]
        public ActionResult Modifica_Profilo(Models.Associazione model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var webclient = new Association.AssociationClient();
                    Association.Associazione ass = new Association.Associazione();
                    ass.IdAss = model.IdAss;
                    ass.password = model.password;
                    ass.nome = model.nome;
                    ass.citta = model.citta;
                    ass.stato = model.stato;
                    ass.tel = model.tel;
                    ass.email = model.email;
                    ass.via = model.via;
                    Session["Associazione"] = ass;
                    bool r = webclient.UpdateProfile(ass);
                    ViewBag.risposta = "Hai modificato i dati con successo";

                    return View("Profilo");

                }
                // TODO: Add update logic here
                else
                {
                   
                    return View();
                    
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
        }
        [HttpPost]
        public ActionResult Modifica_Pass(Models.CambioPass model)
        {
            try
            {
                if ((ModelState.IsValid))
                {
                    
                    var webclient = new Association.AssociationClient();
                    Association.Associazione ass =(Association.Associazione) Session["Associazione"]; //recupero l'associazione che è entrata

                    ass.password = model.nuova_pass;
                    
                    bool r = webclient.UpdatePassword(ass.IdAss, model.nuova_pass);
                    if(r==true)
                    {
                        Session["Associazione"] = ass; //creo la nuova session
                    }
                    ViewBag.risposta = "Hai cambiato password con successo";
                    return View("Successo");

                }
                else
                {
                    
                    return View();
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }

        }
         [HttpGet]
         public ActionResult Elenco()
        {
            var webclient = new Association.AssociationClient();
            string[] listacitta = webclient.GetCitta("");
            ViewData["citta"] = listacitta;
            return View();
        }
        [HttpPost]
        // GET: Volontario/Create
        public ActionResult Elenco(Models.Associazione model)
        {
            if (ModelState.IsValidField("citta"))
            {
                var webclient = new Association.AssociationClient();
               
                string cond = "citta='" + model.citta + "'";
                string[] listacitta = webclient.GetCitta("");
                ViewData["citta"] = listacitta;
                Association.Associazione[] associazioni = webclient.Show_associations(cond);

                ViewData["elenco_associazioni"] = associazioni;
                return View();
            }
            return View();
        }
        [HttpGet]
        public ActionResult Elenco_Volontari()
        {
           
                var webclient = new Volunteer.VolunteerClient();
                Association.Associazione ass = (Association.Associazione)Session["Associazione"];
                string cond = "idass=" + ass.IdAss;
                Volunteer.Volontario[] volontari = webclient.Show_volontari(cond);

                ViewData["elenco_volontari"] = volontari;
                return View();

        }
        [HttpGet]
        public ActionResult Crea_Evento()
        {
            return View("Crea_Evento");
        }
        [HttpPost]
        public ActionResult Crea_Evento(Models.Evento model)
        {
            try
            {
                if ((ModelState.IsValid))
                {
                    var webclient = new Association.AssociationClient();
                    Association.Associazione ass = (Association.Associazione)Session["Associazione"];
                    Association.Evento ev = new Association.Evento();
                    Association.Luogo l = new Association.Luogo();
                    Association.Svolgimento svo = new Association.Svolgimento();
                    ev.IdEv = model.IdEv;
                    ev.nome = model.nome;
                    ev.tipologia = model.tipologia;
                    ev.min_p = model.min_p;
                    ev.max_p = model.max_p;
                    ev.min_v = model.min_v;
                    ev.max_v = model.max_v;
                    ev.costo = model.costo;
                    ev.descrizione = model.descrizione;
                    ev.ass = (Association.Associazione)Session["Associazione"];
                    l.IdLuogo = model.IdLuogo;
                    l.via = model.via;
                    l.citta = model.citta;
                    l.stato = model.stato;
                    svo.evento = ev;
                    svo.luogo = l;
                    svo.data_i = model.data_i;
                    svo.data_f = model.data_f;
                    svo.ora_i = model.ora_i;
                    svo.ora_f = model.ora_f;
                    bool r = webclient.Create_events(svo);
                    ViewBag.risposta = "Evento creato con successo";
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
        public ActionResult Crea_Riunione()
        {
            return View("Crea_Riunione");
        }
        [HttpPost]
        public ActionResult Crea_Riunione(Models.Evento model)
        {
            try
            {
                if ((ModelState.IsValid))
                {
                    var webclient = new Association.AssociationClient();
                    Association.Associazione ass = (Association.Associazione)Session["Associazione"];
                    Association.Evento ev = new Association.Evento();
                    Association.Luogo l = new Association.Luogo();
                    Association.Svolgimento svo = new Association.Svolgimento();
                    ev.IdEv = model.IdEv;
                    ev.nome = model.nome;
                    ev.tipologia = model.tipologia;
                    ev.min_p = model.min_p;
                    ev.max_p = model.max_p;
                    ev.min_v = model.min_v;
                    ev.max_v = model.max_v;
                    ev.costo = model.costo;
                    ev.descrizione = model.descrizione;
                    ev.ass = (Association.Associazione)Session["Associazione"];
                    l.IdLuogo = model.IdLuogo;
                    l.via = ass.via;
                    l.citta = ass.citta;
                    l.stato = ass.stato;
                    svo.evento = ev;
                    svo.luogo = l;
                    svo.data_i = model.data_i;
                    svo.data_f = model.data_i;
                    svo.ora_i = model.ora_i;
                    svo.ora_f = model.ora_f;
                    bool r = webclient.Create_events(svo);
                    ViewBag.risposta = "Evento creato con successo";
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
        public ActionResult Elenco_Eventi()
        {
            var webclient = new Association.AssociationClient();
            Association.Associazione ass = (Association.Associazione)Session["Associazione"];
            string tipologia = " E.tipologia != 'riunione'";
            Association.Svolgimento[] listaeventi = webclient.Show_Event(ass.IdAss, tipologia);
            ViewData["eventi"] = listaeventi;
            return View();
        }
        [HttpGet]
        public ActionResult Elenco_Riunioni()
        {
            var webclient = new Association.AssociationClient();
            Association.Associazione ass = (Association.Associazione)Session["Associazione"];
            string tipologia = " E.tipologia = 'riunione'";
            Association.Svolgimento[] listariunioni = webclient.Show_Event(ass.IdAss, tipologia);
            ViewData["riunioni"] = listariunioni;
            return View();

        }
        [HttpPost]
        public ActionResult Dettagli_Evento(FormCollection form)
        {

            int id = Int32.Parse(form["idev"]);
            var webclient = new Event.EventClient();

            Event.Svolgimento e = webclient.Get_event_by_id(id);
            List<Event.Studente> studenti = webclient.Event_partecipations(e);
            List<Event.Volontario> volontari = webclient.Event_volunteers(e);
            ViewData["evento"] = e;
            ViewData["studenti"] = studenti;
            ViewData["volontari"] = volontari;



            return View();


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
