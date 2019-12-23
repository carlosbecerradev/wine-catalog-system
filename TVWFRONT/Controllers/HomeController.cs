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

        private string BaseURL = "http://localhost:3212/api/Usuario";

        // GET: Acceso
        public ActionResult Acceder()
        {
            UsuarioViewModel oUsr = new UsuarioViewModel();
            ViewBag.ErrorMsg = "";
            return View(oUsr);
        }

        [HttpPost]
        public ActionResult Acceder(UsuarioViewModel usr)
        {
            UsuarioViewModel destUsr = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                //HTTP GET
                var responseTask = client.GetAsync("Usuario?nombre=" + usr.Nombre + "&contrasenia=" + usr.Contrasenia);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<UsuarioViewModel>();
                    readTask.Wait();

                    destUsr = readTask.Result;

                    if (destUsr.Id == -1) //no entró
                    {
                        ViewBag.ErrorMsg = "¡Usuario o clave incorrecta...!";
                        Session["ssUsuario"] = null;
                        return View(usr);
                    }
                    else
                    {
                        Session["ssUsuario"] = destUsr;

                        if (destUsr.IdRole == 1) // ADMIN
                        {
                            return Redirect("http://localhost:3294/Vino/Index");
                        }
                        return Redirect("http://localhost:3294/Usuario/Index");
                    }
                }
                ViewBag.ErrorMsg = "¡No se logró establecer comunicación...!";
                return View(usr);
            }



        }

	}
}