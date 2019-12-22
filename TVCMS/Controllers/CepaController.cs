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
    public class CepaController : Controller
    {
        private string BaseURL = "http://localhost:3212/api/Cepa";
        // GET: /Cepa/
        public ActionResult Index()
        {
            List<CepaViewModel> list = new List<CepaViewModel>();
            HttpClient client = new HttpClient();
            var result = client.GetAsync(BaseURL).Result;
            if (result.IsSuccessStatusCode)
            {
                list = result.Content.ReadAsAsync<List<CepaViewModel>>().Result;
            }
            return View(list);
        }
	}
}