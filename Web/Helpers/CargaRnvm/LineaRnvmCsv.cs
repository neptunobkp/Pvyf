using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Helpers.CargaRnvm
{
    public class LineaRnvmCsv
    {
        public int? Rut { get; set; }
        public string Nombres { get; set; }
        public string Patente { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public string TipoVehiculo { get; set; }
        public string Anio { get; set; }
        public string NumeroMotor { get; set; }
        public string NumeroChasis { get; set; }
        public string Tasacion { get; set; }
    }

    public enum IndiceRnvmCsv
    {
        Rut,
        Nombres = 5,
        Patente,
        Marca = 8,
        Modelo,
        Color,
        TipoVehiculo,
        Anio,
        NumeroMotor = 14, 
        NumeroChasis,
        Tasacion,
    }
}
