using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using TVWAPI.Models;
using TVDAL;

namespace TVWAPI.Controllers
{
    public class CepaController : ApiController
    {

        private DBModel objDB = new DBModel();

        [HttpGet]
        public IEnumerable<CepaViewModel> Get()
        {
            IList<CepaViewModel> lstCepa = null;

            using (DBModel cn = new DBModel())
            {
                lstCepa = cn.Cepas.Select(s => new CepaViewModel()
                {
                    Id = s.Id,
                    Nombre = s.Nombre
                }).ToList<CepaViewModel>();

                return lstCepa;
            }
        }

        [HttpGet]
        public CepaViewModel Get(int Id)
        {
            CepaViewModel cepa = new CepaViewModel();
            using (DBModel cn = new DBModel())
            {
                var x = cn.Cepas.FirstOrDefault(e => e.Id == Id);
                if (x != null)
                {
                    cepa.Id = x.Id;
                    cepa.Nombre = x.Nombre;
                }
                return cepa;
            }

        }

        [HttpPost]
        public IHttpActionResult CrearCepa([FromBody]CepaViewModel cepa)
        {
            if (ModelState.IsValid)
            {
                objDB.Cepas.Add(new Cepa() { Nombre = cepa.Nombre });
                objDB.SaveChanges();
                return Ok(cepa);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut]
        public IHttpActionResult EditarCepa([FromBody]CepaViewModel ObjEdit)
        {
            if (ModelState.IsValid)
            {
                var regexiste = objDB.Cepas.Count(c => c.Id == ObjEdit.Id) > 0;

                if (regexiste)
                {
                    var x = objDB.Cepas.Find(ObjEdit.Id);
                    x.Nombre = ObjEdit.Nombre;

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
        public IHttpActionResult EliminarCepa(int Id)
        {

            var x = objDB.Cepas.Find(Id);

            if (x != null)
            {
                objDB.Cepas.Remove(x);
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
