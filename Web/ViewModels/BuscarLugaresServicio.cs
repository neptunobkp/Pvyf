using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class BuscarLugaresServicio
    {
        public string Nombre { get; set; }
        public string Servicio { get; set; }

    }

    public class EnvioVoucher
    {
        public string Destinatario { get; set; }
        public string IdServicio { get; set; }
    }

    public class LugarServicioViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Servicios { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
    }
}
