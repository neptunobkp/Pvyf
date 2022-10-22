using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Helpers;
using Web.Infraestructura;
using Web.Infraestructura.PaginasBase;
using Web.Model;
using Web.ViewModels;

namespace Web.Pages.Publico
{
    public class NuevosModel : PaginaConFIltrosPublicacionesNuevo
    {
        private readonly ProteccionData _protector;

        public NuevosModel(ApplicationDbContext db, ProteccionData protectorData) : base(db)
        {
            _protector = protectorData;
        }

        [BindProperty] public FiltrosBusquedaNuevos Vm { get; set; }
        public SelectList ListaMarcas { get; set; }

        public SelectList ListaTiposVehiculo { get; set; }
        public SelectList ListaCombustible { get; set; }
        public SelectList ListaTransmision { get; set; }
        public SelectList CantidadesAsientos { get; set; }

        public SelectList ListaPrecios { get; set; }

        public async Task OnGet()
        {
            Vm = new FiltrosBusquedaNuevos();
            ListaTiposVehiculo = await ObtenerTiposVehiculosPublicados();
            ListaMarcas = await ObtenerMarcasPublicadas();
            CantidadesAsientos = await ObtenerCantidadesAsientosPublicados();
            ListaCombustible = await ObtenerCombustiblesPublicados();
            ListaTransmision = await ObtenerTransmisionesPublicados();
            ListaPrecios = ConstruirListadoPrecios();
        }


        public async Task<IActionResult> OnPost()
        {
            var busqueda = new Busqueda
            {
                Fecha = DateTime.Now,
                MarcaId = Vm.FnMarcaId,
                ModeloId = Vm.FnModeloId,
                TipoVehiculo = Vm.FnTipoVehiculo,
                TipoCombustible = Vm.Combustible,
                TipoTransmision = Vm.Transmision,
                CantidadAsientos = Vm.CantidadAsientos,
                PrecioMax = Vm.PrecioMax,
                PrecioMin = Vm.PrecioMin
            };

            var busquedaAdded = await _db.Busquedas.AddAsync(busqueda);
            await _db.SaveChangesAsync();

            return RedirectToPage("Resultados", "PorId", new { id = _protector.Decode(busquedaAdded.Entity.Id.ToString()) });
        }
    }
}