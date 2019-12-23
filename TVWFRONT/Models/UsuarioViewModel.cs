using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TVWFRONT.Models
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Contrasenia { get; set; }
        public bool Estado { get; set; }
        public int IdRole { get; set; }
    }
}