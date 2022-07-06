using System    ;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeliculasWebAPI.Migrations {
    public partial class PagoHerencia : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name   : "Pagos",
                columns: table => new {
                    Id               = table.Column<int>(type: "int", nullable: false)
                                            .Annotation("SqlServer:Identity", "1, 1"),
                    Monto            = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    FechaTransaccion = table.Column<DateTime>(type: "date", nullable: false),
                    TipoPago         = table.Column<int>(type: "int", nullable: false),
                    Email            = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    FourDigits       = table.Column<string>(type: "char(4)", nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Pagos", x => x.Id);
                }
            );

            migrationBuilder.InsertData(
                table  : "Pagos",
                columns: new[] { "Id", "Email", "FechaTransaccion", "Monto", "TipoPago" },
                values : new object[,] {
                    { 3, "prueba@prueba.com", new DateTime(2022, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 9.99m, 1 },
                    { 4, "prueba2@prueba.com", new DateTime(2022, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 7.99m, 1 }
                }
            );

            migrationBuilder.InsertData(
                table  : "Pagos",
                columns: new[] { "Id", "FechaTransaccion", "FourDigits", "Monto", "TipoPago" },
                values : new object[,] {
                    { 1, new DateTime(2022, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "5678", 9.99m, 1 },
                    { 2, new DateTime(2022, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "1234", 7.99m, 1 }
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "Pagos"
            );
        }
    }
}
