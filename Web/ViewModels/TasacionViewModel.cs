using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class TasacionViewModel
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Tasacion { get; set; }

        public string Patente { get; set; }

        public DateTime Fecha { get; set; }

        public bool Contactado { get; set; }
        public string ObservacionContacto { get; set; }
    }
}
