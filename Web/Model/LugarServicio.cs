using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Web.Model
{
    public class LugarServicio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Servicios { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
    }
}
