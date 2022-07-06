﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace PeliculasWebAPI.Migrations {
    public partial class Final : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name   : "Actores",
                columns: table => new {
                    Id        = table.Column<int>(type: "int", nullable: false)
                                     .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre    = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Biografia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaNac  = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Actores", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name   : "Cines",
                columns: table => new {
                    Id        = table.Column<int>(type: "int", nullable: false)
                                     .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre    = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Ubicacion = table.Column<Point>(type: "geography", nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Cines", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name   : "Generos",
                columns: table => new {
                    Identificador = table.Column<int>(type: "int", nullable: false)
                                         .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre        = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Generos", x => x.Identificador);
                }
            );

            migrationBuilder.CreateTable(
                name   : "Peliculas",
                columns: table => new {
                    Id           = table.Column<int>(type: "int", nullable: false)
                                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo       = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Cartelera    = table.Column<bool>(type: "bit", nullable: false),
                    FechaEstreno = table.Column<DateTime>(type: "date", nullable: false),
                    PosterURL    = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Peliculas", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name   : "CinesOfertas",
                columns: table => new {
                    Id                  = table.Column<int>(type: "int", nullable: false)
                                               .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInicio         = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin            = table.Column<DateTime>(type: "date", nullable: false),
                    DescuentoPorcentaje = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    CineId              = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_CinesOfertas", x => x.Id);
                    table.ForeignKey(
                        name           : "FK_CinesOfertas_Cines_CineId",
                        column         : x => x.CineId,
                        principalTable : "Cines",
                        principalColumn: "Id",
                        onDelete       : ReferentialAction.Cascade);
                }
            );

            migrationBuilder.CreateTable(
                name   : "SalasCines",
                columns: table => new {
                    Id           = table.Column<int>(type: "int", nullable: false)
                                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoSalaCine = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Precio       = table.Column<decimal>(type: "decimal(9,3)", precision: 9, scale: 3, nullable: false),
                    CineId       = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_SalasCines", x => x.Id);
                    table.ForeignKey(
                        name           : "FK_SalasCines_Cines_CineId",
                        column         : x => x.CineId,
                        principalTable : "Cines",
                        principalColumn: "Id",
                        onDelete       : ReferentialAction.Cascade);
                }
            );

            migrationBuilder.CreateTable(
                name   : "GeneroPelicula",
                columns: table => new {
                    GenerosIdentificador = table.Column<int>(type: "int", nullable: false),
                    PeliculasId          = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_GeneroPelicula", x => new { x.GenerosIdentificador, x.PeliculasId });
                    table.ForeignKey(
                        name           : "FK_GeneroPelicula_Generos_GenerosIdentificador",
                        column         : x => x.GenerosIdentificador,
                        principalTable : "Generos",
                        principalColumn: "Identificador",
                        onDelete       : ReferentialAction.Cascade);
                    table.ForeignKey(
                        name           : "FK_GeneroPelicula_Peliculas_PeliculasId",
                        column         : x => x.PeliculasId,
                        principalTable : "Peliculas",
                        principalColumn: "Id",
                        onDelete       : ReferentialAction.Cascade);
                }
            );

            migrationBuilder.CreateTable(
                name   : "PeliculasActores",
                columns: table => new {
                    PeliculaId = table.Column<int>(type: "int", nullable: false),
                    ActorId    = table.Column<int>(type: "int", nullable: false),
                    Personaje  = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Orden      = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_PeliculasActores", x => new { x.PeliculaId, x.ActorId });
                    table.ForeignKey(
                        name           : "FK_PeliculasActores_Actores_ActorId",
                        column         : x => x.ActorId,
                        principalTable : "Actores",
                        principalColumn: "Id",
                        onDelete       : ReferentialAction.Cascade);
                    table.ForeignKey(
                        name           : "FK_PeliculasActores_Peliculas_PeliculaId",
                        column         : x => x.PeliculaId,
                        principalTable : "Peliculas",
                        principalColumn: "Id",
                        onDelete       : ReferentialAction.Cascade);
                }
            );

            migrationBuilder.CreateTable(
                name   : "PeliculaSalaCine",
                columns: table => new {
                    PeliculasId  = table.Column<int>(type: "int", nullable: false),
                    SalasCinesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_PeliculaSalaCine", x => new { x.PeliculasId, x.SalasCinesId });
                    table.ForeignKey(
                        name           : "FK_PeliculaSalaCine_Peliculas_PeliculasId",
                        column         : x => x.PeliculasId,
                        principalTable : "Peliculas",
                        principalColumn: "Id",
                        onDelete       : ReferentialAction.Cascade);
                    table.ForeignKey(
                        name           : "FK_PeliculaSalaCine_SalasCines_SalasCinesId",
                        column         : x => x.SalasCinesId,
                        principalTable : "SalasCines",
                        principalColumn: "Id",
                        onDelete       : ReferentialAction.Cascade);
                }
            );

            migrationBuilder.CreateIndex(
                name  : "IX_CinesOfertas_CineId",
                table : "CinesOfertas",
                column: "CineId",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name  : "IX_GeneroPelicula_PeliculasId",
                table : "GeneroPelicula",
                column: "PeliculasId"
            );

            migrationBuilder.CreateIndex(
                name  : "IX_PeliculasActores_ActorId",
                table : "PeliculasActores",
                column: "ActorId"
            );

            migrationBuilder.CreateIndex(
                name  : "IX_PeliculaSalaCine_SalasCinesId",
                table : "PeliculaSalaCine",
                column: "SalasCinesId"
            );

            migrationBuilder.CreateIndex(
                name  : "IX_SalasCines_CineId",
                table : "SalasCines",
                column: "CineId"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "CinesOfertas"
            );

            migrationBuilder.DropTable(
                name: "GeneroPelicula"
            );

            migrationBuilder.DropTable(
                name: "PeliculasActores"
            );

            migrationBuilder.DropTable(
                name: "PeliculaSalaCine"
            );

            migrationBuilder.DropTable(
                name: "Generos"
            );

            migrationBuilder.DropTable(
                name: "Actores"
            );

            migrationBuilder.DropTable(
                name: "Peliculas"
            );

            migrationBuilder.DropTable(
                name: "SalasCines"
            );

            migrationBuilder.DropTable(
                name: "Cines"
            );
        }
    }
}
