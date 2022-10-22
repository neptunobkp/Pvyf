using System.Collections.Generic;
using System.Security.AccessControl;
using Web.Helpers.CargaRnvm;

namespace Web.Helpers.CargaSii
{
    public class LineaSiiCsv
    {
        public LineaSiiCsv()
        {
            Vehiculos = new List<LineaRnvmCsv>();
        }

        public int? Anio { get; set; }
        public string Tipo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Version { get; set; }

        public int? Puertas { get; set; }
        public int? Cilindrada { get; set; }
        public int? Hp { get; set; }

        public string Combustible { get; set; }
        public string Transmision { get; set; }
        public int? Marchas { get; set; }
        public string Traccion { get; set; }
        public string Pais { get; set; }
        public string Equipamiento { get; set; }
        public int? Tasacion { get; set; }


        public List<LineaRnvmCsv> Vehiculos { get; set; }

    }
}