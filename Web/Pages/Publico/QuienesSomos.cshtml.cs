using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Infraestructura.PaginasBase;
using Web.Model;

namespace Web
{
    public class QuienesSomosModel : PaginaConFormularioContacto
    {

        public QuienesSomosModel(IHostingEnvironment hostingEnvironment, ApplicationDbContext db) : base(hostingEnvironment, db) { 
        }

        [BindProperty]
        public SolicitudContacto SolicitudVm { get; set; }

        public async Task<IActionResult> OnPost() {
            await GuardarContacto(SolicitudVm);
            return RedirectToPage("GraciasPorContacto");
        }
    }
}