using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeliculasWebAPI.Migrations {
    public partial class CampoMoneda : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<string>(
                name        : "Moneda",
                table       : "SalasCines",
                type        : "nvarchar(max)",
                nullable    : false,
                defaultValue: ""
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropColumn(
                name : "Moneda",
                table: "SalasCines"
            );
        }
    }
}
