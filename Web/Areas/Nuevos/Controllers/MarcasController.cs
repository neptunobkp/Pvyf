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
    public class MarcasController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public MarcasController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("{marcaId}/Modelos")]
        public async Task<IEnumerable<EntidadLista>> GetModelos(int marcaId)
        {
            return await _db.EntidadesLista.FromSql(
                @"
                select 
                modA.Id Valor,
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
                )
                ",
                new SqlParameter("marca", marcaId)).OrderBy(t => t.Texto).ToListAsync();
        }

        [HttpGet("{marcaId}/Modelos/{modeloId}/versiones")]
        public async Task<IEnumerable<EntidadLista>> GetVersiones(int marcaId, int modeloId)
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
                ",
                new SqlParameter("marca", marcaId), new SqlParameter("modelo", modeloId)).OrderBy(t => t.Texto).ToListAsync();
        }
    }
}