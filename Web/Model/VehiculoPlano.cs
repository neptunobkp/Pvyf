using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Model
{
    public class VehiculoPlano
    {
        public int Id { get; set; }
        public string Patente { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string TipoVehiculo { get; set; }
        public string Color { get; set; }
        public string Anio { get; set; }
        public string Tasacion { get; set; }
        public string NumeroChasis { get; set; }
        public string NumeroMotor { get; set; }
    }
}
