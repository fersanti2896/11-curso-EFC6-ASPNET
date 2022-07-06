using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeliculasWebAPI.Migrations {
    public partial class TotalCalculado : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<int>(
                name        : "Cantidad",
                table       : "FacturaDetalles",
                type        : "int",
                nullable    : false,
                defaultValue: 0
            );

            migrationBuilder.AddColumn<decimal>(
                name             : "Total",
                table            : "FacturaDetalles",
                type             : "decimal(18,2)",
                precision        : 18,
                scale            : 2,
                nullable         : false,
                computedColumnSql: "Precio * Cantidad"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropColumn(
                name : "Total",
                table: "FacturaDetalles"
            );

            migrationBuilder.DropColumn(
                name : "Cantidad",
                table: "FacturaDetalles"
            );

            migrationBuilder.CreateTable(
                name   : "PeliculasConteos",
                columns: table => new {
                    Id              = table.Column<int>(type: "int", nullable: false)
                                           .Annotation("SqlServer:Identity", "1, 1"),
                    CantidadActores = table.Column<int>(type: "int", nullable: false),
                    CantidadCines   = table.Column<int>(type: "int", nullable: false),
                    CantidadGeneros = table.Column<int>(type: "int", nullable: false),
                    Titulo          = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_PeliculasConteos", x => x.Id);
                }
            );
        }
    }
}
