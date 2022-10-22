using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Infraestructura;
using Web.Model.CompraVende;
using Web.ViewModels;

namespace Web.Areas.Ofertas.Controllers
{
    [Area("Ofertas")]
    [Route("[area]/api/[controller]")]
    [ApiController]
    public class MisOfertasController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ProteccionData _protector;

        public MisOfertasController(ApplicationDbContext db, ProteccionData protector)
        {
            _db = db;
            _protector = protector;

        }

        public async Task<IList<VehiculoViewModel>> Get(int usuarioId)
        {
            var vehiculos = new List<VehiculoViewModel>();
            var ofertas = await _db.PublicacionesUsados
                .Include(t => t.Vehiculo)
                .ThenInclude(p => p.Version.Modelo.Marca)
                .Include(t => t.Fotos)
                .Where(w => w.OfertanteId == usuarioId)
                .ToListAsync();

            ConfigurarVehiculos(ofertas, vehiculos);

            return vehiculos;
        }

        private void ConfigurarVehiculos(List<PublicacionUsado> ofertas, List<VehiculoViewModel> vehiculos)
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
                    Precio = oferta.PrecioVehiculo.GetValueOrDefault().ToString(),
                    Descripcion = oferta.Observaciones,
                    FechaPublicacion = oferta.Fecha.ToShortDateString(),
                    Patente = oferta.Vehiculo?.Patente,
                    Titulo = oferta.Vehiculo?.Patente,
                    Marca = oferta.Vehiculo?.Version?.Modelo?.Marca?.Nombre,
                    MarcaId = oferta.Vehiculo?.Version?.Modelo?.Marca?.Id.ToString(),
                    ModeloId = oferta.Vehiculo?.Version?.Modelo?.Id.ToString(),
                    Anio = oferta.Vehiculo?.Anio.GetValueOrDefault().ToString(),
                    Modelo = oferta.Vehiculo?.Version?.Modelo?.Nombre,
                    Url = archivoData,
                    Detalle =
                        $"{oferta.Vehiculo?.Version?.Modelo?.Marca?.Nombre} {oferta.Vehiculo?.Version?.Modelo?.Nombre} {oferta.Vehiculo?.Patente} {oferta.Observaciones} "
                });
            }
        }
    }
}