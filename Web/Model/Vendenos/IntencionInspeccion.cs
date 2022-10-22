using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Model.Vendenos
{
    public class IntencionInspeccion
    {
        public int Id { get; set; }
        public DateTime FechaCandidata { get; set; }
        public string Direccion { get; set; }
        public int IntencionVendenosId { get; set; }

        public int? ComunaId { get; set; }

        public int? TallerId { get; set; }
        public Taller Taller { get; set; }
        public IntencionVendenos IntencionVendenos { get; set; }
    }
}
