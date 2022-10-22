using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Model;
using Web.Model.Historico;
using Web.ViewModels;

namespace Web.Areas.Vehiculos.Controllers
{
    [Area("Vehiculos")]
    [Route("[area]/api/[controller]")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public VehiculosController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("[action]")]
        public IEnumerable<Marca> Marcas()
        {
            return _db.Marcas.ToList();
        }

        [HttpGet("[action]/{marcaid}")]
        public IEnumerable<Modelo> Modelos(int marcaId)
        {
            return _db.Modelos.Where(t => t.MarcaId == marcaId).ToList();
        }

        [HttpGet("TiposVehiculos/{tipoVehiculo}/Marcas")]
        public async Task<IEnumerable<EntidadLista>> MarcasTipadas(string tipoVehiculo)
        {
            return await _db.EntidadesLista.FromSql(
                $"select mar.id Valor, mar.Nombre Texto from marcas mar where exists (select 1 from versiones vers join modelos m on m.Id = vers.ModeloId where vers.TipoVehiculo = @tipo)", new SqlParameter("tipo", tipoVehiculo))
                .OrderBy(t => t.Texto)
                .ToListAsync();
        }

        [HttpGet("TiposVehiculos/{tipoVehiculo}/Marcas/{marcaId}/Modelos")]
        public async Task<IEnumerable<EntidadLista>> ModelosTipados(string tipoVehiculo, int marcaId)
        {
            return await _db.EntidadesLista.FromSql(
                $"select modA.Id Valor, modA.Nombre Texto from modelos modA where exists  (select 1 from versiones vers join modelos m on m.Id = vers.ModeloId  where m.Id = moda.Id and m.MarcaId = @marca and vers.TipoVehiculo = @tipo)",
                new SqlParameter("marca", marcaId), new SqlParameter("tipo", tipoVehiculo)).OrderBy(t => t.Texto).ToListAsync();
        }

        [HttpGet("TiposVehiculos/{tipoVehiculo}/Marcas/{marcaId}/Modelos/{modeloId}/versiones")]
        public async Task<IEnumerable<EntidadLista>> VersionesTipadas(string tipoVehiculo, int marcaId, int modeloId)
        {
            return await _db.EntidadesLista.FromSql(
                $"select vers.Id Valor, vers.Nombre Texto from Versiones vers join modelos m on m.id	= vers.ModeloId where m.MarcaId = @marca and ModeloId = @modelo and vers.TipoVehiculo = @tipo",
                new SqlParameter("marca", marcaId), new SqlParameter("modelo", modeloId), new SqlParameter("tipo", tipoVehiculo)).OrderBy(t => t.Texto).ToListAsync();
        }

        [HttpGet("Marcas/{marcaId}/Modelos/{modeloId}/versiones")]
        public async Task<IEnumerable<EntidadLista>> VersionesMarcaModelo(int marcaId, int modeloId)
        {
            return await _db.EntidadesLista.FromSql(
                $"select vers.Id Valor, vers.Nombre Texto from Versiones vers join modelos m on m.id	= vers.ModeloId where m.MarcaId = @marca and ModeloId = @modelo",
                new SqlParameter("marca", marcaId), new SqlParameter("modelo", modeloId)).OrderBy(t => t.Texto).ToListAsync();
        }

        [HttpGet("[action]/{patente}")]
        public VehiculoVendenosViewModel Buscar(string patente)
        {
            var result = new VehiculoVendenosViewModel();

            var vehiculoBruto = _db.VehiculosPlanos.SingleOrDefault(t => t.Patente == patente);
            if (vehiculoBruto != null)
            {
                int parser = 0;
                return new VehiculoVendenosViewModel
                {
                    Bruto = true,
                    Patente = patente,
                    MarcaDesc = vehiculoBruto.Marca,
                    ModeloDesc = vehiculoBruto.Modelo,
                    TipoVehiculo = vehiculoBruto.TipoVehiculo,
                    Anio = Int32.TryParse(vehiculoBruto.Anio, out parser) ? parser : (int?)null
                };
            }

            return result;
        }

        [HttpGet("test")]
        public string Test()
        {
            int zero = 0;
            int result = 10 / zero;
            return result.ToString();
        }

        //    private async Task IdentificarModelo(Tasacion2019 cadaT2019, Tasacion tasacionN)
        //    {
        //        if (await _db.Marcas.AnyAsync(t => t.Nombre == cadaT2019.Marca))
        //        {
        //            var marcaExistente = await _db.Marcas.FirstAsync(t => t.Nombre == cadaT2019.Marca);
        //            if (await _db.Modelos.AnyAsync(t => t.Nombre == cadaT2019.Modelo))
        //            {
        //                var modeloExistente = await _db.Modelos.FirstAsync(t => t.Nombre == cadaT2019.Modelo);
        //                tasacionN.ModeloId = modeloExistente.Id;
        //            }
        //            else
        //            {
        //                var modeloNuevo = await _db.Modelos.AddAsync(new Modelo { MarcaId = marcaExistente.Id, Nombre = cadaT2019.Modelo });

        //                tasacionN.ModeloId = modeloNuevo.Entity.Id;
        //            }
        //        }
        //        else
        //        {
        //            var nuevaMarca = await _db.Marcas.AddAsync(new Marca { Nombre = cadaT2019.Marca });
        //            var nuevoModelo = await _db.Modelos.AddAsync(new Modelo { MarcaId = nuevaMarca.Entity.Id, Nombre = cadaT2019.Modelo });
        //            tasacionN.ModeloId = nuevoModelo.Entity.Id;
        //        }
        //        #region help
        //        /*
        //               var tasaciones2019 = _db.Tasaciones2019.ToList();

        //            foreach (var cadaT2019 in tasaciones2019)
        //            {
        //                var tasacionN = new Tasacion
        //                {
        //                    Anio = cadaT2019.Anio,
        //                    Monto = cadaT2019.Tasa
        //                };

        //                await IdentificarModelo(cadaT2019, tasacionN);
        //                _db.Tasaciones.Add(tasacionN);
        //            }

        //            await _db.SaveChangesAsync();
        //             * select distinct Modelo, (select top 1 mar.id from marcas mar where mar.nombre = tass.marca) from Tasaciones2019 tass

        //insert into tasaciones
        //select 
        //(	select top 1 mode.id 
        //    from modelos mode 
        //    where mode.nombre = tass.modelo and mode.MarcaId = (select top 1 mar.id from marcas mar where mar.nombre = tass.marca )
        //),
        //tass.tasa,
        //tass.Anio
        //from Tasaciones2019 tass
        //             */
        //        #endregion
        //    }
    }
}