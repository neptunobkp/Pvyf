using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Model;

namespace Web.Areas.Dims.Controllers
{
    [Area("Dims")]
    [Route("[area]/api/[controller]")]
    [ApiController]
    public class ComunasController : ControllerBase
    {
        [HttpGet("{ciudad}")]
        public IEnumerable<Comuna> Get([FromServices] ApplicationDbContext db, string ciudad) =>
            db.Comunas.Where(t => t.Ciudad == ciudad).Select(t => new Comuna { Id = t.Id, Nombre = t.Nombre }).ToList();
    }
}