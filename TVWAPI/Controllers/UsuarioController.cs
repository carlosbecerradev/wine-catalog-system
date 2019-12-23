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
    public class UsuarioController : ApiController
    {

        private DBModel objDB = new DBModel();

        // Buscar usuario para el login
        [HttpGet]
        public UsuarioViewModel Get(string nombre, string contrasenia)
        {
            UsuarioViewModel usr = new UsuarioViewModel();
            using (DBModel cn = new DBModel())
            {
                var x = cn.Usuarios.ToList().FirstOrDefault(e => (e.Nombre == nombre && e.Contrasenia == contrasenia));
                if (x != null)
                {
                    usr.Id = x.Id;
                    usr.Nombre = x.Nombre;
                    usr.Contrasenia = x.Contrasenia;
                    usr.Estado = x.Estado;
                    usr.IdRole = x.IdRole;
                }
                else
                {
                    usr.Id = -1;
                }
                return usr;
            }
        }

        [HttpGet]
        public IEnumerable<UsuarioViewModel> Get()
        {
            IList<UsuarioViewModel> lstUsuario = null;

            using (DBModel cn = new DBModel())
            {
                lstUsuario = cn.Usuarios.Select(s => new UsuarioViewModel()
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Contrasenia = s.Contrasenia,
                    Estado = s.Estado,
                    IdRole = s.IdRole
                }).ToList<UsuarioViewModel>();

                return lstUsuario;
            }
        }

        [HttpGet]
        public UsuarioViewModel Get(int Id)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            using (DBModel cn = new DBModel())
            {
                var x = cn.Usuarios.FirstOrDefault(e => e.Id == Id);
                if (x != null)
                {
                    usuario.Id = x.Id;
                    usuario.Nombre = x.Nombre;
                    usuario.Contrasenia = x.Contrasenia;
                    usuario.Estado = x.Estado;
                    usuario.IdRole = x.IdRole;
                }
                return usuario;
            }

        }

        [HttpPost]
        public IHttpActionResult CrearUsuario([FromBody]UsuarioViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                objDB.Usuarios.Add(new Usuario() {
                    Nombre = usuario.Nombre,
                    Contrasenia = usuario.Contrasenia,
                    Estado = usuario.Estado,
                    IdRole = usuario.IdRole
                });
                objDB.SaveChanges();
                return Ok(usuario);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut]
        public IHttpActionResult EditarUsuario([FromBody]UsuarioViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                var regexiste = objDB.Usuarios.Count(c => c.Id == usuario.Id) > 0;

                if (regexiste)
                {
                    var x = objDB.Usuarios.Find(usuario.Id);
                    x.Nombre = usuario.Nombre;
                    x.Contrasenia = usuario.Contrasenia;
                    x.Estado = usuario.Estado;
                    x.IdRole = usuario.IdRole;

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
        public IHttpActionResult EliminarUsuario(int Id)
        {

            var x = objDB.Usuarios.Find(Id);

            if (x != null)
            {
                objDB.Usuarios.Remove(x);
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
