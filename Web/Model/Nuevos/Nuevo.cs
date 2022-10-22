using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Model.CompraVende;

namespace Web.Model.Nuevos
{
    public class PublicacionNuevo
    {
        public PublicacionNuevo()
        {
            FechaPublicacion = DateTime.Now;
            Fotos = new List<FotoPublicacionNuevo>();
        }

        public int Id { get; set; }
        public int VersionId { get; set; }
        public Versiona Version { get; set; }

        public int Precio { get; set; }
        public string Observaciones { get; set; }

        public string DescripcionCorta { get; set; }
        public string DescripcionLarga { get; set; }

        public int? VendedorId { get; set; }
        public Vendedor Vendedor { get; set; }

        public DateTime FechaPublicacion { get; set; }

        public List<FotoPublicacionNuevo> Fotos { get; set; }
    }
}
