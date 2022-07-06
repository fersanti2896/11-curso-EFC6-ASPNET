using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeliculasWebAPI.Migrations {
    public partial class NoBorrarCineWithSalaCine : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropForeignKey(
                name : "FK_SalasCines_Cines_CineId",
                table: "SalasCines"
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

            migrationBuilder.UpdateData(
                table    : "Peliculas",
                keyColumn: "Id",
                keyValue : 5,
                column   : "FechaEstreno",
                value    : new DateTime(2022, 5, 25, 0, 0, 0, 0, DateTimeKind.Local)
            );

            migrationBuilder.AddForeignKey(
                name           : "FK_SalasCines_Cines_CineId",
                table          : "SalasCines",
                column         : "CineId",
                principalTable : "Cines",
                principalColumn: "Id",
                onDelete       : ReferentialAction.Restrict
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropForeignKey(
                name : "FK_SalasCines_Cines_CineId",
                table: "SalasCines"
            );

            migrationBuilder.UpdateData(
                table    : "CinesOfertas",
                keyColumn: "Id",
                keyValue : 1,
                columns  : new[] { "FechaFin", "FechaInicio" },
                values   : new object[] { new DateTime(2022, 5, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 5, 12, 0, 0, 0, 0, DateTimeKind.Local) }
            );

            migrationBuilder.UpdateData(
                table    : "CinesOfertas",
                keyColumn: "Id",
                keyValue : 2,
                columns  : new[] { "FechaFin", "FechaInicio" },
                values   : new object[] { new DateTime(2022, 5, 17, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 5, 12, 0, 0, 0, 0, DateTimeKind.Local) }
            );

            migrationBuilder.UpdateData(
                table    : "Peliculas",
                keyColumn: "Id",
                keyValue : 5,
                column   : "FechaEstreno",
                value    : new DateTime(2022, 5, 12, 0, 0, 0, 0, DateTimeKind.Local)
            );

            migrationBuilder.AddForeignKey(
                name           : "FK_SalasCines_Cines_CineId",
                table          : "SalasCines",
                column         : "CineId",
                principalTable : "Cines",
                principalColumn: "Id",
                onDelete       : ReferentialAction.Cascade
            );
        }
    }
}
