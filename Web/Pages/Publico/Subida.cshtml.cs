using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Data;
using Web.Helpers;
using Web.Helpers.CargaRnvm;
using Web.Helpers.CargaSii;

namespace Web.Pages.Publico
{
    public class SubidaModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public SubidaModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            //var cargador = new CargaSiiHelper();
            var cargador = new CargaRnvmHelper();
            cargador.Procesar();
        }
    }

}