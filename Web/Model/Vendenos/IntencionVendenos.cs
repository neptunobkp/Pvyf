using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Model.Vendenos
{
    public class IntencionVendenos
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int? VehiculoId { get; set; }
        public Vehiculo Vehiculo { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int? Kilometraje { get; set; }
        public int? AnoTasacion { get; set; }
        public int? TasacionId { get; set; }
        public Tasacion Tasacion { get; set; }
        public int? Valor { get; set; }
        public int? PorcentajeDescuento { get; set; }
        public bool Contactado { get; set; }
        public string ObservacionContacto { get; set; }
    }
}
