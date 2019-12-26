using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

using System.Threading.Tasks;
using System.IO;
using TVWAPI.Models;
using TVDAL;

namespace TVWAPI.Models
{
    public class UploadController : ApiController
    {
        private DBModel objDB = new DBModel();

        
        public Task<HttpResponseMessage> Post()
        {
            List<string> saveFilePath = new List<string>();
            if (!Request.Content.IsMimeMultipartContent()){
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            string rootPath = HttpContext.Current.Server.MapPath("~/Public");
            var provider = new MultipartFileStreamProvider(rootPath);
            var task = Request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<HttpResponseMessage>(t =>
                {
                    if(t.IsCanceled || t.IsFaulted) {
                        Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
                    }
                    foreach(MultipartFileData item in provider.FileData) {
                        try
                        {
                            string name = item.Headers.ContentDisposition.FileName.Replace("\"", "");
                            string newFileName =    Guid.NewGuid() + Path.GetExtension(name);
                            File.Move(item.LocalFileName, Path.Combine(rootPath, newFileName));

                            Uri baseUri = new Uri(Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.PathAndQuery, string.Empty));
                            string fileRelativePath = "~/Public/" + newFileName;
                            Uri fileFullPath = new Uri(baseUri, VirtualPathUtility.ToAbsolute(fileRelativePath));
                            saveFilePath.Add(fileFullPath.ToString());
                        }
                        catch(Exception ex){
                            String message = ex.Message;
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.Created, saveFilePath);
                });
            return task;
        }

        public IEnumerable<string> Get()
        {
            IList<string> lstImages = new List<string>();

            DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Request.MapPath("~/Public"));

            foreach (var archivo in di.GetFiles())
            {
                string arch = "http://localhost:3212/Public/" + archivo.Name;
                lstImages.Add(arch);
            }
             
            return lstImages;
        }

    }
}
