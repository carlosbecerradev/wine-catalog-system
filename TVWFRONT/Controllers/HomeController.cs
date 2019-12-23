using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TVWFRONT.Models;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

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


	}
}