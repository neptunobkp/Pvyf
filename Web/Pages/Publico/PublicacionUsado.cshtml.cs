using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Infraestructura;
using Web.Services.Prismic;
using Web.ViewModels;

namespace Web.Pages.Publico
{
    public class VehiculoModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly ProteccionData _protector;

        public VehiculoModel(ApplicationDbContext db, ProteccionData protector)
        {
            _db = db;
            _protector = protector;
        }

        public VehiculoViewModel Vm { get; set; }

        public async Task OnGet(string id)
        {
            var decodedId = System.Convert.ToInt32(_protector.Encode(id));
            var oferta = await _db.PublicacionesUsados
                .Include(t => t.Vehiculo.Version.Modelo.Marca)
                .Include(t => t.Fotos)
                .Include(t => t.Region)
                .SingleAsync(t => t.Id == decodedId);

            Vm = new VehiculoViewModel
            {
                Id = oferta.Id,
                Precio = String.Format("{0:n0}", oferta.PrecioVehiculo.GetValueOrDefault()),
                Descripcion = Helpers.StringHelpers.CadenaADobleBarra(oferta.Observaciones),
                FechaPublicacion = oferta.Fecha.ToShortDateString(),
                Patente = oferta.Vehiculo?.Patente,
                Titulo = oferta.Vehiculo?.Patente,
                Marca = oferta.Vehiculo?.Version.Modelo.Marca.Nombre,
                Modelo = oferta.Vehiculo?.Version.Modelo.Nombre,
                Version = oferta.Vehiculo?.Version.Nombre,
                Anio = oferta.Vehiculo?.Anio.ToString(),
                TipoVehiculo = Helpers.StringHelpers.CadenaADobleBarra(oferta.Vehiculo?.Version.TipoVehiculo),
                Combustible = Helpers.StringHelpers.CadenaADobleBarra(oferta.Vehiculo?.Version.Combustible),
                TipoTransmision = Helpers.StringHelpers.CadenaADobleBarra(oferta.Vehiculo?.Version.Transmision),
                ColorVehiculo = Helpers.StringHelpers.CadenaADobleBarra(oferta.Vehiculo?.ColorExterior),
                ConsumoCombustible = Helpers.StringHelpers.CadenaADobleBarra(oferta.Vehiculo?.Version.RMixto.ToString()),
                TipoTraccion = Helpers.StringHelpers.CadenaADobleBarra(oferta.Vehiculo?.Version?.Traccion),
                Ubicacion = Helpers.StringHelpers.CadenaADobleBarra(oferta.Region?.Nombre),
                NombreVendedor = Helpers.StringHelpers.CadenaADobleBarra(oferta.Ofertante?.Nombre),
                TelefonoVendedor = Helpers.StringHelpers.CadenaADobleBarra(oferta.Telefono),
            };

            int indice = 1;
            foreach (var cadaFoto in oferta.Fotos)
            {
                var image = _db.Archivos.Find(cadaFoto.ArchivoId);
                Vm.Fotos.Add(new FotoViewModel()
                {
                    Indice = indice++,
                    Data = $"data:{image.Extension};base64," + Convert.ToBase64String(image.ArchivoBytes, 0, image.ArchivoBytes.Length),
                    Nombre = "fotox"
                });
            }
        }


    }
}