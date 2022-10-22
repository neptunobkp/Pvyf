using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Data.Migrations
{
    public partial class inspeccionusado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InspeccionUsados",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaCandidata = table.Column<DateTime>(nullable: false),
                    Direccion = table.Column<string>(nullable: true),
                    ComunaId = table.Column<int>(nullable: true),
                    PublicacionUsadoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspeccionUsados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspeccionUsados_PublicacionesUsados_PublicacionUsadoId",
                        column: x => x.PublicacionUsadoId,
                        principalTable: "PublicacionesUsados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InspeccionUsados_PublicacionUsadoId",
                table: "InspeccionUsados",
                column: "PublicacionUsadoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InspeccionUsados");
        }
    }
}
