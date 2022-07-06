using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeliculasWebAPI.Migrations {
    public partial class Secuencia : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.EnsureSchema(
                name: "factura"
            );

            migrationBuilder.CreateSequence<int>(
                name  : "NumFactura",
                schema: "factura"
            );

            migrationBuilder.AddColumn<int>(
                name           : "NumFactura",
                table          : "Facturas",
                type           : "int",
                nullable       : false,
                defaultValueSql: "NEXT VALUE FOR factura.NumFactura"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropSequence(
                name  : "NumFactura",
                schema: "factura"
            );

            migrationBuilder.DropColumn(
                name : "NumFactura",
                table: "Facturas"
            );
        }
    }
}
