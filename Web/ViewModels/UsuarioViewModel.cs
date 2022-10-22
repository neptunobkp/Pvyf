using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool EsAdministrador { get; set; }
        public string Contrasena { get; set; }
        public string Correo { get; set; }
    }
}
