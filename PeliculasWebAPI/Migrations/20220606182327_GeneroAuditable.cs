using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeliculasWebAPI.Migrations {
    public partial class GeneroAuditable : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<string>(
                name     : "UsuarioCreacion",
                table    : "Generos",
                type     : "nvarchar(150)",
                maxLength: 150,
                nullable : true
            );

            migrationBuilder.AddColumn<string>(
                name     : "UsuarioModificacion",
                table    : "Generos",
                type     : "nvarchar(150)",
                maxLength: 150,
                nullable : true
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropColumn(
                name : "UsuarioCreacion",
                table: "Generos"
            );

            migrationBuilder.DropColumn(
                name : "UsuarioModificacion",
                table: "Generos"
            );
        }
    }
}
