using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeliculasWebAPI.Migrations {
    public partial class FotoActor : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AlterColumn<string>(
                name        : "PosterURL",
                table       : "Peliculas",
                type        : "varchar(600)",
                unicode     : false,
                maxLength   : 600,
                nullable    : true,
                oldClrType  : typeof(string),
                oldType     : "varchar(500)",
                oldUnicode  : false,
                oldMaxLength: 500,
                oldNullable : true
            );

            migrationBuilder.AddColumn<string>(
                name     : "FotoURL",
                table    : "Actores",
                type     : "varchar(600)",
                unicode  : false,
                maxLength: 600,
                nullable : true
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name : "FotoURL",
                table: "Actores"
            );

            migrationBuilder.AlterColumn<string>(
                name        : "PosterURL",
                table       : "Peliculas",
                type        : "varchar(500)",
                unicode     : false,
                maxLength   : 500,
                nullable    : true,
                oldClrType  : typeof(string),
                oldType     : "varchar(600)",
                oldUnicode  : false,
                oldMaxLength: 600,
                oldNullable : true
            );
        }
    }
}
