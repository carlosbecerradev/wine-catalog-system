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
    public class VinoController : Controller
    {

        private string BaseURL = "http://localhost:3212/api/Vino";

        // GET: /Cepa/
        public ActionResult Index()
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


        // GET: Rol/Details/5
        public ActionResult Details(int id)
        {
            VinoViewModel ObjResult = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                //HTTP GET
                var responseTask = client.GetAsync("Vino?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<VinoViewModel>();
                    readTask.Wait();

                    ObjResult = readTask.Result;
                    ObjResult.TipoVino = client.GetAsync("http://localhost:3212/api/TipoVino?id=" + ObjResult.IdTipoVino.ToString()).Result.Content.ReadAsAsync<TipoVinoViewModel>().Result;
                    ObjResult.Marca = client.GetAsync("http://localhost:3212/api/Marca?id=" + ObjResult.IdMarca.ToString()).Result.Content.ReadAsAsync<MarcaViewModel>().Result;
                    ObjResult.Cepa = client.GetAsync("http://localhost:3212/api/Cepa?id=" + ObjResult.IdCepa.ToString()).Result.Content.ReadAsAsync<CepaViewModel>().Result;
                }
            }

            return View(ObjResult);
        }

        // GET: Rol/Create
        public ActionResult Create()
        {
            VinoViewModel obj = new VinoViewModel();
            HttpClient client = new HttpClient();

            obj.lstTVino = client.GetAsync("http://localhost:3212/api/TipoVino").Result.Content.ReadAsAsync<List<TipoVinoViewModel>>().Result;
            obj.lstMarca = client.GetAsync("http://localhost:3212/api/Marca").Result.Content.ReadAsAsync<List<MarcaViewModel>>().Result;
            obj.lstCepa = client.GetAsync("http://localhost:3212/api/Cepa").Result.Content.ReadAsAsync<List<CepaViewModel>>().Result;

            Dictionary<string, string> recibirPaises = client.GetAsync("http://country.io/names.json").Result.Content.ReadAsAsync<Dictionary<string, string>>().Result;
            List<string> lstAuxiliar = new List<string>();
            foreach(string value in recibirPaises.Values) {
                lstAuxiliar.Add(value);            
            }
            obj.lstPais = lstAuxiliar.OrderBy(o => o).ToList();
            
            return View(obj);
        }

        // POST: Rol/Create
        [HttpPost]
        public ActionResult Create(VinoViewModel objC)
        {
            try
            {
                // TODO: Add insert logic here
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseURL);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<VinoViewModel>("Vino", objC);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }

                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator - CreateC.");

                return View(objC);

            }
            catch
            {
                return View(objC);
            }
        }

        // GET: Rol/Edit/5
        public ActionResult Edit(int id)
        {
            VinoViewModel ObjEd = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                //HTTP GET
                var responseTask = client.GetAsync("Vino?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<VinoViewModel>();
                    readTask.Wait();

                    ObjEd = readTask.Result;
                }
                ObjEd.lstTVino = client.GetAsync("http://localhost:3212/api/TipoVino").Result.Content.ReadAsAsync<List<TipoVinoViewModel>>().Result;
                ObjEd.lstMarca = client.GetAsync("http://localhost:3212/api/Marca").Result.Content.ReadAsAsync<List<MarcaViewModel>>().Result;
                ObjEd.lstCepa = client.GetAsync("http://localhost:3212/api/Cepa").Result.Content.ReadAsAsync<List<CepaViewModel>>().Result;

                Dictionary<string, string> recibirPaises = client.GetAsync("http://country.io/names.json").Result.Content.ReadAsAsync<Dictionary<string, string>>().Result;
                List<string> lstAuxiliar = new List<string>();
                foreach (string value in recibirPaises.Values)
                {
                    lstAuxiliar.Add(value);
                }
                ObjEd.lstPais = lstAuxiliar.OrderBy(o => o).ToList();
            }

            return View(ObjEd);
        }

        // POST: Rol/Edit/5
        [HttpPost]
        public ActionResult Edit(VinoViewModel objEdit)
        {

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseURL);

                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<VinoViewModel>("Vino", objEdit);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View(objEdit);

            }
            catch
            {
                return View(objEdit);
            }
        }

        // GET: Rol/Delete/5
        public ActionResult Delete(int id)
        {
            VinoViewModel ObjDel = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                //HTTP GET
                var responseTask = client.GetAsync("Vino?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<VinoViewModel>();
                    readTask.Wait();

                    ObjDel = readTask.Result;
                }
            }
            return View(ObjDel);
        }

        // POST: Rol/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseURL);

                    //HTTP DELETE
                    var deleteTask = client.DeleteAsync("Vino/" + id.ToString());
                    deleteTask.Wait();

                    var result = deleteTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

	}
}