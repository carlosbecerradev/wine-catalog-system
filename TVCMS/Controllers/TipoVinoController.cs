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
    public class TipoVinoController : Controller
    {

        private string BaseURL = "http://localhost:3212/api/TipoVino";

        // GET: /Cepa/
        public ActionResult Index()
        {
            List<TipoVinoViewModel> list = new List<TipoVinoViewModel>();
            HttpClient client = new HttpClient();
            var result = client.GetAsync(BaseURL).Result;
            if (result.IsSuccessStatusCode)
            {
                list = result.Content.ReadAsAsync<List<TipoVinoViewModel>>().Result;
            }
            return View(list);
        }


        // GET: Rol/Details/5
        public ActionResult Details(int id)
        {
            TipoVinoViewModel ObjResult = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                //HTTP GET
                var responseTask = client.GetAsync("TipoVino?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<TipoVinoViewModel>();
                    readTask.Wait();

                    ObjResult = readTask.Result;
                }
            }

            return View(ObjResult);
        }

        // GET: Rol/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rol/Create
        [HttpPost]
        public ActionResult Create(TipoVinoViewModel objC)
        {
            try
            {
                // TODO: Add insert logic here
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseURL);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<TipoVinoViewModel>("TipoVino", objC);
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
            TipoVinoViewModel ObjEd = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                //HTTP GET
                var responseTask = client.GetAsync("TipoVino?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<TipoVinoViewModel>();
                    readTask.Wait();

                    ObjEd = readTask.Result;
                }
            }

            return View(ObjEd);
        }

        // POST: Rol/Edit/5
        [HttpPost]
        public ActionResult Edit(TipoVinoViewModel objEdit)
        {

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseURL);

                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<TipoVinoViewModel>("TipoVino", objEdit);
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
            TipoVinoViewModel ObjDel = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                //HTTP GET
                var responseTask = client.GetAsync("TipoVino?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<TipoVinoViewModel>();
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
                    var deleteTask = client.DeleteAsync("TipoVino/" + id.ToString());
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