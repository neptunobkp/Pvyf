using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Clauses;
using Web.Data;
using Web.Infraestructura;
using Web.Model.CompraVende;
using Web.ViewModels;

namespace Web.Areas.Ofertas.Controllers
{
    [Area("Ofertas")]
    [Route("[area]/api/[controller]")]
    [ApiController]
    public class OfertasController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ProteccionData _protector;
        public OfertasController(ApplicationDbContext db, ProteccionData protectorData)
        {
            _db = db;
            _protector = protectorData;
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IList<VehiculoViewModel>> Post([FromForm] WrapperVehiculoVendenosViewModel criterio)
        {
            var vehiculos = new List<VehiculoViewModel>();
            IQueryable<PublicacionUsado> ofertasQuery = _db.PublicacionesUsados
                .Include(t => t.Vehiculo)
                .ThenInclude(p => p.Version.Modelo.Marca)
                .Include(t => t.Fotos);

            if (criterio.Vm.MarcaId.HasValue)
                ofertasQuery = ofertasQuery.Where(t =>
                    t.Vehiculo.Version.Modelo != null && t.Vehiculo.Version.Modelo.MarcaId == criterio.Vm.MarcaId);

            if (criterio.Vm.ModeloId.HasValue)
                ofertasQuery = ofertasQuery.Where(t =>
                    t.Vehiculo.Version.Modelo != null && t.Vehiculo.Version.ModeloId == criterio.Vm.ModeloId);

            if (criterio.Vm.Anio.HasValue)
                ofertasQuery = ofertasQuery.Where(t =>
                    t.Vehiculo.Version.Modelo != null && t.Vehiculo.Anio == criterio.Vm.Anio);

            var ofertas = await ofertasQuery.ToListAsync();
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
                    Precio = String.Format("{0:n0}", oferta.PrecioVehiculo.GetValueOrDefault()),
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