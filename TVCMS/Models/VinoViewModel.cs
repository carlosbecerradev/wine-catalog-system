using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TVCMS.Models
{
    public class VinoViewModel
    {
        public int Id { get; set; }
        public string ImagenUri { get; set; }
        public int IdTipoVino { get; set; }
        public int IdMarca { get; set; }
        public int IdCepa { get; set; }
        public string Pais { get; set; }
        public System.DateTime Cosecha { get; set; }
        public Nullable<decimal> Precio { get; set; }
        public int Stock { get; set; }
    }
}