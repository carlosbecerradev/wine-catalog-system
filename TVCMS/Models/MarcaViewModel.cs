using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TVCMS.Models
{
    public class MarcaViewModel
    {
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Debe ingresar un nombre, revisar!")]
        public string Nombre { get; set; }
    }
}