using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Infraestructura;
using Web.Model.Vendenos;

namespace Web.Pages.Publico
{
    public class GraciasModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly ProteccionData _protector;
        public GraciasModel(ApplicationDbContext db, ProteccionData protectorData)
        {
            _db = db;
            _protector = protectorData;
        }
        public string Descripcion { get; set; }
        public IntencionInspeccion IntencionInspeccion { get; set; }
        public async Task OnGet(string id)
        {
            var decodedId = System.Convert.ToInt32(_protector.Encode(id));
            IntencionInspeccion = await _db.IntencionesInspecciones
                .Include(t => t.IntencionVendenos)
                    .ThenInclude(t => t.Vehiculo)
                .SingleAsync(t => t.Id == decodedId);

            if (IntencionInspeccion.IntencionVendenos.Vehiculo.VersionId.HasValue)
            {
                var vehiculo = await _db.Vehiculos.Include(t => t.Version.Modelo.Marca).SingleAsync(t => t.Id == IntencionInspeccion.IntencionVendenos.VehiculoId);
                Descripcion = $" {vehiculo.Anio} - {vehiculo.Version?.Modelo?.Marca?.Nombre} { vehiculo.Version?.Modelo?.Nombre} ";
            }
            else
            {
                var stringPatente = IntencionInspeccion.IntencionVendenos.Vehiculo.Patente.ToUpper();
                var vehiculoPlano = _db.VehiculosPlanos.SingleOrDefault(t => t.Patente == stringPatente);
                Descripcion = $"{vehiculoPlano.Anio} - {vehiculoPlano.Marca} {vehiculoPlano.Modelo} ";
            }
        }
    }
}