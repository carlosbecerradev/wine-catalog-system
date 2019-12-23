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
    public class RoleController : Controller
    {

        private string BaseURL = "http://localhost:3212/api/Role";

        // GET: /Cepa/
        public ActionResult Index()
        {
            List<RoleViewModel> list = new List<RoleViewModel>();
            HttpClient client = new HttpClient();
            var result = client.GetAsync(BaseURL).Result;
            if (result.IsSuccessStatusCode)
            {
                list = result.Content.ReadAsAsync<List<RoleViewModel>>().Result;
            }
            return View(list);
        }


        // GET: Rol/Details/5
        public ActionResult Details(int id)
        {
            RoleViewModel ObjResult = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                //HTTP GET
                var responseTask = client.GetAsync("Role?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<RoleViewModel>();
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
        public ActionResult Create(RoleViewModel objC)
        {
            try
            {
                // TODO: Add insert logic here
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseURL);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<RoleViewModel>("Role", objC);
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
            RoleViewModel ObjEd = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                //HTTP GET
                var responseTask = client.GetAsync("Role?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<RoleViewModel>();
                    readTask.Wait();

                    ObjEd = readTask.Result;
                }
            }

            return View(ObjEd);
        }

        // POST: Rol/Edit/5
        [HttpPost]
        public ActionResult Edit(RoleViewModel objEdit)
        {

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseURL);

                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<RoleViewModel>("Role", objEdit);
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
            RoleViewModel ObjDel = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                //HTTP GET
                var responseTask = client.GetAsync("Role?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<RoleViewModel>();
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
                    var deleteTask = client.DeleteAsync("Role/" + id.ToString());
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