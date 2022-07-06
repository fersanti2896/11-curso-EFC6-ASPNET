using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeliculasWebAPI.Migrations {
    public partial class CineDatalleTableSplitting : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<string>(
                name    : "CodigoEtica",
                table   : "Cines",
                type    : "nvarchar(max)",
                nullable: true
            );

            migrationBuilder.AddColumn<string>(
                name    : "Historia",
                table   : "Cines",
                type    : "nvarchar(max)",
                nullable: true
            );

            migrationBuilder.AddColumn<string>(
                name    : "Misiones",
                table   : "Cines",
                type    : "nvarchar(max)",
                nullable: true
            );

            migrationBuilder.AddColumn<string>(
                name    : "Valores",
                table   : "Cines",
                type    : "nvarchar(max)",
                nullable: true
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropColumn(
                name : "CodigoEtica",
                table: "Cines"
            );

            migrationBuilder.DropColumn(
                name : "Historia",
                table: "Cines"
            );

            migrationBuilder.DropColumn(
                name : "Misiones",
                table: "Cines"
            );

            migrationBuilder.DropColumn(
                name : "Valores",
                table: "Cines"
            );
        }
    }
}
