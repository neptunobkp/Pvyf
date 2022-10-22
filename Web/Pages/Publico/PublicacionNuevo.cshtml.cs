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

namespace Web.Pages.Publico
{
    public class PublicacionNuevoModel : PaginaConFormularioContacto
    {
        private readonly ProteccionData _protector;
        public PublicacionNuevoModel(IHostingEnvironment hostingEnvironment, ApplicationDbContext db, ProteccionData protector) : base(hostingEnvironment, db)
        {
            _protector = protector;
        }

        public VehiculoViewModel Vm { get; set; }

        [BindProperty]
        public SolicitudContacto SolicitudVm { get; set; }

        public async Task OnGet(string id)
        {
            var decodedId = System.Convert.ToInt32(_protector.Encode(id));
            SolicitudVm = new SolicitudContacto { Pagina = "/PublicacionNuevo" };
            var publicacionNuevo = await _db.PublicacionesNuevos
                .Include(t => t.Version.Modelo.Marca)
                .Include(t => t.Fotos)
                .SingleAsync(t => t.Id == decodedId);

            Vm = new VehiculoViewModel
            {
                Id = publicacionNuevo.Id,
                Precio = publicacionNuevo.Precio.ToString(),
                FechaPublicacion = publicacionNuevo.FechaPublicacion.ToShortDateString(),
                Anio = DateTime.Now.Year.ToString(),
                Marca = publicacionNuevo.Version.Modelo.Marca.Nombre,
                Modelo = publicacionNuevo.Version.Modelo.Nombre,
                DescripcionCorta = publicacionNuevo.DescripcionCorta,
                DescripcionLarga = publicacionNuevo.DescripcionLarga
            };

            Vm.AtributosLeft.Add($"Transmisión {publicacionNuevo.Version.Transmision}");
            Vm.AtributosLeft.Add($"Tracción {publicacionNuevo.Version.Traccion}");

            Vm.AtributosRight.Add($"Tamaño del motor {publicacionNuevo.Version.Cilindrada} CC");
            Vm.AtributosRight.Add($"{publicacionNuevo.Version.Puertas} puertas");

            Vm.DetallesTecnicos.Add(new ItemLista { Valor = "Abs", Texto = publicacionNuevo.Version.FrenosAsistidos ? "si" : "no" });
            Vm.DetallesTecnicos.Add(new ItemLista { Valor = "Tracción", Texto = publicacionNuevo.Version.Traccion });

            int indice = 1;
            foreach (var cadaFoto in publicacionNuevo.Fotos)
            {
                var image = _db.Archivos.Find(cadaFoto.ArchivoId);
                Vm.Fotos.Add(new FotoViewModel()
                {
                    Indice = indice++,
                    Data = $"data:{image.Extension};base64," + Convert.ToBase64String(image.ArchivoBytes, 0, image.ArchivoBytes.Length),
                    Nombre = "fotox"
                });
            }
        }

        public async Task<IActionResult> OnPost()
        {
            await GuardarContacto(SolicitudVm);
            return RedirectToPage("GraciasPorContacto");
        }
    }
}