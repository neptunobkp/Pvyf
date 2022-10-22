using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Model.Tipos;

namespace Web.Model
{
    public class Modelo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public int MarcaId { get; set; }
        public Marca Marca { get; set; }

        public bool Eliminado { get; set; }
            
        public ICollection<Versiona> Versiones { get; set; }
    }
}
