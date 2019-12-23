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
    public class MarcaController : ApiController
    {
        private DBModel objDB = new DBModel();

        [HttpGet]
        public IEnumerable<MarcaViewModel> Get()
        {
            IList<MarcaViewModel> lstMarca = null;

            using (DBModel cn = new DBModel())
            {
                lstMarca = cn.Marcas.Select(s => new MarcaViewModel()
                {
                    Id = s.Id,
                    Nombre = s.Nombre
                }).ToList<MarcaViewModel>();

                return lstMarca;
            }
        }

        [HttpGet]
        public MarcaViewModel Get(int Id)
        {
            MarcaViewModel marca = new MarcaViewModel();
            using (DBModel cn = new DBModel())
            {
                var x = cn.Marcas.FirstOrDefault(e => e.Id == Id);
                if (x != null)
                {
                    marca.Id = x.Id;
                    marca.Nombre = x.Nombre;
                }
                return marca;
            }

        }

        [HttpPost]
        public IHttpActionResult CrearMarca([FromBody]MarcaViewModel marca)
        {
            if (ModelState.IsValid)
            {
                objDB.Marcas.Add(new Marca() { Nombre = marca.Nombre });
                objDB.SaveChanges();
                return Ok(marca);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut]
        public IHttpActionResult EditarMarca(int Id, [FromBody]MarcaViewModel marca)
        {
            if (ModelState.IsValid)
            {
                var regexiste = objDB.Marcas.Count(c => c.Id == Id) > 0;

                if (regexiste)
                {
                    var x = objDB.Marcas.Find(Id);
                    x.Nombre = marca.Nombre;

                    objDB.SaveChanges();
                    return Ok(marca);
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
        public IHttpActionResult EliminarMarca(int Id)
        {

            var x = objDB.Marcas.Find(Id);

            if (x != null)
            {
                objDB.Marcas.Remove(x);
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
