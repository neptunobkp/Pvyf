using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.ViewModels;

namespace Web.Areas.Tasaciones.Controllers
{
    [Area("Tasaciones")]
    [Route("[area]/api/[controller]")]
    [ApiController]
    public class ContactosController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ContactosController(ApplicationDbContext db)
        {
            _db = db;
        }


        [HttpPost("{id}/Contactado")]
        public async Task<ActionResult> Contactado(int id, [FromForm] ObservacionViewModel item)
        {
            var intencionVenta = await _db.IntencionesVendenos.FindAsync(id);
            intencionVenta.Contactado = !intencionVenta.Contactado;
            intencionVenta.ObservacionContacto = item.Obs;
            await _db.SaveChangesAsync();
            return Ok(true);
        }
    }
}