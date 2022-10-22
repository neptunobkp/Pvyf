using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Infraestructura;
using Web.Model;
using Web.ViewModels;

namespace Web.Areas.Opiniones.Controllers
{
    [Area("Opiniones")]
    [Route("[area]/api/[controller]")]
    [ApiController]
    public class PrimerosController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ProteccionData _protector;

        public PrimerosController(ApplicationDbContext db, ProteccionData protector)
        {
            _db = db;
            _protector = protector;
        }

        public async Task<IList<OpinionViewModel>> Get() => (await _db.Opiniones.OrderBy(t => t.Id).Take(3).ToListAsync()).Select(t => new OpinionViewModel
        {
            Id = t.Id,
            IdEnc = _protector.Decode(t.Id.ToString()),
            Titulo = t.Titulo,
            DetalleBreve = t.DetalleBreve,
            ArchivoImagen = t.UriArchivoFoto
        }).ToList();
    }
}