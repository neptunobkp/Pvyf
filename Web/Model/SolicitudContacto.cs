using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Model
{
    public class SolicitudContacto
    {
        public SolicitudContacto()
        {
            Fecha = DateTime.Now;
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Ingrese su nombre por favor")]
        public string Nombres { get; set; }

        [EmailAddress(ErrorMessage = "Ingrese un correo válido por favor")]
        [Required(ErrorMessage = "Ingrese su correo por favor")]
        public string Correo { get; set; }
        public string Celular { get; set; }

        [Required(ErrorMessage = "Ingrese su mensaje por favor")]
        public string Comentario { get; set; }
        public string Pagina { get; set; }
        public DateTime Fecha { get; set; }
        public bool Contactado { get; set; }
        public string ObservacionContacto { get; set; }
    }
}
