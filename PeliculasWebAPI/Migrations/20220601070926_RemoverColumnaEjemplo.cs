using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeliculasWebAPI.Migrations {
    public partial class RemoverColumnaEjemplo : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropColumn(
                name : "Ejemplo",
                table: "Generos"
            );

            migrationBuilder.UpdateData(
                table    : "CinesOfertas",
                keyColumn: "Id",
                keyValue : 1,
                columns  : new[] { "FechaFin", "FechaInicio" },
                values   : new object[] { new DateTime(2022, 6, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Local) }
            );

            migrationBuilder.UpdateData(
                table    : "CinesOfertas",
                keyColumn: "Id",
                keyValue : 2,
                columns  : new[] { "FechaFin", "FechaInicio" },
                values   : new object[] { new DateTime(2022, 6, 6, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Local) }
            );

            migrationBuilder.UpdateData(
                table    : "Peliculas",
                keyColumn: "Id",
                keyValue : 5,
                column   :   "FechaEstreno",
                value    : new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Local)
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<string>(
                name    : "Ejemplo",
                table   : "Generos",
                type    : "nvarchar(max)",
                nullable: true
            );

            migrationBuilder.UpdateData(
                table    : "CinesOfertas",
                keyColumn: "Id",
                keyValue : 1,
                columns  : new[] { "FechaFin", "FechaInicio" },
                values   : new object[] { new DateTime(2022, 6, 6, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 5, 30, 0, 0, 0, 0, DateTimeKind.Local) }
            );

            migrationBuilder.UpdateData(
                table    : "CinesOfertas",
                keyColumn: "Id",
                keyValue : 2,
                columns  : new[] { "FechaFin", "FechaInicio" },
                values   : new object[] { new DateTime(2022, 6, 4, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 5, 30, 0, 0, 0, 0, DateTimeKind.Local) }
            );

            migrationBuilder.UpdateData(
                table    : "Peliculas",
                keyColumn: "Id",
                keyValue : 5,
                column   : "FechaEstreno",
                value    : new DateTime(2022, 5, 30, 0, 0, 0, 0, DateTimeKind.Local)
            );
        }
    }
}
