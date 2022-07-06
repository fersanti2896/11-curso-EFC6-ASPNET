using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeliculasWebAPI.Migrations {
    public partial class ProductoHerTablaTipo : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name   : "Productos",
                columns: table => new {
                    Id     = table.Column<int>(type: "int", nullable: false)
                                    .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Mercancia",
                columns: table => new {
                    Id                   = table.Column<int>(type: "int", nullable: false),
                    DisponibleInventario = table.Column<bool>(type: "bit", nullable: false),
                    Peso                 = table.Column<double>(type: "float", nullable: false),
                    Volumen              = table.Column<double>(type: "float", nullable: false),
                    EsRopa               = table.Column<bool>(type: "bit", nullable: false),
                    EsColeccionable      = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Mercancia", x => x.Id);
                    table.ForeignKey(
                        name           : "FK_Mercancia_Productos_Id",
                        column         : x => x.Id,
                        principalTable : "Productos",
                        principalColumn: "Id");
                }
            );

            migrationBuilder.CreateTable(
                name: "PeliculasAlquilables",
                columns: table => new {
                    Id         = table.Column<int>(type: "int", nullable: false),
                    PeliculaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_PeliculasAlquilables", x => x.Id);
                    table.ForeignKey(
                        name           : "FK_PeliculasAlquilables_Productos_Id",
                        column         : x => x.Id,
                        principalTable : "Productos",
                        principalColumn: "Id");
                }
            );

            migrationBuilder.UpdateData(
                table    : "CinesOfertas",
                keyColumn: "Id",
                keyValue : 1,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2022, 6, 2, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 5, 26, 0, 0, 0, 0, DateTimeKind.Local) }
            );

            migrationBuilder.UpdateData(
                table    : "CinesOfertas",
                keyColumn: "Id",
                keyValue : 2,
                columns  : new[] { "FechaFin", "FechaInicio" },
                values   : new object[] { new DateTime(2022, 5, 31, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 5, 26, 0, 0, 0, 0, DateTimeKind.Local) }
            );

            migrationBuilder.UpdateData(
                table    : "Pagos",
                keyColumn: "Id",
                keyValue : 1,
                column   : "TipoPago",
                value    : 2
            );

            migrationBuilder.UpdateData(
                table    : "Pagos",
                keyColumn: "Id",
                keyValue : 2,
                column   : "TipoPago",
                value    : 2
            );

            migrationBuilder.UpdateData(
                table    : "Peliculas",
                keyColumn: "Id",
                keyValue : 5,
                column   : "FechaEstreno",
                value    : new DateTime(2022, 5, 26, 0, 0, 0, 0, DateTimeKind.Local)
            );

            migrationBuilder.InsertData(
                table    : "Productos",
                columns  : new[] { "Id", "Nombre", "Precio" },
                values   : new object[,] {
                    { 2, "Taza coleccionable", 11m },
                    { 1, "Dr Strange", 5.99m }
                }
            );

            migrationBuilder.InsertData(
                table    : "Mercancia",
                columns  : new[] { "Id", "DisponibleInventario", "EsColeccionable", "EsRopa", "Peso", "Volumen" },
                values   : new object[] { 2, true, false, true, 1.0, 1.0 }
            );

            migrationBuilder.InsertData(
                table    : "PeliculasAlquilables",
                columns  : new[] { "Id", "PeliculaId" },
                values   : new object[] { 1, 6 }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "Mercancia"
            );

            migrationBuilder.DropTable(
                name: "PeliculasAlquilables"
            );

            migrationBuilder.DropTable(
                name: "Productos"
            );

            migrationBuilder.DeleteData(
                table    : "Pagos",
                keyColumn: "Id",
                keyValue : 1
            );

            migrationBuilder.DeleteData(
                table    : "Pagos",
                keyColumn: "Id",
                keyValue : 2
            );

            migrationBuilder.UpdateData(
                table    : "CinesOfertas",
                keyColumn: "Id",
                keyValue : 1,
                columns  : new[] { "FechaFin", "FechaInicio" },
                values   : new object[] { new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 5, 25, 0, 0, 0, 0, DateTimeKind.Local) }
            );

            migrationBuilder.UpdateData(
                table    : "CinesOfertas",
                keyColumn: "Id",
                keyValue : 2,
                columns  : new[] { "FechaFin", "FechaInicio" },
                values   : new object[] { new DateTime(2022, 5, 30, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 5, 25, 0, 0, 0, 0, DateTimeKind.Local) }
            );

            migrationBuilder.InsertData(
                table  : "Pagos",
                columns: new[] { "Id", "FechaTransaccion", "FourDigits", "Monto", "TipoPago" },
                values : new object[,] {
                    { 1, new DateTime(2022, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "5678", 9.99m, 1 },
                    { 2, new DateTime(2022, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "1234", 7.99m, 1 }
                }
            );

            migrationBuilder.UpdateData(
                table    : "Peliculas",
                keyColumn: "Id",
                keyValue : 5,
                column   : "FechaEstreno",
                value    : new DateTime(2022, 5, 25, 0, 0, 0, 0, DateTimeKind.Local)
            );
        }
    }
}
