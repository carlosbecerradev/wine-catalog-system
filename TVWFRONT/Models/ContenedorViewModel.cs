using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TVWFRONT.Models
{
    public class ContenedorViewModel
    {
        public IEnumerable<VinoViewModel> lstVinos { get; set; }
        public IEnumerable<TipoVinoViewModel> lstTVino { get; set; }
        public IEnumerable<MarcaViewModel> lstMarcas { get; set; }
        public IEnumerable<CepaViewModel> lstCepas { get; set; }
    }
}