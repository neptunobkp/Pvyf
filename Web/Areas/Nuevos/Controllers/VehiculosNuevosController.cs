using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
    public class VehiculosNuevosController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ProteccionData _protector;

        public VehiculosNuevosController(ApplicationDbContext db, ProteccionData protector)
        {
            _db = db;
            _protector = protector;
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IList<VehiculoViewModel>> Post([FromForm] WrapperFiltrosVehiculoNuevo criterio)
        {
            var vehiculos = new List<VehiculoViewModel>();
            IQueryable<PublicacionNuevo> publicaciones = _db.PublicacionesNuevos
                .Include(t => t.Version)
                .ThenInclude(p => p.Modelo)
                .ThenInclude(p => p.Marca)
                .Include(t => t.Fotos);

            if (!string.IsNullOrEmpty(criterio.Vm.FnTipoVehiculo))
                publicaciones = publicaciones.Where(t => t.Version.TipoVehiculo == criterio.Vm.FnTipoVehiculo);

            if (criterio.Vm.FnMarcaId.HasValue)
                publicaciones = publicaciones.Where(t => t.Version.Modelo.MarcaId == criterio.Vm.FnMarcaId);

            if (criterio.Vm.FnModeloId.HasValue)
                publicaciones = publicaciones.Where(t => t.Version.ModeloId == criterio.Vm.FnModeloId);

            if (criterio.Vm.FnVersionId.HasValue)
                publicaciones = publicaciones.Where(t => t.VersionId == criterio.Vm.FnVersionId);

            if (!string.IsNullOrEmpty(criterio.Vm.Combustible))
                publicaciones = publicaciones.Where(t => t.Version.Combustible == criterio.Vm.Combustible);

            if (!string.IsNullOrEmpty(criterio.Vm.Transmision))
                publicaciones = publicaciones.Where(t => t.Version.Transmision == criterio.Vm.Transmision);

            if (criterio.Vm.CilindradaMin.HasValue && criterio.Vm.CilindradaMin.Value > 0)
            {
                var valorCilindradaMinima = criterio.Vm.CilindradaMin * 1000;
                publicaciones = publicaciones.Where(t => t.Version.Cilindrada >= valorCilindradaMinima);
            }

            if (criterio.Vm.CilindradaMax.HasValue && criterio.Vm.CilindradaMax.Value < 10)
            {
                var valorCilindradaMaxima = criterio.Vm.CilindradaMax * 1000;
                publicaciones = publicaciones.Where(t => t.Version.Cilindrada <= valorCilindradaMaxima);
            }

            var resultado = await publicaciones.ToListAsync();
            ConfigurarVehiculos(resultado, vehiculos);
            return vehiculos;
        }

        public async Task<IList<VehiculoViewModel>> Get(int id)
        {
            var criterio = _db.Busquedas.Find(id);
            var vehiculos = new List<VehiculoViewModel>();
            IQueryable<PublicacionNuevo> ofertasQuery = _db.PublicacionesNuevos
                .Include(t => t.Version)
                .ThenInclude(t => t.Modelo).ThenInclude(t => t.Marca)
                .Include(t => t.Fotos);

            if (!string.IsNullOrEmpty(criterio.TipoVehiculo))
                ofertasQuery = ofertasQuery.Where(t => t.Version.TipoVehiculo == criterio.TipoVehiculo);

            if (criterio.MarcaId.HasValue)
                ofertasQuery = ofertasQuery.Where(t => t.Version.Modelo.MarcaId == criterio.MarcaId);

            if (criterio.ModeloId.HasValue)
                ofertasQuery = ofertasQuery.Where(t => t.Version.ModeloId == criterio.ModeloId);

            if (criterio.CantidadAsientos.HasValue)
                ofertasQuery = ofertasQuery.Where(t => t.Version.Asientos == criterio.CantidadAsientos.Value);

            if (!string.IsNullOrEmpty(criterio.TipoTransmision))
                ofertasQuery = ofertasQuery.Where(t => t.Version.Transmision == criterio.TipoTransmision);

            if (!string.IsNullOrEmpty(criterio.TipoCombustible))
                ofertasQuery = ofertasQuery.Where(t => t.Version.Combustible == criterio.TipoCombustible);

            if (criterio.PrecioMin.HasValue && criterio.PrecioMin.Value > 0)
            {
                var precioMinEnMillones = criterio.PrecioMin.Value ;
                ofertasQuery = ofertasQuery.Where(t => t.Precio >= precioMinEnMillones);
            }

            if (criterio.PrecioMax.HasValue && criterio.PrecioMin.Value > 0)
            {
                var precioMaxEnMillones = criterio.PrecioMax.Value;
                ofertasQuery = ofertasQuery.Where(t => t.Precio <= precioMaxEnMillones);
            }

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