using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Data;
using Web.Infraestructura.PaginasBase;
using Web.Model;

namespace Web.Pages.Publico
{
    public class TasacionNoDisponibleModel : PaginaConFormularioContacto
    {
        public TasacionNoDisponibleModel(IHostingEnvironment hostingEnvironment, ApplicationDbContext db) : base(hostingEnvironment, db)
        {
        }

        [BindProperty]
        public SolicitudContacto SolicitudVm { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            await GuardarContacto(SolicitudVm);
            return RedirectToPage("GraciasPorContacto");
        }
    }
}