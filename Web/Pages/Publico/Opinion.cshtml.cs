using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Infraestructura;
using Web.Infraestructura.PaginasBase;
using Web.Model;
using Web.ViewModels;

namespace Web
{
    public class OpinionModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly ProteccionData _protector;

        public OpinionModel(ApplicationDbContext db, ProteccionData protector)
        {
            _db = db;
            _protector = protector;
        }

        public Opinion opinion { get; set; }

        public async Task OnGet(string id)
        {
            var decodedId = System.Convert.ToInt32(_protector.Encode(id));
            this.opinion = await _db.Opiniones.SingleAsync(t => t.Id == decodedId);
        }
    }
}