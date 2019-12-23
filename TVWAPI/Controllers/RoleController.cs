using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using TVWAPI.Models;
using TVDAL;

namespace CTVAPI.Controllers
{
    public class RoleController : ApiController
    {

        private DBModel objDB = new DBModel();

        [HttpGet]
        public IEnumerable<RoleViewModel> Get()
        {
            IList<RoleViewModel> lstRole = null;

            using (DBModel cn = new DBModel())
            {
                lstRole = cn.Roles.Select(s => new RoleViewModel()
                {
                    Id = s.Id,
                    Tipo = s.Tipo
                }).ToList<RoleViewModel>();

                return lstRole;
            }
        }

        [HttpGet]
        public RoleViewModel Get(int Id)
        {
            RoleViewModel role = new RoleViewModel();
            using (DBModel cn = new DBModel())
            {
                var x = cn.Roles.FirstOrDefault(e => e.Id == Id);
                if (x != null)
                {
                    role.Id = x.Id;
                    role.Tipo = x.Tipo;
                }
                return role;
            }

        }

        [HttpPost]
        public IHttpActionResult CrearRol([FromBody]RoleViewModel role)
        {
            if (ModelState.IsValid)
            {
                objDB.Roles.Add(new Role() { Tipo = role.Tipo });
                objDB.SaveChanges();
                return Ok(role);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut]
        public IHttpActionResult EditarRol([FromBody]RoleViewModel role)
        {
            if (ModelState.IsValid)
            {
                var regexiste = objDB.Roles.Count(c => c.Id == role.Id) > 0;

                if (regexiste)
                {
                    var x = objDB.Roles.Find(role.Id);
                    x.Tipo = role.Tipo;

                    objDB.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }

            }
            else
            {
                return BadRequest();
            }

        }

        [HttpDelete]
        public IHttpActionResult EliminarRol(int Id)
        {

            var x = objDB.Roles.Find(Id);

            if (x != null)
            {
                objDB.Roles.Remove(x);
                objDB.SaveChanges();
                return Ok(x);

            }
            else
            {
                return NotFound();
            }

        }

    }
}
