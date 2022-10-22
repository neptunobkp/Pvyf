using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class LandingPageViewModel
    {
        public LandingPageViewModel()
        {
            Vehiculos = new List<VehiculoViewModel>();
        }

        public string Titulo { get; set; }
        public string Hero { get; set; }
        public string Footer { get; set; }

        public List<VehiculoViewModel> Vehiculos { get; set; }
    }

    public class VehiculoViewModel
    {
        public VehiculoViewModel()
        {
            Fotos = new List<FotoViewModel>();
            AtributosLeft = new List<string>();
            AtributosRight = new List<string>();
            DetallesTecnicos = new List<ItemLista>();
        }
        public string IdEnc { get; set; }
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Precio { get; set; }
        public string Anio { get; set; }
        public string Url { get; set; }
        public string Descripcion { get; set; }
        public string Patente { get; set; }
        public string FechaPublicacion { get; set; }
        public string Ubicacion { get; set; }

        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Version { get; set; }

        public string Detalle { get; set; }

        public string MarcaId { get; set; }
        public string ModeloId { get; set; }

        public List<FotoViewModel> Fotos { get; set; }

        public List<string> AtributosLeft { get; set; }
        public List<string> AtributosRight { get; set; }

        public List<ItemLista> DetallesTecnicos { get; set; }

        public string DescripcionCorta { get; set; }
        public string DescripcionLarga { get; set; }

        public string Combustible { get; set; }
        public string TipoVehiculo { get; set; }
        public string TipoTransmision { get; set; }
        public string TipoTraccion { get; set; }
        public string ColorVehiculo { get; set; }
        public string ConsumoCombustible { get; set; }

        public string NombreVendedor { get; set; }
        public string TelefonoVendedor { get; set; }
    }

    public class FotoViewModel
    {
        public int Indice { get; set; }
        public string Data { get; set; }
        public string Nombre { get; set; }
    }
}
