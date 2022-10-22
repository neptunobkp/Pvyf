using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.ViewModels;

namespace Web.Pages.Publico
{
    public class CompraModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public CompraModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty] public VehiculoVendenosViewModel Vm { get; set; }

        public SelectList Marcas { get; set; }
      
        public void OnGet()
        {
            Vm = new VehiculoVendenosViewModel();
            Marcas = new SelectList(_db.Marcas.OrderBy(t => t.Nombre).ToList(), "Id", "Nombre");
        }
    }
}