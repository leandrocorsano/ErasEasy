//=============================================================================
// Authors: Francesca Rossi, Leandro Corsano
//=============================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ErasEasyLife.Controllers
{
    public class HomeController : Controller
    {
        /*
         *RESTITUISCE LA PAGINA PRINCIPALE DELL'APPLICAZIONE
         */
        public ActionResult Index()
        {
            return View();
        }

    }
}