using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Model.Historico
{
    public class Tasacion2019
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string ModeloVersion { get; set; }
        public int Tasa { get; set; }
        public int Anio { get; set; }
    }
}
