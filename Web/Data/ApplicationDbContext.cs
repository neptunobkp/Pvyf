using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web.Data.Configurations;
using Web.Model;
using Web.Model.CompraVende;
using Web.Model.Historico;
using Web.Model.Nuevos;
using Web.Model.Tipos;
using Web.Model.Vendenos;
using Web.ViewModels;
//using IntencionVendenos = Web.Data.Migrations.IntencionVendenos;

namespace Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<Versiona> Versiones { get; set; }
        public DbSet<Tasacion> Tasaciones { get; set; }
        public DbSet<Tasacion2019> Tasaciones2019 { get; set; }
        public DbSet<Comuna> Comunas { get; set; }
        public DbSet<Taller> Talleres { get; set; }

        public DbSet<LugarServicio> LugaresServicios { get; set; }
        public DbSet<Archivo> Archivos { get; set; }
        public DbSet<PublicacionUsado> PublicacionesUsados { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Model.Vendenos.IntencionVendenos> IntencionesVendenos { get; set; }
        public DbSet<IntencionInspeccion> IntencionesInspecciones { get; set; }
        public DbSet<InspeccionUsado> InspeccionUsados { get; set; }
        public DbSet<Region> Regiones { get; set; }
        public DbSet<TipoVehiculo> TiposVehiculo { get; set; }
        public DbQuery<ItemLista> ItemsLista { get; set; }
        public DbQuery<EntidadLista> EntidadesLista { get; set; }

        public DbSet<Busqueda> Busquedas { get; set; }
        public DbSet<TipoCombustible> TiposCombustible { get; set; }

        public DbSet<PublicacionNuevo> PublicacionesNuevos { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<FotoPublicacionNuevo> FotosNuevos { get; set; }
        public DbSet<SolicitudContacto> SolicitudesContacto { get; set; }
        public DbSet<Opinion> Opiniones { get; set; }
        public DbQuery<VehiculoPlano> VehiculosPlanos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MarcaConfiguration).Assembly);
        }
    }
}
