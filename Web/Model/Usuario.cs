using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Model
{
    public class Usuario 
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool EsAdministrador { get; set; }
        public string IdentificadorExterno { get; set; }
        public string Contrasena { get; set; }
        public string Correo { get; set; }
        public int? Rut { get; set; }
        public bool Deshabilitado { get; set; }
    }
}
