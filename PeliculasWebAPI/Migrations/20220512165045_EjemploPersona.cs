using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeliculasWebAPI.Migrations {
    public partial class EjemploPersona : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name   : "Personas",
                columns: table => new {
                    Id     = table.Column<int>(type: "int", nullable: false)
                                  .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name   : "Mensajes",
                columns: table => new {
                    Id         = table.Column<int>(type: "int", nullable: false)
                                      .Annotation("SqlServer:Identity", "1, 1"),
                    Contenido  = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmisorId   = table.Column<int>(type: "int", nullable: false),
                    ReceptorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Mensajes", x => x.Id);
                    table.ForeignKey(
                        name           : "FK_Mensajes_Personas_EmisorId",
                        column         : x => x.EmisorId,
                        principalTable : "Personas",
                        principalColumn: "Id",
                        onDelete       : ReferentialAction.Restrict
                    );
                    table.ForeignKey(
                        name           : "FK_Mensajes_Personas_ReceptorId",
                        column         : x => x.ReceptorId,
                        principalTable : "Personas",
                        principalColumn: "Id",
                        onDelete       : ReferentialAction.Restrict
                    );
                });

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

            migrationBuilder.InsertData(
                table  : "Personas",
                columns: new[] { "Id", "Nombre" },
                values : new object[,] {
                    { 1, "Fernando" },
                    { 2, "Maria" }
                }
            );

            migrationBuilder.InsertData(
                table : "Mensajes",
                columns: new[] { "Id", "Contenido", "EmisorId", "ReceptorId" },
                values : new object[,] {
                    { 1, "Hola Maria!", 1, 2 },
                    { 2, "Hola Fernando! ¿cómo estás?", 2, 1 },
                    { 3, "Me encuentro bien, ´¿cómo te encuentras?", 1, 2 },
                    { 4, "Muy bien!", 2, 1 }
                }
            );

            migrationBuilder.CreateIndex(
                name  : "IX_Mensajes_EmisorId",
                table : "Mensajes",
                column: "EmisorId"
            );

            migrationBuilder.CreateIndex(
                name  : "IX_Mensajes_ReceptorId",
                table : "Mensajes",
                column: "ReceptorId"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "Mensajes"
            );

            migrationBuilder.DropTable(
                name: "Personas"
            );

            migrationBuilder.UpdateData(
                table    : "CinesOfertas",
                keyColumn: "Id",
                keyValue : 1,
                columns  : new[] { "FechaFin", "FechaInicio" },
                values   : new object[] { new DateTime(2022, 5, 18, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 5, 11, 0, 0, 0, 0, DateTimeKind.Local) }
            );

            migrationBuilder.UpdateData(
                table    : "CinesOfertas",
                keyColumn: "Id",
                keyValue : 2,
                columns  : new[] { "FechaFin", "FechaInicio" },
                values   : new object[] { new DateTime(2022, 5, 16, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 5, 11, 0, 0, 0, 0, DateTimeKind.Local) }
            );

            migrationBuilder.UpdateData(
                table    : "Peliculas",
                keyColumn: "Id",
                keyValue : 5,
                column   : "FechaEstreno",
                value    : new DateTime(2022, 5, 11, 0, 0, 0, 0, DateTimeKind.Local)
            );
        }
    }
}
