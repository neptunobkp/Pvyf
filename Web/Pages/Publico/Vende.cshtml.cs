using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Helpers;
using Web.Infraestructura;
using Web.Model;
using Web.Model.Vendenos;
using Web.ViewModels;

namespace Web.Pages.Publico
{
    public class VendeModel : PageModel
    {
        private ApplicationDbContext _db;
        private readonly ProteccionData _protector;

        public SelectList Marcas { get; set; }

        public VendeModel(ApplicationDbContext db, ProteccionData protectorData)
        {
            _db = db;
            _protector = protectorData;
        }

        [BindProperty] public VehiculoVendenosViewModel Vm { get; set; }
        public SelectList TiposVehiculo { get; set; }

        public void OnGet()
        {
            Vm = new VehiculoVendenosViewModel();
            TiposVehiculo = _db.ItemsLista.FromSql("select distinct TipoVehiculo Texto, TipoVehiculo Valor  from versiones").ToList().AsSelectList();
            Marcas = new SelectList(_db.Marcas.OrderBy(t => t.Nombre).ToList(), "Id", "Nombre");
        }

        public async Task<IActionResult> OnPost()
        {
            var vehiculoId = HandleVehiculo();
            var vehiculoPlano = _db.VehiculosPlanos.SingleOrDefault(t => t.Patente == Vm.Patente);
            if (vehiculoPlano != null)
            {
                int intparser;
                var intencion = _db.IntencionesVendenos.Add(new IntencionVendenos
                {
                    Fecha = DateTime.Now,
                    VehiculoId = vehiculoId,
                    Kilometraje = HandleKilometraje(),
                    Correo = Vm.Correo,
                    PorcentajeDescuento = 40,
                    Valor = int.TryParse(vehiculoPlano.Tasacion, out intparser) ? intparser : 0
                });

                await _db.SaveChangesAsync();
                return RedirectToPage("Tasacion", new { id = _protector.Decode(intencion.Entity.Id.ToString()) });
            }
            else
            {
                var tasacion = _db.Tasaciones.FirstOrDefault(t => t.VersionId == Vm.VersionId && t.Anio == Vm.Anio);
                if (tasacion == null)
                    return RedirectToPage("TasacionNoDisponible");

                int intparser;
                var intencion = _db.IntencionesVendenos.Add(new IntencionVendenos
                {
                    Fecha = DateTime.Now,
                    VehiculoId = vehiculoId,
                    Kilometraje = HandleKilometraje(),
                    Correo = Vm.Correo,
                    TasacionId = tasacion.Id,
                    PorcentajeDescuento = 40,
                    Valor = tasacion.Monto
                });

                await _db.SaveChangesAsync();
                return RedirectToPage("Tasacion", new { id = _protector.Decode(intencion.Entity.Id.ToString()) });
            }
        }

        private int? HandleVehiculo()
        {
            int vehiculoId;
            if (_db.Vehiculos.Any(t => t.Patente == Vm.Patente))
                vehiculoId = _db.Vehiculos.First(t => t.Patente == Vm.Patente).Id;
            else
            {
                var vehiculoAdded = _db.Vehiculos.Add(new Vehiculo
                {
                    VersionId = Vm.VersionId,
                    Anio = Vm.Anio,
                    Kilometraje = HandleKilometraje(),
                    Patente = Vm.Patente
                });
                _db.SaveChanges();
                vehiculoId = vehiculoAdded.Entity.Id;
            }

            return vehiculoId;
        }

        private int? HandleKilometraje()
        {
            int intparser;
            if (Vm.Kilometraje != null)
                return int.TryParse(Vm.Kilometraje.Replace(".", "").Replace(",", ""), out intparser) ? intparser : (int?)null;
            return null;
        }
    }
}