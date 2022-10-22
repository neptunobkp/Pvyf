using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Data.Migrations
{
    public partial class descuento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehiculosPlanos");

            migrationBuilder.AlterColumn<string>(
                name: "Nombres",
                table: "SolicitudesContacto",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Correo",
                table: "SolicitudesContacto",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comentario",
                table: "SolicitudesContacto",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PorcentajeDescuento",
                table: "IntencionesVendenos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PorcentajeDescuento",
                table: "IntencionesVendenos");

            migrationBuilder.AlterColumn<string>(
                name: "Nombres",
                table: "SolicitudesContacto",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Correo",
                table: "SolicitudesContacto",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Comentario",
                table: "SolicitudesContacto",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "VehiculosPlanos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Anio = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Marca = table.Column<string>(nullable: true),
                    Modelo = table.Column<string>(nullable: true),
                    NumeroChasis = table.Column<string>(nullable: true),
                    NumeroMotor = table.Column<string>(nullable: true),
                    Patente = table.Column<string>(nullable: true),
                    Tasacion = table.Column<string>(nullable: true),
                    TipoVehiculo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiculosPlanos", x => x.Id);
                });
        }
    }
}
