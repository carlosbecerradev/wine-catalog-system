using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TVCMS.Models
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Debe ingresar un nombre, revisar!")]
        public string Nombre { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Debe ingresar un nombre, revisar!")]
        public string Contrasenia { get; set; }
        
        [Required(ErrorMessage = "Debe ingresar 1 o 0, revisar!")]
        public bool Estado { get; set; }
        [Display(Name = "Role")]
        [Required(ErrorMessage = "Seleccionar rol, revisar!")]
        public int IdRole { get; set; }
        
        public virtual RoleViewModel Role { get; set; }

        [NotMapped]
        public List<RoleViewModel> lstRole { get; set; }
    }
}