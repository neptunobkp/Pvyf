using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Web.ViewModels
{
    public class TasacionVenodesViewModel
    {
        public string OfertaDesde { get; set; }
        public string OfertaHasta { get; set; }

        public string Direccion { get; set; }
        public string Comuna { get; set; }
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "Ingrese su comuna por favor")]
        public int? ComunaId { get; set; }

        public int? TallerId { get; set; }

        [Required(ErrorMessage = "Ingrese una fecha por favor")]
        public DateTime? Fecha { get; set; }

        [Required(ErrorMessage = "Ingrese su nombre por favor")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese su teléfono por favor")]
        public string Telefono { get; set; }

        [EmailAddress(ErrorMessage = "Ingrese un correo válido por favor")]
        [Required(ErrorMessage = "Ingrese su correo por favor")]
        public string Correo { get; set; }

        public string GlosaVehiculo { get; set; }

        [HiddenInput]
        public int IntencionVendenosId { get; set; }
    }
}
