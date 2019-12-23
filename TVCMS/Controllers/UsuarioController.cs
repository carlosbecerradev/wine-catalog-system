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
    public class UsuarioController : Controller
    {
        private string BaseURL = "http://localhost:3212/api/Usuario";
        // GET: /Cepa/
        public ActionResult Index()
        {
            List<UsuarioViewModel> list = new List<UsuarioViewModel>();
            HttpClient client = new HttpClient();
            var result = client.GetAsync(BaseURL).Result;
            if (result.IsSuccessStatusCode)
            {
                list = result.Content.ReadAsAsync<List<UsuarioViewModel>>().Result;
            }
            return View(list);
        }
	}
}