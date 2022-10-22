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

namespace Web
{
    public class ContactoModel : PaginaConFormularioContacto
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public SolicitudContacto Vm { get; set; }

        public ContactoModel(IHostingEnvironment hostingEnvironment, ApplicationDbContext db) : base(hostingEnvironment, db)
        {
        }

        public void OnGet()
        {
            Vm = new SolicitudContacto { Pagina = "/Contacto" };
        }

        public async Task<IActionResult> OnPost()
        {
            await GuardarContacto(Vm);
            return RedirectToPage("GraciasPorContacto");
        }


    }
}