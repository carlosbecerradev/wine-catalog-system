using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TVCMS.Models
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Debe ingresar un tipo, revisar!")]
        public string Tipo { get; set; }
    }
}