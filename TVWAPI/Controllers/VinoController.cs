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
    public class VinoController : ApiController
    {

        private DBModel objDB = new DBModel();

        [HttpGet]
        public IEnumerable<VinoViewModel> Get()
        {
            IList<VinoViewModel> lstVino = null;

            using (DBModel cn = new DBModel())
            {
                lstVino = cn.Vinoes.Select(s => new VinoViewModel()
                {
                    Id = s.Id,
                    ImagenUri = s.ImagenUri,
                    IdTipoVino = s.IdTipoVino,
                    IdMarca = s.IdMarca,
                    IdCepa = s.IdCepa,
                    Pais = s.Pais,
                    Cosecha = s.Cosecha,
                    Precio = s.Precio,
                    Stock = s.Stock
                }).ToList<VinoViewModel>();

                return lstVino;
            }
        }

        [HttpGet]
        public VinoViewModel Get(int Id)
        {
            VinoViewModel vino = new VinoViewModel();
            using (DBModel cn = new DBModel())
            {
                var x = cn.Vinoes.FirstOrDefault(e => e.Id == Id);
                if (x != null)
                {
                    vino.Id = x.Id;
                    vino.ImagenUri = x.ImagenUri;
                    vino.IdTipoVino = x.IdTipoVino;
                    vino.IdMarca = x.IdMarca;
                    vino.IdCepa = x.IdCepa;
                    vino.Pais = x.Pais;
                    vino.Cosecha = x.Cosecha;
                    vino.Precio = x.Precio;
                    vino.Stock = x.Stock;
                }
                return vino;
            }

        }

        [HttpPost]
        public IHttpActionResult CrearVino([FromBody]VinoViewModel vino)
        {
            if (ModelState.IsValid)
            {
                objDB.Vinoes.Add(new Vino() {
                    ImagenUri = vino.ImagenUri,
                    IdTipoVino = vino.IdTipoVino,
                    IdMarca = vino.IdMarca,
                    IdCepa = vino.IdCepa,
                    Pais = vino.Pais,
                    Cosecha = vino.Cosecha,
                    Precio = vino.Precio,
                    Stock = vino.Stock
                });
                objDB.SaveChanges();
                return Ok(vino);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut]
        public IHttpActionResult EditarVino(int Id, [FromBody]VinoViewModel vino)
        {
            if (ModelState.IsValid)
            {
                var regexiste = objDB.Vinoes.Count(c => c.Id == Id) > 0;

                if (regexiste)
                {
                    var x = objDB.Vinoes.Find(Id);
                    x.ImagenUri = vino.ImagenUri;
                    x.IdTipoVino = vino.IdTipoVino;
                    x.IdMarca = vino.IdMarca;
                    x.IdCepa = vino.IdCepa;
                    x.Pais = vino.Pais;
                    x.Cosecha = vino.Cosecha;
                    x.Precio = vino.Precio;
                    x.Stock = vino.Stock;

                    objDB.SaveChanges();
                    return Ok(vino);
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
        public IHttpActionResult EliminarVino(int Id)
        {

            var x = objDB.Vinoes.Find(Id);

            if (x != null)
            {
                objDB.Vinoes.Remove(x);
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
