using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TVCMS.Models
{
    public class VinoViewModel
    {
        public int Id { get; set; }
        public string ImagenUri { get; set; }
        [Display(Name = "Tipo Vino", Description = "Nombre")]
        public int IdTipoVino { get; set; }
        [Display(Name = "Marca")]
        public int IdMarca { get; set; }
        [Display(Name = "Cepa")]
        public int IdCepa { get; set; }
        public string Pais { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime Cosecha { get; set; }
        public Nullable<decimal> Precio { get; set; }
        public int Stock { get; set; }

        [NotMapped]
        public List<TipoVinoViewModel> lstTVino { get; set; }
        [NotMapped]
        public List<MarcaViewModel> lstMarca { get; set; }
        [NotMapped]
        public List<CepaViewModel> lstCepa { get; set; }
        [NotMapped]
        public List<string> lstPais { get; set; }

    }
}