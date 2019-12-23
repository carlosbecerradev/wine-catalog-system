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
    public class TipoVinoController : ApiController
    {
        private DBModel objDB = new DBModel();

        [HttpGet]
        public IEnumerable<TipoVinoViewModel> Get()
        {
            IList<TipoVinoViewModel> lstTVino = null;

            using (DBModel cn = new DBModel())
            {
                lstTVino = cn.TipoVinoes.Select(s => new TipoVinoViewModel()
                {
                    Id = s.Id,
                    Nombre = s.Nombre
                }).ToList<TipoVinoViewModel>();

                return lstTVino;
            }
        }

        [HttpGet]
        public TipoVinoViewModel Get(int Id)
        {
            TipoVinoViewModel tVino = new TipoVinoViewModel();
            using (DBModel cn = new DBModel())
            {
                var x = cn.TipoVinoes.FirstOrDefault(e => e.Id == Id);
                if (x != null)
                {
                    tVino.Id = x.Id;
                    tVino.Nombre = x.Nombre;
                }
                return tVino;
            }

        }

        [HttpPost]
        public IHttpActionResult CrearTVino([FromBody]TipoVinoViewModel tVino)
        {
            if (ModelState.IsValid)
            {
                objDB.TipoVinoes.Add(new TipoVino() { Nombre = tVino.Nombre });
                objDB.SaveChanges();
                return Ok(tVino);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut]
        public IHttpActionResult EditarTVino([FromBody]TipoVinoViewModel tVino)
        {
            if (ModelState.IsValid)
            {
                var regexiste = objDB.TipoVinoes.Count(c => c.Id == tVino.Id) > 0;

                if (regexiste)
                {
                    var x = objDB.TipoVinoes.Find(tVino.Id);
                    x.Nombre = tVino.Nombre;

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
        public IHttpActionResult EliminarTVino(int Id)
        {

            var x = objDB.TipoVinoes.Find(Id);

            if (x != null)
            {
                objDB.TipoVinoes.Remove(x);
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
