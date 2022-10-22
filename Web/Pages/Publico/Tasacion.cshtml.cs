using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Infraestructura;
using Web.Model.Vendenos;
using Web.ViewModels;
using System;

namespace Web.Pages.Publico
{
    public class TasacionModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly ProteccionData _protector;
        [BindProperty] public TasacionVenodesViewModel Vm { get; set; }

        public SelectList Comunas { get; set; }

        public TasacionModel(ApplicationDbContext db, ProteccionData protectorData)
        {
            _db = db;
            _protector = protectorData;
        }

        public void OnGet(string id)
        {
            var decodedId = 0;
            var encriptedId = _protector.Encode(id);
            if (Int32.TryParse(encriptedId, out decodedId) && decodedId > 0)
            {
                Comunas = new SelectList(_db.Comunas.Select(t => t.Nombre).Distinct().ToList());
                InitVm(decodedId);
            }
            else
                Vm = new TasacionVenodesViewModel { IntencionVendenosId = decodedId };
        }

        private void InitVm(int id)
        {
            var nfi = ConfigurarFormatoMonedo();
            var intencion = _db.IntencionesVendenos.Find(id);
            var vehiculo = _db.Vehiculos
                .Include(t => t.Version)
                .ThenInclude(t => t.Modelo)
                .ThenInclude(t => t.Marca)
                .Single(t => t.Id == intencion.VehiculoId);
            var descuento = intencion.PorcentajeDescuento.GetValueOrDefault();
            var elT = (decimal)intencion.Valor.GetValueOrDefault() * .85m * (100 - descuento)/100;
            var elTMin = elT;
            var elTMax = elTMin * .98m;

            Vm = new TasacionVenodesViewModel
            {
                Correo = intencion.Correo,
                GlosaVehiculo = $"{vehiculo.Anio}   {vehiculo.Version?.Modelo?.Marca?.Nombre} {vehiculo.Version?.Modelo?.Nombre}",
                IntencionVendenosId = id,
                OfertaDesde = $"{elTMin.ToString("n", nfi)}",
                OfertaHasta = $"{elTMax.ToString("n", nfi)}"
            };
        }

        private static NumberFormatInfo ConfigurarFormatoMonedo()
        {
            NumberFormatInfo nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = ".";
            nfi.NumberDecimalDigits = 0;
            return nfi;
        }

        public async Task<IActionResult> OnPost()
        {
            var intencionVendenos = _db.IntencionesVendenos.Find(Vm.IntencionVendenosId);
            intencionVendenos.Nombre = Vm.Nombre;
            intencionVendenos.Telefono = Vm.Telefono;
            intencionVendenos.Correo = Vm.Correo;

            var intencionInspeccion = _db.IntencionesInspecciones.Add(new IntencionInspeccion
            {
                Direccion = Vm.Direccion,
                IntencionVendenosId = Vm.IntencionVendenosId
            });

            await _db.SaveChangesAsync();
            return RedirectToPage("Gracias", new { id = _protector.Decode(intencionInspeccion.Entity.Id.ToString()) });
        }

    }
}