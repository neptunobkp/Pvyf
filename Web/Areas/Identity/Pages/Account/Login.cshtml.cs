using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.ViewModels;

namespace Web.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public LoginModel(ApplicationDbContext db)
        {
            _db = db;
        }


        public async void OnGet(string returnUrl)
        {
        }

        [BindProperty] // Bind on Post
        public LogInViewModel LogInModel { get; set; }
        public async Task<IActionResult> OnPostAsync(string returnUrl)
        {
            if (!ModelState.IsValid) return Page();

            var usuario = await _db.Usuarios.AsNoTracking().SingleOrDefaultAsync(t => t.Correo == LogInModel.Username);

            if (usuario == null)
            {
                ModelState.AddModelError("", "El usuario no existe");
                return Page();
            }

            if (usuario.Contrasena != LogInModel.Password)
            {
                ModelState.AddModelError("", "La contrasena no es válida");
                return Page();
            }


            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Correo));
            identity.AddClaim(new Claim(ClaimTypes.Name, usuario.Nombre));
            identity.AddClaim(new Claim("usuarioid", usuario.Id.ToString()));

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = true });


            return RedirectToPage("../Administracion/Index");
        }
    }
}
