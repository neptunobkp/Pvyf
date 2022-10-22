using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Model;
using Web.ViewModels;

namespace Web.Areas.Mantenciones.Controllers
{
    [Area("Mantenciones")]
    [Route("[area]/api/[controller]")]
    [ApiController]
    public class LugaresServicioController : ControllerBase
    {
        private ApplicationDbContext _db;

        public LugaresServicioController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<LugarServicioViewModel>> Post([FromForm] BuscarLugaresServicio filtros)
        {
            IQueryable<LugarServicio> lugaresQuery = _db.LugaresServicios;

            if (!string.IsNullOrEmpty(filtros.Servicio))
                lugaresQuery = lugaresQuery.Where(t => t.Servicios.Contains(filtros.Servicio));

            if (!string.IsNullOrEmpty(filtros.Nombre))
                lugaresQuery = lugaresQuery.Where(t => t.Nombre.Contains(filtros.Nombre));

            var lugares = await lugaresQuery.ToListAsync();

            return lugares.Select(t => new LugarServicioViewModel
            {
                Lat = t.Lat.GetValueOrDefault().ToString().Replace(",", "."),
                Lng = t.Lng.GetValueOrDefault().ToString().Replace(",", "."),
                Nombre = t.Nombre,
                Direccion = t.Direccion,
                Id = t.Id,
                Correo = t.Correo,
                Telefono = t.Telefono
            });
        }

    }
}