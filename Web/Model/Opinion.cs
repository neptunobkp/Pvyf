using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Model
{
    public class Opinion
    {
        public Opinion()
        {
            Fecha = DateTime.Now;
        }
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public String Titulo { get; set; }
        public String DetalleBreve { get; set; }
        public String TextoPrincipal { get; set; }
        public String UriArchivoFoto { get; set; }
        public String RangoPrecios { get; set; }
        public int Puntuacion { get; set; }
    }
}
