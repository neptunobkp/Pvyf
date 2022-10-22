using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class FiltrosBusquedaNuevos
    {
        public string FnTipoVehiculo { get; set; }
        public int? FnMarcaId { get; set; }
        public int? FnModeloId { get; set; }
        public int? FnVersionId { get; set; }

        public int? CantidadAsientos { get; set; }

        public int? PrecioMin { get; set; }
        public int? PrecioMax { get; set; }

        public string Combustible { get; set; }
        public string Transmision { get; set; }

        public string Traccion { get; set; }

        public int? CilindradaMin { get; set; }
        public int? CilindradaMax { get; set; }
    }
}
