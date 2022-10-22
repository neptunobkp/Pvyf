using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Helpers;
using Web.ViewModels;

namespace Web.Infraestructura.PaginasBase
{
    public class PaginaConFIltrosPublicacionesNuevo : PageModel
    {
        protected ApplicationDbContext _db;

        public PaginaConFIltrosPublicacionesNuevo(ApplicationDbContext db)
        {
            _db = db;
        }

        protected async Task<SelectList> ObtenerCantidadesAsientosPublicados()
        {
            return (await _db.EntidadesLista.FromSql(
                    @"
                        select 
                        distinct 
                        vers.asientos Valor, 
                        CAST(vers.asientos AS NVARCHAR(3)) texto 
                        from Versiones vers 
                        where exists (select 1 from PublicacionesNuevos pnu where pnu.VersionId = vers.Id)
                    ")
                .ToListAsync()).AsSelectList();
        }

        protected async Task<SelectList> ObtenerMarcasPublicadas()
        {
            return (await _db.EntidadesLista.FromSql(
                      @"
                    select mar.id Valor,
                    mar.Nombre Texto
                    from marcas mar
                    where exists
                    (
                        select 1
                        from PublicacionesNuevos pnu
                            join versiones vers
                                on vers.Id = pnu.VersionId
                            join modelos m
                                on m.Id = vers.ModeloId
                        where m.MarcaId = mar.Id
                    )
                    ")
                  .ToListAsync()).AsSelectList();
        }

        protected async Task<SelectList> ObtenerTiposVehiculosPublicados()
        {
            return (await _db.ItemsLista.FromSql(
                    @"select distinct 
                        vers.tipovehiculo Valor, 
                        vers.tipoVehiculo texto 
                    from Versiones vers 
                    where exists (select 1 from PublicacionesNuevos pnu where pnu.VersionId = vers.Id)")
                .ToListAsync()).AsSelectList();
        }

        protected async Task<SelectList> ObtenerCombustiblesPublicados()
        {
            return (await _db.ItemsLista.FromSql(
                    @"select distinct 
                        vers.Combustible Valor, 
                        vers.Combustible texto 
                    from Versiones vers 
                    where exists (select 1 from PublicacionesNuevos pnu where pnu.VersionId = vers.Id)")
                .ToListAsync()).AsSelectList();
        }

        protected async Task<SelectList> ObtenerTransmisionesPublicados()
        {
            return (await _db.ItemsLista.FromSql(
                    @"select distinct 
                        vers.Transmision Valor, 
                        vers.Transmision texto 
                    from Versiones vers 
                    where exists (select 1 from PublicacionesNuevos pnu where pnu.VersionId = vers.Id)")
                .ToListAsync()).AsSelectList();
        }

        protected SelectList ConstruirListadoPrecios()
        {
            var nfi = ConfigurarFormatoMonedo();
            int valorInicial = 500000;
            var itemLista = new List<ItemLista> { new ItemLista { Valor = valorInicial.ToString(), Texto = $"${valorInicial.ToString("n", nfi)}", } };

            for (int i = 1000000; i < 22000000; i += 500000)
                itemLista.Add(new ItemLista { Valor = i.ToString(), Texto = $"${i.ToString("n", nfi)}", });

            return new SelectList(itemLista, "Valor", "Texto");
        }

        private static NumberFormatInfo ConfigurarFormatoMonedo()
        {
            NumberFormatInfo nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = ".";
            nfi.NumberDecimalDigits = 0;
            return nfi;
        }
    }
}
