using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Model.Tipos;

namespace Web.Model.CompraVende
{
    public class PublicacionUsado
    {
        public PublicacionUsado()
        {
            Fotos = new List<FotoPublicacionUsado>();
        }

        public int Id { get; set; }
        public DateTime Fecha { get; set; }

        public int? VehiculoId { get; set; }
        public Vehiculo Vehiculo { get; set; }

        public int? CantidadDuenos { get; set; }
        public int? PrecioVehiculo { get; set; }

        public int? Rut { get; set; }
        public string Nombres { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }

        public string Direccion { get; set; }
        public string Observaciones { get; set; }

        public List<FotoPublicacionUsado> Fotos { get; set; }

        public int? OfertanteId { get; set; }
        public Usuario Ofertante { get; set; }

        public int? RegionId { get; set; }
        public Region Region { get; set; }
    }

  

   
}
