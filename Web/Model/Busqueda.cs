using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Model
{
    public class Busqueda
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int? MarcaId { get; set; }
        public int? ModeloId { get; set; }
        public string TipoVehiculo { get; set; }
        public int? PrecioMin { get; set; }
        public int? PrecioMax { get; set; }
        public int? CantidadAsientos { get; set; }
        public string TipoCombustible { get; set; }
        public string TipoTransmision { get; set; }
        public string ConsumoPromedio { get; set; }
    }
}
