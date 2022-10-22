using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Data.Migrations
{
    public partial class MigInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Archivos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArchivoBytes = table.Column<byte[]>(nullable: true),
                    NombreArchivo = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Archivos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Busquedas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    MarcaId = table.Column<int>(nullable: true),
                    ModeloId = table.Column<int>(nullable: true),
                    TipoVehiculo = table.Column<string>(nullable: true),
                    PrecioMin = table.Column<int>(nullable: true),
                    PrecioMax = table.Column<int>(nullable: true),
                    CantidadAsientos = table.Column<int>(nullable: true),
                    TipoCombustible = table.Column<string>(nullable: true),
                    TipoTransmision = table.Column<string>(nullable: true),
                    ConsumoPromedio = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Busquedas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comunas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Ciudad = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comunas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LugaresServicios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    Servicios = table.Column<string>(nullable: true),
                    Lat = table.Column<decimal>(type: "DECIMAL(12,9)", nullable: true),
                    Lng = table.Column<decimal>(type: "DECIMAL(12,9)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LugaresServicios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 160, nullable: false),
                    Eliminado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Opiniones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Titulo = table.Column<string>(nullable: true),
                    DetalleBreve = table.Column<string>(nullable: true),
                    TextoPrincipal = table.Column<string>(nullable: true),
                    UriArchivoFoto = table.Column<string>(nullable: true),
                    RangoPrecios = table.Column<string>(nullable: true),
                    Puntuacion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opiniones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regiones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regiones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudesContacto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombres = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    Celular = table.Column<string>(nullable: true),
                    Comentario = table.Column<string>(nullable: true),
                    Pagina = table.Column<string>(nullable: true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Contactado = table.Column<bool>(nullable: false),
                    ObservacionContacto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesContacto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasaciones2019",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<string>(nullable: true),
                    Marca = table.Column<string>(nullable: true),
                    Modelo = table.Column<string>(nullable: true),
                    ModeloVersion = table.Column<string>(nullable: true),
                    Tasa = table.Column<int>(nullable: false),
                    Anio = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasaciones2019", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposCombustible",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Eliminado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposCombustible", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposVehiculo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Eliminado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposVehiculo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    EsAdministrador = table.Column<bool>(nullable: false),
                    IdentificadorExterno = table.Column<string>(nullable: true),
                    Contrasena = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    Rut = table.Column<int>(nullable: true),
                    Deshabilitado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehiculosPlanos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Patente = table.Column<string>(nullable: true),
                    Marca = table.Column<string>(nullable: true),
                    Modelo = table.Column<string>(nullable: true),
                    TipoVehiculo = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Anio = table.Column<string>(nullable: true),
                    Tasacion = table.Column<string>(nullable: true),
                    NumeroChasis = table.Column<string>(nullable: true),
                    NumeroMotor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiculosPlanos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendedores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RazonSocial = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Talleres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ComunaId = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talleres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Talleres_Comunas_ComunaId",
                        column: x => x.ComunaId,
                        principalTable: "Comunas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Modelos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    MarcaId = table.Column<int>(nullable: false),
                    Eliminado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modelos_Marcas_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marcas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Versiones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Eliminado = table.Column<bool>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    ModeloId = table.Column<int>(nullable: false),
                    TipoVehiculo = table.Column<string>(nullable: true),
                    Combustible = table.Column<string>(nullable: true),
                    MotorTfs = table.Column<string>(nullable: true),
                    Hp = table.Column<int>(nullable: true),
                    TorqueNm = table.Column<string>(nullable: true),
                    VFinalKmH = table.Column<string>(nullable: true),
                    Traccion = table.Column<string>(nullable: true),
                    Transmision = table.Column<string>(nullable: true),
                    DiametroLlantas = table.Column<string>(nullable: true),
                    PaisFabricacion = table.Column<string>(nullable: true),
                    SensorEstacionamiento = table.Column<bool>(nullable: false),
                    RCiudadKmLtr = table.Column<decimal>(nullable: true),
                    RCarreteraKmLtr = table.Column<decimal>(nullable: true),
                    Airbags = table.Column<int>(nullable: true),
                    Asientos = table.Column<int>(nullable: false),
                    RMixto = table.Column<decimal>(nullable: true),
                    FrenosAsistidos = table.Column<bool>(nullable: false),
                    FarosLed = table.Column<bool>(nullable: false),
                    LlantasAleacion = table.Column<bool>(nullable: false),
                    FijacionAsientosNinos = table.Column<bool>(nullable: false),
                    AirbagPasajero = table.Column<bool>(nullable: false),
                    AirbagLaterales = table.Column<bool>(nullable: false),
                    Alarma = table.Column<bool>(nullable: false),
                    CierreCentralizado = table.Column<bool>(nullable: false),
                    CamaraRetroceso = table.Column<bool>(nullable: false),
                    RuedaRepuesto = table.Column<bool>(nullable: false),
                    ControlCrucero = table.Column<bool>(nullable: false),
                    Climatizador = table.Column<bool>(nullable: false),
                    AireAcondicionado = table.Column<bool>(nullable: false),
                    Bluetooth = table.Column<bool>(nullable: false),
                    StartStop = table.Column<bool>(nullable: false),
                    EspejosElectrico = table.Column<bool>(nullable: false),
                    Aleron = table.Column<bool>(nullable: false),
                    AperturaElectricaMaletero = table.Column<bool>(nullable: false),
                    Cilindrada = table.Column<int>(nullable: true),
                    Puertas = table.Column<int>(nullable: true),
                    Marchas = table.Column<int>(nullable: true),
                    EquipamientoDetallado = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versiones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Versiones_Modelos_ModeloId",
                        column: x => x.ModeloId,
                        principalTable: "Modelos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublicacionesNuevos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VersionId = table.Column<int>(nullable: false),
                    Precio = table.Column<int>(nullable: false),
                    Observaciones = table.Column<string>(nullable: true),
                    DescripcionCorta = table.Column<string>(nullable: true),
                    DescripcionLarga = table.Column<string>(nullable: true),
                    VendedorId = table.Column<int>(nullable: true),
                    FechaPublicacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicacionesNuevos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicacionesNuevos_Vendedores_VendedorId",
                        column: x => x.VendedorId,
                        principalTable: "Vendedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PublicacionesNuevos_Versiones_VersionId",
                        column: x => x.VersionId,
                        principalTable: "Versiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasaciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VersionId = table.Column<int>(nullable: false),
                    Monto = table.Column<int>(nullable: false),
                    Anio = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasaciones_Versiones_VersionId",
                        column: x => x.VersionId,
                        principalTable: "Versiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Patente = table.Column<string>(nullable: true),
                    Anio = table.Column<int>(nullable: true),
                    VersionId = table.Column<int>(nullable: true),
                    Kilometraje = table.Column<int>(nullable: true),
                    ColorInterior = table.Column<string>(nullable: true),
                    ColorExterior = table.Column<string>(nullable: true),
                    NumeroMotor = table.Column<string>(nullable: true),
                    NumeroChasis = table.Column<string>(nullable: true),
                    Estado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehiculos_Versiones_VersionId",
                        column: x => x.VersionId,
                        principalTable: "Versiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FotosNuevos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PublicacionNuevoId = table.Column<int>(nullable: false),
                    ArchivoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FotosNuevos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FotosNuevos_Archivos_ArchivoId",
                        column: x => x.ArchivoId,
                        principalTable: "Archivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FotosNuevos_PublicacionesNuevos_PublicacionNuevoId",
                        column: x => x.PublicacionNuevoId,
                        principalTable: "PublicacionesNuevos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IntencionesVendenos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    VehiculoId = table.Column<int>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    Apellido = table.Column<string>(nullable: true),
                    Kilometraje = table.Column<int>(nullable: true),
                    AnoTasacion = table.Column<int>(nullable: true),
                    TasacionId = table.Column<int>(nullable: true),
                    Valor = table.Column<int>(nullable: true),
                    Contactado = table.Column<bool>(nullable: false),
                    ObservacionContacto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntencionesVendenos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntencionesVendenos_Tasaciones_TasacionId",
                        column: x => x.TasacionId,
                        principalTable: "Tasaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IntencionesVendenos_Vehiculos_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PublicacionesUsados",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    VehiculoId = table.Column<int>(nullable: true),
                    CantidadDuenos = table.Column<int>(nullable: true),
                    PrecioVehiculo = table.Column<int>(nullable: true),
                    Rut = table.Column<int>(nullable: true),
                    Nombres = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    Observaciones = table.Column<string>(nullable: true),
                    OfertanteId = table.Column<int>(nullable: true),
                    RegionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicacionesUsados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicacionesUsados_Usuarios_OfertanteId",
                        column: x => x.OfertanteId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PublicacionesUsados_Regiones_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PublicacionesUsados_Vehiculos_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IntencionesInspecciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaCandidata = table.Column<DateTime>(nullable: false),
                    Direccion = table.Column<string>(nullable: true),
                    IntencionVendenosId = table.Column<int>(nullable: false),
                    ComunaId = table.Column<int>(nullable: true),
                    TallerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntencionesInspecciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntencionesInspecciones_IntencionesVendenos_IntencionVendenosId",
                        column: x => x.IntencionVendenosId,
                        principalTable: "IntencionesVendenos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IntencionesInspecciones_Talleres_TallerId",
                        column: x => x.TallerId,
                        principalTable: "Talleres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FotoPublicacionUsado",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PublicacionUsadoId = table.Column<int>(nullable: false),
                    ArchivoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FotoPublicacionUsado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FotoPublicacionUsado_Archivos_ArchivoId",
                        column: x => x.ArchivoId,
                        principalTable: "Archivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FotoPublicacionUsado_PublicacionesUsados_PublicacionUsadoId",
                        column: x => x.PublicacionUsadoId,
                        principalTable: "PublicacionesUsados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FotoPublicacionUsado_ArchivoId",
                table: "FotoPublicacionUsado",
                column: "ArchivoId");

            migrationBuilder.CreateIndex(
                name: "IX_FotoPublicacionUsado_PublicacionUsadoId",
                table: "FotoPublicacionUsado",
                column: "PublicacionUsadoId");

            migrationBuilder.CreateIndex(
                name: "IX_FotosNuevos_ArchivoId",
                table: "FotosNuevos",
                column: "ArchivoId");

            migrationBuilder.CreateIndex(
                name: "IX_FotosNuevos_PublicacionNuevoId",
                table: "FotosNuevos",
                column: "PublicacionNuevoId");

            migrationBuilder.CreateIndex(
                name: "IX_IntencionesInspecciones_IntencionVendenosId",
                table: "IntencionesInspecciones",
                column: "IntencionVendenosId");

            migrationBuilder.CreateIndex(
                name: "IX_IntencionesInspecciones_TallerId",
                table: "IntencionesInspecciones",
                column: "TallerId");

            migrationBuilder.CreateIndex(
                name: "IX_IntencionesVendenos_TasacionId",
                table: "IntencionesVendenos",
                column: "TasacionId");

            migrationBuilder.CreateIndex(
                name: "IX_IntencionesVendenos_VehiculoId",
                table: "IntencionesVendenos",
                column: "VehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Modelos_MarcaId",
                table: "Modelos",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicacionesNuevos_VendedorId",
                table: "PublicacionesNuevos",
                column: "VendedorId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicacionesNuevos_VersionId",
                table: "PublicacionesNuevos",
                column: "VersionId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicacionesUsados_OfertanteId",
                table: "PublicacionesUsados",
                column: "OfertanteId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicacionesUsados_RegionId",
                table: "PublicacionesUsados",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicacionesUsados_VehiculoId",
                table: "PublicacionesUsados",
                column: "VehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Talleres_ComunaId",
                table: "Talleres",
                column: "ComunaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasaciones_VersionId",
                table: "Tasaciones",
                column: "VersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_VersionId",
                table: "Vehiculos",
                column: "VersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Versiones_ModeloId",
                table: "Versiones",
                column: "ModeloId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Busquedas");

            migrationBuilder.DropTable(
                name: "FotoPublicacionUsado");

            migrationBuilder.DropTable(
                name: "FotosNuevos");

            migrationBuilder.DropTable(
                name: "IntencionesInspecciones");

            migrationBuilder.DropTable(
                name: "LugaresServicios");

            migrationBuilder.DropTable(
                name: "Opiniones");

            migrationBuilder.DropTable(
                name: "SolicitudesContacto");

            migrationBuilder.DropTable(
                name: "Tasaciones2019");

            migrationBuilder.DropTable(
                name: "TiposCombustible");

            migrationBuilder.DropTable(
                name: "TiposVehiculo");

            migrationBuilder.DropTable(
                name: "VehiculosPlanos");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "PublicacionesUsados");

            migrationBuilder.DropTable(
                name: "Archivos");

            migrationBuilder.DropTable(
                name: "PublicacionesNuevos");

            migrationBuilder.DropTable(
                name: "IntencionesVendenos");

            migrationBuilder.DropTable(
                name: "Talleres");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Regiones");

            migrationBuilder.DropTable(
                name: "Vendedores");

            migrationBuilder.DropTable(
                name: "Tasaciones");

            migrationBuilder.DropTable(
                name: "Vehiculos");

            migrationBuilder.DropTable(
                name: "Comunas");

            migrationBuilder.DropTable(
                name: "Versiones");

            migrationBuilder.DropTable(
                name: "Modelos");

            migrationBuilder.DropTable(
                name: "Marcas");
        }
    }
}
