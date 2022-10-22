using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.ViewModels;

namespace Web.Areas.Mantenciones.Controllers
{
    [Area("Mantenciones")]
    [Route("[area]/api/[controller]")]
    [ApiController]
    public class ClavesLugarController : ControllerBase
    {
        private ApplicationDbContext _db;

        public ClavesLugarController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<ItemLista>> Get()
        {
            var servicios = await _db.ItemsLista
                .FromSql("select DISTINCT SERVICIOS AS Valor, '' as Texto from lugaresservicios").ToListAsync();

            var itemListaResult = new List<ItemLista>();
            foreach (var cadaGropoServicio in servicios)
            {
                foreach (var cadaServicio in cadaGropoServicio.Valor.Split(','))
                {
                    if (!itemListaResult.Any(t => t.Valor == cadaServicio))
                    {
                        var servicioDecoded = DecodeServicio(cadaServicio);
                        if (!string.IsNullOrEmpty(servicioDecoded))
                            itemListaResult.Add(new ItemLista
                            {
                                Valor = cadaServicio,
                                Texto = servicioDecoded
                            });
                    }
                }
            }

            return itemListaResult;
        }

        private string DecodeServicio(string cadaServicio)
        {
            switch (cadaServicio)
            {
                case "TALLER": return "Taller mecánico";
                case "CARWASH": return "Car wash";
                case "MANTENIMIENTO": return "Servicio de mantenimiento";
            }

            return string.Empty;
        }
    }
}