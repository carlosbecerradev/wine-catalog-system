using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TVWFRONT.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Inicio()
        {
            return View();
        }


        // GET: /Home/
        public ActionResult Catalogo()
        {
            return View();
        }

        // GET: /Home/
        public ActionResult Acceder()
        {
            return View();
        }
	}
}