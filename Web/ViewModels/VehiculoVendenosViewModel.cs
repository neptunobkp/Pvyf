using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Model;

namespace Web.ViewModels
{
    public class WrapperVehiculoVendenosViewModel
    {
        public VehiculoVendenosViewModelNoV Vm { get; set; }
    }

    public class WrapperFiltrosVehiculoNuevo
    {
        public FiltrosBusquedaNuevos Vm { get; set; }
    }

    public class VehiculoVendenosViewModelNoV
    {
        public string Patente { get; set; }

        public int? ModeloId { get; set; }

        public int? MarcaId { get; set; }

  
        public int? Anio { get; set; }
        public int? Kilometraje { get; set; }

    
        public string Correo { get; set; }
    }

    public class VehiculoVendenosViewModel
    {
        [Required(ErrorMessage = "Ingrese la patente de su vehículo")]
        public string Patente { get; set; }

        [Required(ErrorMessage = "Seleccione el modelo de su vehículo")]
        public int? ModeloId { get; set; }

        [Required(ErrorMessage = "Seleccione su tipo de vehículo")]
        public string TipoVehiculo { get; set; }


        [Required(ErrorMessage = "Seleccione la versiòn de su vehículo")]
        public int? VersionId { get; set; }

        [Required(ErrorMessage =  "Seleccione la marca de su vehículo")]
        public int? MarcaId { get; set; }

        [Range(1990, 2021, ErrorMessage = "Ingrese un año válido")]
        [Required(ErrorMessage = "Ingrese el año")]
        public int? Anio { get; set; }

        [Required(ErrorMessage = "Ingrese el kilometraje")]
        public string Kilometraje { get; set; }

        [EmailAddress(ErrorMessage = "Ingrese un correo válido")]
        [Required(ErrorMessage = "Ingrese un correo")]
        public string Correo { get; set; }

        public bool Bruto { get; set; }

        public string MarcaDesc { get; set; }
        public string ModeloDesc { get; set; }

    }

    public class ItemLista
    {
        public string Valor { get; set; }
        public string Texto { get; set; }
    }

    public class EntidadLista
    {
        public int Valor { get; set; }
        public string Texto { get; set; }
    }
}
