using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Model
{
    public class Tasacion
    {
        public int Id { get; set; }
        public int VersionId { get; set; }
        public Versiona Version { get; set; }
        public int Monto { get; set; }
        public int Anio { get; set; }
    }
}
