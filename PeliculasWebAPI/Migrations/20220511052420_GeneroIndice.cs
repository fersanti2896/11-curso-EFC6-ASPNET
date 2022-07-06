﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeliculasWebAPI.Migrations {
    public partial class GeneroIndice : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateIndex(
                name  : "IX_Generos_Nombre",
                table : "Generos",
                column: "Nombre",
                unique: true
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropIndex(
                name: "IX_Generos_Nombre",
                table: "Generos"
            );          
        }
    }
}
