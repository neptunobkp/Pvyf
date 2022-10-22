using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Data;

namespace Web.Areas.Dims.Controllers
{
    [Area("Dims")]
    [Route("[area]/api/[controller]")]
    [ApiController]
    public class CiudadesController : ControllerBase
    {
        [HttpGet] public IEnumerable<string> Get([FromServices] ApplicationDbContext db) => db.Comunas.Select(t => t.Ciudad).Distinct().ToList();
    }
}