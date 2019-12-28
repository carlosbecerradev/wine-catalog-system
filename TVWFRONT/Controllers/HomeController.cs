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
        public ActionResult Catalogo(int orden = 1)
        {
            ContenedorViewModel container = new ContenedorViewModel();
            
            HttpClient client = new HttpClient();

            var result = client.GetAsync(BaseURL).Result;
            if (result.IsSuccessStatusCode)
            {
                if (orden == 1)
                {
                    container.lstVinos = modeloVino().OrderByDescending(o => o.Precio);
                }
                else
                    if (orden == 2)
                    {
                        container.lstVinos = modeloVino().OrderBy(o => o.Precio);
                    }

                container.lstMarcas = client.GetAsync("http://localhost:3212/api/Marca").Result.Content.ReadAsAsync<List<MarcaViewModel>>().Result;
                container.lstCepas = client.GetAsync("http://localhost:3212/api/Cepa").Result.Content.ReadAsAsync<List<CepaViewModel>>().Result;
                container.lstTVino = client.GetAsync("http://localhost:3212/api/TipoVino").Result.Content.ReadAsAsync<List<TipoVinoViewModel>>().Result;                
            }            
            

            return View(container);
        }


        [HttpPost]
        public ActionResult Catalogo(string tipo_vino = "", string marca = "", string cepa = "")
        {
            ContenedorViewModel container = new ContenedorViewModel();

            HttpClient client = new HttpClient();

            var result = client.GetAsync(BaseURL).Result;
            if (result.IsSuccessStatusCode)
            {
                if (tipo_vino == "" && marca == "" && cepa == "")
                {
                    container.lstVinos = container.lstVinos = modeloVino().OrderByDescending(p => p.Precio);
                }
                else
                {
                    IEnumerable<VinoViewModel> prevlist = new List<VinoViewModel>();
                    prevlist = modeloVino().Where(w => w.TipoVino.Nombre == tipo_vino || w.Marca.Nombre == marca || w.Cepa.Nombre == cepa);
                    container.lstVinos = prevlist;
                }


                container.lstMarcas = client.GetAsync("http://localhost:3212/api/Marca").Result.Content.ReadAsAsync<List<MarcaViewModel>>().Result;
                container.lstCepas = client.GetAsync("http://localhost:3212/api/Cepa").Result.Content.ReadAsAsync<List<CepaViewModel>>().Result;
                container.lstTVino = client.GetAsync("http://localhost:3212/api/TipoVino").Result.Content.ReadAsAsync<List<TipoVinoViewModel>>().Result;
            }


            return View(container);
        }


        public List<VinoViewModel> modeloVino()
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
            return list;
        }


	}
}