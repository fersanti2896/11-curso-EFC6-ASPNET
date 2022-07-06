using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeliculasWebAPI.Migrations {
    public partial class CineOfertaNulo : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropForeignKey(
                name : "FK_CinesOfertas_Cines_CineId",
                table: "CinesOfertas"
            );

            migrationBuilder.DropIndex(
                name : "IX_CinesOfertas_CineId",
                table: "CinesOfertas"
            );

            migrationBuilder.AlterColumn<int>(
                name      : "CineId",
                table     : "CinesOfertas",
                type      : "int",
                nullable  : true,
                oldClrType: typeof(int),
                oldType   : "int"
            );

            migrationBuilder.CreateIndex(
                name : "IX_CinesOfertas_CineId",
                table : "CinesOfertas",
                column: "CineId",
                unique: true,
                filter: "[CineId] IS NOT NULL"
            );

            migrationBuilder.AddForeignKey(
                name           : "FK_CinesOfertas_Cines_CineId",
                table          : "CinesOfertas",
                column         : "CineId",
                principalTable : "Cines",
                principalColumn: "Id"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropForeignKey(
                name : "FK_CinesOfertas_Cines_CineId",
                table: "CinesOfertas"
            );

            migrationBuilder.DropIndex(
                name : "IX_CinesOfertas_CineId",
                table: "CinesOfertas"
            );

            migrationBuilder.AlterColumn<int>(
                name: "CineId",
                table       : "CinesOfertas",
                type        : "int",
                nullable    : false,
                defaultValue: 0,
                oldClrType  : typeof(int),
                oldType     : "int",
                oldNullable : true
            );

            migrationBuilder.CreateIndex(
                name  : "IX_CinesOfertas_CineId",
                table : "CinesOfertas",
                column: "CineId",
                unique: true
            );

            migrationBuilder.AddForeignKey(
                name           : "FK_CinesOfertas_Cines_CineId",
                table          : "CinesOfertas",
                column         : "CineId",
                principalTable : "Cines",
                principalColumn: "Id",
                onDelete       : ReferentialAction.Cascade
            );
        }
    }
}
