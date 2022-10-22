using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Model;

namespace Web.Areas.Talleres.Controllers
{
    [Area("Talleres")]
    [Route("[area]/api/[controller]")]
    [ApiController]
    public class TalleresController : ControllerBase
    {
        [HttpGet("{comunaId}")]
        public async Task<IEnumerable<Taller>> Get([FromServices] ApplicationDbContext db, int comunaId) =>
          await db.Talleres.Where(t => t.ComunaId == comunaId).Select(t => new Taller { Id = t.Id, Nombre = t.Nombre }).ToListAsync();
    }
}