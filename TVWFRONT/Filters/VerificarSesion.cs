using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TVWFRONT.Models;
using TVWFRONT.Controllers;

namespace TVWFRONT.Filters
{
    public class VerificarSesion : ActionFilterAttribute
    {

        private UsuarioViewModel objusr;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);
                // traer el usuario de la session
                objusr = (UsuarioViewModel)HttpContext.Current.Session["ssUsuario"];
                if (objusr == null)
                {
                    if (filterContext.Controller is HomeController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Home/Acceder");
                    }
                }
            }
            catch (Exception)
            {
                //filterContext.Result = new RedirectResult("~/Acceso/Login");
                filterContext.Result = new RedirectResult("~/Acceso/Login");
            }

        }

    }
}