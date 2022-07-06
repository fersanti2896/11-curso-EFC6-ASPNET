using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeliculasWebAPI.Migrations {
    public partial class EjemploConversion : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AlterColumn<string>(
                name           : "TipoSalaCine",
                table          : "SalasCines",
                type           : "nvarchar(max)",
                nullable       : false,
                defaultValue   : "DosD",
                oldClrType     : typeof(int),
                oldType        : "int",
                oldDefaultValue: 1
            );

            migrationBuilder.UpdateData(
                table    : "SalasCines",
                keyColumn: "Id",
                keyValue : 1,
                column   : "TipoSalaCine",
                value    : "DosD"
            );

            migrationBuilder.UpdateData(
                table    : "SalasCines",
                keyColumn: "Id",
                keyValue : 2,
                column   : "TipoSalaCine",
                value    : "TresD"
            );

            migrationBuilder.UpdateData(
                table    : "SalasCines",
                keyColumn: "Id",
                keyValue : 3,
                column   : "TipoSalaCine",
                value    : "DosD"
            );

            migrationBuilder.UpdateData(
                table    : "SalasCines",
                keyColumn: "Id",
                keyValue : 4,
                column   : "TipoSalaCine",
                value    : "TresD"
            );

            migrationBuilder.UpdateData(
                table    : "SalasCines",
                keyColumn: "Id",
                keyValue : 5,
                column   : "TipoSalaCine",
                value    : "DosD"
            );

            migrationBuilder.UpdateData(
                table    : "SalasCines",
                keyColumn: "Id",
                keyValue : 6,
                column   : "TipoSalaCine",
                value    : "TresD"
            );

            migrationBuilder.UpdateData(
                table    : "SalasCines",
                keyColumn: "Id",
                keyValue : 7,
                column   : "TipoSalaCine",
                value    : "Premium"
            );

            migrationBuilder.UpdateData(
                table    : "SalasCines",
                keyColumn: "Id",
                keyValue : 8,
                column   : "TipoSalaCine",
                value    : "DosD"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.AlterColumn<int>(
                name: "TipoSalaCine",
                table: "SalasCines",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "DosD"
            );

            migrationBuilder.UpdateData(
                table: "SalasCines",
                keyColumn: "Id",
                keyValue: 1,
                column: "TipoSalaCine",
                value: 1
            );

            migrationBuilder.UpdateData(
                table: "SalasCines",
                keyColumn: "Id",
                keyValue: 2,
                column: "TipoSalaCine",
                value: 2
            );

            migrationBuilder.UpdateData(
                table: "SalasCines",
                keyColumn: "Id",
                keyValue: 3,
                column: "TipoSalaCine",
                value: 1
            );

            migrationBuilder.UpdateData(
                table: "SalasCines",
                keyColumn: "Id",
                keyValue: 4,
                column: "TipoSalaCine",
                value: 2
            );

            migrationBuilder.UpdateData(
                table: "SalasCines",
                keyColumn: "Id",
                keyValue: 5,
                column: "TipoSalaCine",
                value: 1
            );

            migrationBuilder.UpdateData(
                table: "SalasCines",
                keyColumn: "Id",
                keyValue: 6,
                column: "TipoSalaCine",
                value: 2
            );

            migrationBuilder.UpdateData(
                table: "SalasCines",
                keyColumn: "Id",
                keyValue: 7,
                column: "TipoSalaCine",
                value: 3
            );

            migrationBuilder.UpdateData(
                table: "SalasCines",
                keyColumn: "Id",
                keyValue: 8,
                column: "TipoSalaCine",
                value: 1
            );
        }
    }
}
