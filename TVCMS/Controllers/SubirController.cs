using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace TVCMS.Controllers
{
    public class SubirController : Controller
    {
        //
        // GET: /Subir/
        public ActionResult Index()
        {
            return View();

        }
            
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            using (var client = new HttpClient()) 
            {
                using (var content = new MultipartFormDataContent()) 
                {
                    byte[] Bytes = new byte[file.InputStream.Length + 1];
                    file.InputStream.Read(Bytes, 0, Bytes.Length);
                    var fileContent = new ByteArrayContent(Bytes);
                    fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = file.FileName };
                    content.Add(fileContent);
                    var requestUri = "http://localhost:3212/api/Upload";
                    var result = client.PostAsync(requestUri, content).Result;
                    if(result.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        List<string> m = result.Content.ReadAsAsync<List<string>>().Result;
                        ViewBag.Success = m.FirstOrDefault();
                    }
                    else
                    {
                        ViewBag.Failed = " Failed!" + result.Content.ToString();
                    }

                }
            }
            return View();
        }
	}
}