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
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Web.Data;
using Web.Helpers;
using Web.Model;
using Web.Model.CompraVende;
using Web.ViewModels;

namespace Web.Pages.Publico
{
    public class OfertaModel : PageModel
    {
        private ApplicationDbContext _db;

        public OfertaModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public SelectList Versiones { get; set; }
        public SelectList TiposVehiculo { get; set; }
        public SelectList Marcas { get; set; }
        public SelectList Regiones { get; set; }
        public SelectList Comunas { get; set; }

        [BindProperty] public VehiculoOfertaViewModel Vm { get; set; }

        [BindProperty] public IFormFileCollection Archivos { get; set; }
        public void OnGet()
        {
            Vm = new VehiculoOfertaViewModel();
            Marcas = new SelectList(_db.Marcas.OrderBy(t => t.Nombre).ToList(), "Id", "Nombre");
            TiposVehiculo = _db.ItemsLista.FromSql("select distinct TipoVehiculo Texto, TipoVehiculo Valor  from versiones").ToList().AsSelectList();
            Regiones = new SelectList(_db.Regiones.OrderBy(t => t.Nombre).ToList(), "Id", "Nombre");
            Comunas = new SelectList(_db.Comunas.OrderBy(t => t.Nombre).ToList(), "Id", "Nombre");
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var vehiculoId = ConfigurarVehiculo();
            var usuario = ConfigurarUsuario();
            var nuevaOferta = await ConfigurarOferta(vehiculoId, usuario);


            var nuevaOfertaAdded = _db.PublicacionesUsados.Add(nuevaOferta);
            var inspeccion = ConfigurarInspeccion(nuevaOfertaAdded);
            await _db.InspeccionUsados.AddAsync(inspeccion);
            await _db.SaveChangesAsync();

            TempData["MensajeSistema"] = "Oferta guardada";
            return RedirectToPage("Compra");
        }

        private InspeccionUsado ConfigurarInspeccion(EntityEntry<PublicacionUsado> nuevaOfertaAdded)
        {
            return new InspeccionUsado
            {
                PublicacionUsadoId = nuevaOfertaAdded.Entity.Id,
                ComunaId = Vm.ComunaId,
                Direccion = Vm.DireccionInspeccion,
                FechaCandidata = Vm.FechaInspeccion.GetValueOrDefault(DateTime.Now),
            };
        }

        private InspeccionUsado ConfigurarInspeccion(int publicacionUsadoId)
        {
            var inspeccion = new InspeccionUsado
            {
                Direccion = Vm.DireccionInspeccion,
                FechaCandidata = Vm.FechaInspeccion.Value,
                ComunaId = Vm.ComunaId,
                PublicacionUsadoId = publicacionUsadoId
            };

            return inspeccion;
        }

        private async Task<PublicacionUsado> ConfigurarOferta(int vehiculoId, Usuario usuario)
        {
            var nuevaOferta = new PublicacionUsado
            {
                Fecha = DateTime.Now,
                VehiculoId = vehiculoId,
                Correo = Vm.Correo,
                PrecioVehiculo = Vm.Precio,
                Observaciones = Vm.Observaciones,
                Nombres = Vm.Nombre,
                Telefono = Vm.Telefono,
                OfertanteId = usuario.Id
            };

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
                nuevaOferta.Fotos.Add(new FotoPublicacionUsado
                {
                    ArchivoId = newArchivo.Id
                });
            }

            return nuevaOferta;
        }

        private int ConfigurarVehiculo()
        {
            if (_db.Vehiculos.Any(t => t.Patente == Vm.Patente))
            {
                var vehiculo = _db.Vehiculos.First(t => t.Patente == Vm.Patente);
                return vehiculo.Id;
            }
            else
            {
                var vehiculoAdded = _db.Vehiculos.Add(new Vehiculo
                {
                    VersionId = Vm.VersionId,
                    Anio = Vm.Anio,
                    Kilometraje = Vm.Kilometraje,
                    Patente = Vm.Patente
                });
                _db.SaveChanges();
                return vehiculoAdded.Entity.Id;
            }

        }

        private Usuario ConfigurarUsuario()
        {
            var usuario = new Usuario();
            int? rutExtraido = ExtraerRut(Vm.Rut);
            if (_db.Usuarios.Any(t => t.Rut == rutExtraido))
            {
                usuario = _db.Usuarios.First(t => t.Rut == rutExtraido);
            }
            else
            {
                var usuarioAdded = _db.Usuarios.Add(new Usuario
                {
                    Nombre = Vm.Nombre,
                    Contrasena = Vm.Correo.Split('@').First(),
                    Correo = Vm.Correo,
                    Rut = rutExtraido
                });
                _db.SaveChanges();
                usuario.Id = usuarioAdded.Entity.Id;
            }

            return usuario;
        }

        private int? ExtraerRut(string vmRut)
        {
            if (string.IsNullOrEmpty(vmRut))
                return null;

            vmRut = vmRut.Replace(".", "");
            if (vmRut.Contains("-"))
                return Convert.ToInt32(vmRut.Split('-').First());

            return Convert.ToInt32(vmRut);
        }
    }
}