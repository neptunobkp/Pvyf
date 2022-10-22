using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Model;
using Web.ViewModels;

namespace Web.Areas.Usuarios.Controllers
{
    [Area("Usuarios")]
    [Route("[area]/api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public UsuariosController(ApplicationDbContext db)
        {
            _db = db;

        }

        [HttpGet]
        public async Task<IEnumerable<UsuarioViewModel>> Get()
        {
            List<UsuarioViewModel> result = new List<UsuarioViewModel>();
            var usuarios = await _db.Usuarios.AsNoTracking().ToListAsync();

            foreach (var cadaUsuario in usuarios)
            {
                result.Add(new UsuarioViewModel
                {
                    Id = cadaUsuario.Id,
                    Nombre = cadaUsuario.Nombre,
                    Correo = cadaUsuario.Correo,
                    EsAdministrador = cadaUsuario.EsAdministrador
                });
            }


            return result;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> PutItem([FromBody] UsuarioViewModel item)
        {
            var usuario = await _db.Usuarios.FindAsync(item.Id);
            usuario.EsAdministrador = item.EsAdministrador;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> PostItem([FromBody] UsuarioViewModel item)
        {
            var usuario = new Usuario
            {
                Nombre = item.Nombre,
                Correo = item.Correo,
                Contrasena = item.Contrasena,
                EsAdministrador = item.EsAdministrador
            };
            _db.Usuarios.Add(usuario);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}