using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeliculasWebAPI.Migrations {
    public partial class VistaPeliculasConteo : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.Sql(@" CREATE VIEW [dbo].[PeliculasConteos]
                                    AS
                                    SELECT Id, Titulo,
                                    (SELECT COUNT(*) FROM GeneroPelicula
                                     WHERE PeliculasId = Peliculas.Id)
                                     AS CantidadGeneros,
                                    (SELECT COUNT(DISTINCT CineId) FROM PeliculaSalaCine
                                    INNER JOIN SalasCines
                                    ON SalasCines.Id = PeliculaSalaCine.SalasCinesId
                                    WHERE PeliculasId = Peliculas.Id)
                                    AS CantidadCines,
                                    (SELECT COUNT(*) FROM PeliculasActores
                                    WHERE PeliculaId = Peliculas.Id)
                                    AS CantidadActores
                                    FROM Peliculas");
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.Sql("DROP VIEW [dbo].[PeliculasConteos]");
        }
    }
}
