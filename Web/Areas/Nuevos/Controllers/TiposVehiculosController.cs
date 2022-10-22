using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.ViewModels;

namespace Web.Areas.Nuevos.Controllers
{
    [Area("Nuevos")]
    [Route("[area]/api/[controller]")]
    [ApiController]
    public class TiposVehiculosController : ControllerBase
    {

        private readonly ApplicationDbContext _db;

        public TiposVehiculosController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("{tipoVehiculo}/Marcas")]
        public async Task<IEnumerable<EntidadLista>> MarcasTipadas(string tipoVehiculo)
        {
            return await _db.EntidadesLista.FromSql(
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
                              and vers.TipoVehiculo = @tipo
                    )
                    ", new SqlParameter("tipo", tipoVehiculo))
                .OrderBy(t => t.Texto)
                .ToListAsync();
        }

        [HttpGet("{tipoVehiculo}/Marcas/{marcaId}/Modelos")]
        public async Task<IEnumerable<EntidadLista>> ModelosTipados(string tipoVehiculo, int marcaId)
        {
            return await _db.EntidadesLista.FromSql(
                @"
                select modA.Id Valor,
                modA.Nombre Texto
                from modelos modA
                where exists
                (
                    select 1
                    from 
		                PublicacionesNuevos pnu
		                join versiones vers
			                on vers.id = pnu.VersionId
                        join modelos m
                            on m.Id = vers.ModeloId
                    where m.Id = moda.Id
                          and m.MarcaId = @marca
                          and vers.TipoVehiculo = @tipo
                )
                ",
                new SqlParameter("marca", marcaId), new SqlParameter("tipo", tipoVehiculo)).OrderBy(t => t.Texto).ToListAsync();
        }

        [HttpGet("{tipoVehiculo}/Marcas/{marcaId}/Modelos/{modeloId}/versiones")]
        public async Task<IEnumerable<EntidadLista>> VersionesTipadas(string tipoVehiculo, int marcaId, int modeloId)
        {
            return await _db.EntidadesLista.FromSql(
                @"
                select vers.Id Valor,
                 vers.Nombre Texto
                from 
	                PublicacionesNuevos pnu
	                join Versiones vers
		                on vers.Id = pnu.VersionId
                    join modelos m
                        on m.id = vers.ModeloId
                where m.MarcaId = @marca
                      and ModeloId = @modelo
                      and vers.TipoVehiculo = @tipo
                ",
                new SqlParameter("marca", marcaId), new SqlParameter("modelo", modeloId), new SqlParameter("tipo", tipoVehiculo)).OrderBy(t => t.Texto).ToListAsync();
        }
    }
}