using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Helpers;
using Web.Model;
using Web.Model.CompraVende;
using Web.Model.Nuevos;
using Web.ViewModels;

namespace Web.Pages.Publico
{
    public class PublicarNuevoModel : PageModel
    {
        private ApplicationDbContext _db;

        public PublicarNuevoModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public SelectList Versiones { get; set; }

        public SelectList TiposVehiculo { get; set; }
        public SelectList Marcas { get; set; }
        [BindProperty] public VehiculoOfertaViewModel Vm { get; set; }

        [BindProperty] public IFormFileCollection Archivos { get; set; }
        public void OnGet()
        {
            Vm = new VehiculoOfertaViewModel();
            TiposVehiculo = _db.ItemsLista.FromSql("select distinct TipoVehiculo Texto, TipoVehiculo Valor  from versiones").ToList().AsSelectList();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var publicacion = new PublicacionNuevo();
            publicacion.VersionId = Vm.VersionId.GetValueOrDefault();
            publicacion.Precio = Vm.Precio.GetValueOrDefault();
            publicacion.Observaciones = Vm.Observaciones;
            foreach (var formFile in Archivos)
            {
                MemoryStream ms = new MemoryStream();
                formFile.CopyTo(ms);

                var newArchivo = new Archivo
                {
                    ArchivoBytes = ms.ToArray(),
                    NombreArchivo = formFile.FileName,
                    Extension = formFile.ContentType
                };
                await _db.Archivos.AddAsync(newArchivo);
                await _db.SaveChangesAsync();
                publicacion.Fotos.Add(new FotoPublicacionNuevo()
                {
                    ArchivoId = newArchivo.Id
                });
            }

            await _db.PublicacionesNuevos.AddAsync(publicacion);
            await _db.SaveChangesAsync();
            TempData["MensajeSistema"] = "Oferta guardada";
            return RedirectToPage("Compra");
        }
      
        
    }
}