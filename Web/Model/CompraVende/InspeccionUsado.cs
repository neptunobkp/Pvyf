using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Model.CompraVende
{
    public class InspeccionUsado
    {
        public int Id { get; set; }
        public DateTime FechaCandidata { get; set; }
        public string Direccion { get; set; }

        public int? ComunaId { get; set; }
        public int? PublicacionUsadoId { get; set; }
        public PublicacionUsado PublicacionUsado { get; set; }
    }
}
