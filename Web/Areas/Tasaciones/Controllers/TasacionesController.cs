using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.ViewModels;

namespace Web.Areas.Tasaciones.Controllers
{
    [Area("Tasaciones")]
    [Route("[area]/api/[controller]")]
    [ApiController]
    public class TasacionesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public TasacionesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<TasacionViewModel>> Get()
        {
            var tasaciones = await _db.IntencionesVendenos
                .Include(t => t.Vehiculo).ThenInclude(t => t.Version).ThenInclude(t => t.Modelo).ThenInclude(t => t.Marca)
                .Include(t => t.Tasacion)
                .AsNoTracking().ToListAsync();


            var result = new List<TasacionViewModel>();

            foreach (var cadaTasacion in tasaciones)
            {
                result.Add(new TasacionViewModel
                {
                    Id = cadaTasacion.Id,
                    Marca = cadaTasacion.Vehiculo.Version?.Modelo?.Marca?.Nombre,
                    Modelo = cadaTasacion.Vehiculo.Version?.Modelo?.Nombre,
                    Tasacion = cadaTasacion.Valor.GetValueOrDefault().ToString(),
                    Patente = cadaTasacion.Vehiculo?.Patente,
                    Fecha = cadaTasacion.Fecha,
                    Contactado = cadaTasacion.Contactado,
                    ObservacionContacto = cadaTasacion.ObservacionContacto
                });
            }

            return result;
        }
    }
}