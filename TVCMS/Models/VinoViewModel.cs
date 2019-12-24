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

        [DataType(DataType.ImageUrl)]
        [Required(ErrorMessage = "Debe seleccionar una imagen, revisar!")]
        public string ImagenUri { get; set; }

        [Display(Name = "Tipo Vino")]
        [Required(ErrorMessage = "Seleccionar tipo de vino, revisar!")]
        public int IdTipoVino { get; set; }

        [Display(Name = "Marca")]
        [Required(ErrorMessage = "Seleccionar una marca, revisar!")]
        public int IdMarca { get; set; }

        [Display(Name = "Cepa")]
        [Required(ErrorMessage = "Seleccionar una cepa, revisar!")]
        public int IdCepa { get; set; }

        [Required(ErrorMessage = "Seleccionar un país, revisar!")]
        public string Pais { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Seleccionar una fecha, revisar!")]
        public System.DateTime Cosecha { get; set; }

        [Range(1, 500)]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Debe ingresar un precio, revisar!")]
        public Nullable<decimal> Precio { get; set; }

        [Range(1, 3000)]
        [Required(ErrorMessage = "Debe ingresar la cantidad de stock, revisar!")]
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
        [NotMapped]
        public List<string> lstPais { get; set; }

    }
}