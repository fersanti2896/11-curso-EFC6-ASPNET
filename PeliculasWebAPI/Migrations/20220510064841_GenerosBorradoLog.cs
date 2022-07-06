using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeliculasWebAPI.Migrations {
    public partial class GenerosBorradoLog : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<bool>(
                name        : "EstaBorrado",
                table       : "Generos",
                type        : "bit",
                nullable    : false,
                defaultValue: false
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropColumn(
                name : "EstaBorrado",
                table: "Generos"
            );
        }
    }
}
