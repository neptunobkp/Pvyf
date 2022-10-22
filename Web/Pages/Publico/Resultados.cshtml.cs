using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Model;
using Web.Model.Vendenos;
using Web.ViewModels;
using Web.Helpers;
using Web.Infraestructura;

namespace Web.Pages.Publico
{
    public class ResultadosModel : PageModel
    {
        private readonly ProteccionData _protector;

        private ApplicationDbContext _db;
        public ResultadosModel(ApplicationDbContext db, ProteccionData protectorData)
        {
            _db = db;
            _protector = protectorData;
        }
        public FiltrosBusquedaNuevos Vm { get; set; }
        public SelectList Marcas { get; set; }

        public SelectList ListaTiposVehiculo { get; set; }
        public SelectList ListaCombustible { get; set; }
        public SelectList ListaMotorTfs { get; set; }
        public SelectList ListaHp { get; set; }
        public SelectList ListaTorqueNm { get; set; }
        public SelectList ListaVFinalKmH { get; set; }

        public SelectList ListaTraccion { get; set; }
        public SelectList ListaTransmision { get; set; }
        public SelectList ListaSensorEstacionamiento { get; set; }

        public SelectList ListaPaisFabricacion { get; set; }

        public int Id { get; set; }


        public async Task OnGetPorTipo(string tipoVehiculo)
        {
            var busqueda = new Busqueda
            {
                Fecha = DateTime.Now,
                TipoVehiculo = tipoVehiculo
            };

            _db.Busquedas.Add(busqueda);
            _db.SaveChanges();
            Id = busqueda.Id;

            await ConfigurarListas();
            Vm = new FiltrosBusquedaNuevos { FnTipoVehiculo = tipoVehiculo };
        }

        public async Task OnGetPorId(string id)
        {
            var decodedId = System.Convert.ToInt32(_protector.Encode(id));
            Id = decodedId;
            var busqueda = _db.Busquedas.Find(Id);
            Vm = new FiltrosBusquedaNuevos();

            if (!string.IsNullOrEmpty(busqueda.TipoVehiculo))
                Vm.FnTipoVehiculo = busqueda.TipoVehiculo;

            Vm.FnMarcaId = busqueda.MarcaId;
            Vm.FnModeloId = busqueda.ModeloId;
            Vm.Transmision = busqueda.TipoTransmision;
            Vm.Combustible = busqueda.TipoCombustible;
            Vm.CantidadAsientos = busqueda.CantidadAsientos;


            await ConfigurarListas();

            //ListaMotorTfs = _db.ItemsLista.FromSql("select distinct Combustible Valor, Combustible Texto from Versiones").ToList().AsSelectList();
            //ListaMotorTfs = _db.ItemsLista.FromSql("select distinct MotorTfs Valor, MotorTfs Texto from Versiones").ToList().AsSelectList();
            //ListaTorqueNm = _db.ItemsLista.FromSql("select distinct TorqueNm Valor, TorqueNm Texto from Versiones").ToList().AsSelectList();
            //ListaVFinalKmH = _db.ItemsLista.FromSql("select distinct VFinalKmH Valor, VFinalKmH Texto from Versiones").ToList().AsSelectList();
            //ListaTraccion = _db.ItemsLista.FromSql("select distinct Traccion Valor, Traccion Texto from Versiones").ToList().AsSelectList();
            //ListaPaisFabricacion = _db.ItemsLista.FromSql("select distinct PaisFabricacion Valor, PaisFabricacion Texto from Versiones").ToList().AsSelectList();
        }

        private async Task ConfigurarListas()
        {
            ListaTiposVehiculo = (await _db.ItemsLista.FromSql(
                    "select distinct vers.tipovehiculo Valor, vers.tipoVehiculo texto from Versiones vers where exists (select 1 from PublicacionesNuevos pnu where pnu.VersionId = vers.Id)")
                .ToListAsync()).AsSelectList();

            Marcas = new SelectList(_db.Marcas.OrderBy(t => t.Nombre).ToList(), "Id", "Nombre");

            ListaCombustible = _db.ItemsLista.FromSql("select distinct Combustible Valor, Combustible Texto from Versiones")
                .ToList().AsSelectList();
            ListaTransmision = _db.ItemsLista.FromSql("select distinct Transmision Valor, Transmision Texto from Versiones")
                .ToList().AsSelectList();
        }
    }
}