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

        private string BaseURL = "http://localhost:3212/api/Vino";
        //
        // GET: /Home/
        public ActionResult Inicio()
        {
            return View();
        }


        // GET: /Home/
        public ActionResult Catalogo()
        {

            List<VinoViewModel> list = new List<VinoViewModel>();
            HttpClient client = new HttpClient();
            var result = client.GetAsync(BaseURL).Result;
            if (result.IsSuccessStatusCode)
            {
                list = result.Content.ReadAsAsync<List<VinoViewModel>>().Result;
                foreach (VinoViewModel vino in list)
                {
                    vino.TipoVino = client.GetAsync("http://localhost:3212/api/TipoVino?id=" + vino.IdTipoVino.ToString()).Result.Content.ReadAsAsync<TipoVinoViewModel>().Result;
                    vino.Marca = client.GetAsync("http://localhost:3212/api/Marca?id=" + vino.IdMarca.ToString()).Result.Content.ReadAsAsync<MarcaViewModel>().Result;
                    vino.Cepa = client.GetAsync("http://localhost:3212/api/Cepa?id=" + vino.IdCepa.ToString()).Result.Content.ReadAsAsync<CepaViewModel>().Result;
                    
                }
            }

            return View(list);
        }

        
        public ActionResult Ordenar()
        {
            return View();
        }

	}
}