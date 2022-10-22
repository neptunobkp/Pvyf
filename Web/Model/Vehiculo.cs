using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Model.CompraVende;
using Web.Model.Tipos;

namespace Web.Model
{
    public class Vehiculo
    {
        public int Id { get; set; }
        public string Patente { get; set; }

        public int? Anio { get; set; }

        public int? VersionId { get; set; }
        public Versiona Version { get; set; }

        public int? Kilometraje { get; set; }

        public string ColorInterior { get; set; }
        public string ColorExterior { get; set; }

        public string NumeroMotor { get; set; }
        public string NumeroChasis { get; set; }

        public TiposEstado Estado { get; set; }
    }
}
