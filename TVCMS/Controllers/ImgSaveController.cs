using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.IO;

namespace TVCMS.Controllers
{
    public class ImgSaveController : Controller
    {
        //
        // GET: /ImgSave/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if(file != null && file.ContentLength > 0) {
                string imgname = Path.GetFileName(file.FileName);
                string imgext = Path.GetExtension(imgname);

                if(imgext == ".jpg" || imgext == ".png") 
                {
                    string imgpath = Path.Combine(Server.MapPath("~/Public"), imgname);
                    file.SaveAs(imgpath);
                }
            }
            return View();
        }


	}
}