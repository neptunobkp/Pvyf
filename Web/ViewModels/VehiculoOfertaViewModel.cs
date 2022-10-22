using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Model;

namespace Web.ViewModels
{
    public class VehiculoOfertaViewModel
    {
        [Required(ErrorMessage = "Campo requerido")]
        public string Patente { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public int? ModeloId { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public int? MarcaId { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public int? Anio { get; set; }
        public int? Kilometraje { get; set; }

        public string ColorInterior { get; set; }
        public string ColorExterior { get; set; }
        public int? CantidadDuenos { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public int? Precio { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public string Rut { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public string Telefono { get; set; }

        public string Observaciones { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public int? VersionId { get; set; }
        public string TipoVehiculo { get; set; }

        public string Estado { get; set; }

        [EmailAddress(ErrorMessage = "Correo no válido")]
        [Required(ErrorMessage = "Campo requerido")]
        public string Correo { get; set; }


        public string DireccionInspeccion { get; set; }
        [Required(ErrorMessage = "Ingrese una fecha por favor")]
        public DateTime? FechaInspeccion { get; set; }

        [Required(ErrorMessage = "Ingrese su comuna por favor")]
        public int? ComunaId { get; set; }

        [Required(ErrorMessage = "Ingrese su región por favor")]
        public int? RegionId { get; set; }
        public string ComunaInspeccion { get; set; }

        public bool IncluyeInspeccion { get; set; }
    }
}
