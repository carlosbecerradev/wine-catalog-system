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


        // GET: Rol/Details/5
        public ActionResult Details(int id)
        {
            UsuarioViewModel ObjResult = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                //HTTP GET
                var responseTask = client.GetAsync("Usuario?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<UsuarioViewModel>();
                    readTask.Wait();

                    ObjResult = readTask.Result;
                    ObjResult.Role = client.GetAsync("http://localhost:3212/api/Role?id=" + ObjResult.IdRole.ToString()).Result.Content.ReadAsAsync<RoleViewModel>().Result;
                    
                }
            }

            return View(ObjResult);
        }

        // GET: Rol/Create
        public ActionResult Create()
        {
            UsuarioViewModel obj = new UsuarioViewModel();
            HttpClient client = new HttpClient();
            obj.lstRole = client.GetAsync("http://localhost:3212/api/Role").Result.Content.ReadAsAsync<List<RoleViewModel>>().Result;

            return View(obj);
        }

        // POST: Rol/Create
        [HttpPost]
        public ActionResult Create(UsuarioViewModel objC)
        {
            try
            {
                // TODO: Add insert logic here
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseURL);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<UsuarioViewModel>("Usuario", objC);
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
            UsuarioViewModel ObjEd = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                //HTTP GET
                var responseTask = client.GetAsync("Usuario?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<UsuarioViewModel>();
                    readTask.Wait();

                    ObjEd = readTask.Result;
                }

                ObjEd.lstRole = client.GetAsync("http://localhost:3212/api/Role").Result.Content.ReadAsAsync<List<RoleViewModel>>().Result;

            }

            return View(ObjEd);
        }

        // POST: Rol/Edit/5
        [HttpPost]
        public ActionResult Edit(UsuarioViewModel objEdit)
        {

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseURL);

                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<UsuarioViewModel>("Usuario", objEdit);
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
            UsuarioViewModel ObjDel = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                //HTTP GET
                var responseTask = client.GetAsync("Usuario?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<UsuarioViewModel>();
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
                    var deleteTask = client.DeleteAsync("Usuario/" + id.ToString());
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