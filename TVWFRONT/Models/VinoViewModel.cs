using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TVWFRONT.Models
{
    public class VinoViewModel
    {

        public int Id { get; set; }
        public string ImagenUri { get; set; }
        public int IdTipoVino { get; set; }        
        public int IdMarca { get; set; }
        public int IdCepa { get; set; }
        public string Pais { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime Cosecha { get; set; }
        public Nullable<decimal> Precio { get; set; }
        public int Stock { get; set; }

        public virtual CepaViewModel Cepa { get; set; }
        public virtual MarcaViewModel Marca { get; set; }
        public virtual TipoVinoViewModel TipoVino { get; set; }

        [NotMapped]
        public List<TipoVinoViewModel> lstTVino { get; set; }
        [NotMapped]
        public List<MarcaViewModel> lstMarca { get; set; }
        [NotMapped]
        public List<CepaViewModel> lstCepa { get; set; }


    }
}