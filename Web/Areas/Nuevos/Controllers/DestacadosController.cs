using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Helpers;
using Web.Infraestructura;
using Web.Model;
using Web.Model.Nuevos;
using Web.ViewModels;

namespace Web.Areas.Nuevos.Controllers
{
    [Area("Nuevos")]
    [Route("[area]/api/[controller]")]
    [ApiController]
    public class DestacadosController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ProteccionData _protector;

        public DestacadosController(ApplicationDbContext db, ProteccionData protector)
        {
            _db = db;
            _protector = protector;
        }

        public async Task<IList<VehiculoViewModel>> Get()
        {
            var vehiculos = new List<VehiculoViewModel>();

            IQueryable<PublicacionNuevo> ofertasQuery = _db.PublicacionesNuevos
                .Include(t => t.Version.Modelo.Marca)
                .Include(t => t.Fotos)
                .Take(4);

            var ofertas = await ofertasQuery.ToListAsync();
            ConfigurarVehiculos(ofertas, vehiculos);

            return vehiculos;

        }

        private void ConfigurarVehiculos(List<PublicacionNuevo> ofertas, List<VehiculoViewModel> vehiculos)
        {
            foreach (var oferta in ofertas)
            {
                string archivoData = "";
                if (oferta.Fotos.Count > 0 && oferta.Fotos[0].ArchivoId > 0)
                {
                    var image = _db.Archivos.Find(oferta.Fotos.First().ArchivoId);
                    archivoData = $"data:{image.Extension};base64," +
                                  Convert.ToBase64String(image.ArchivoBytes, 0, image.ArchivoBytes.Length);
                }

                vehiculos.Add(new VehiculoViewModel
                {
                    Id = oferta.Id,
                    IdEnc = _protector.Decode(oferta.Id.ToString()),
                    Precio = StringHelpers.FormatearMiles(oferta.Precio.ToString()),
                    FechaPublicacion = oferta.FechaPublicacion.ToShortDateString(),
                    Marca = oferta.Version?.Modelo?.Marca?.Nombre,
                    MarcaId = oferta.Version?.Modelo?.MarcaId.ToString(),
                    ModeloId = oferta.Version?.Modelo?.Id.ToString(),
                    Modelo = oferta.Version?.Modelo?.Nombre,
                    Url = archivoData,
                    Combustible = oferta.Version.Combustible,
                    TipoVehiculo = oferta.Version.TipoVehiculo.ToString()
                });
            }
        }
    }
}