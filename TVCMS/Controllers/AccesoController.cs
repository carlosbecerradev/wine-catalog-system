using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TVCMS.Models;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TVCMS.Controllers
{
    public class AccesoController : Controller
    {
        private string BaseURL = "http://localhost:3212/api/Usuario";

        // GET: Acceso
        public ActionResult Login()
        {
            UsuarioViewModel oUsr = new UsuarioViewModel();
            ViewBag.ErrorMsg = "";
            return View(oUsr);
        }

        [HttpPost]
        public ActionResult Login(UsuarioViewModel usr)
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
                            return RedirectToAction("Index", "Usuario");
                        }
                        return RedirectToAction("Index", "Cepa");
                    }
                }
                ViewBag.ErrorMsg = "¡No se logró establecer comunicación...!";
                return View(usr);
            }



        }
	}
}