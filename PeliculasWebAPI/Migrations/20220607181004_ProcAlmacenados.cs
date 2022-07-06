using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeliculasWebAPI.Migrations {
    public partial class ProcAlmacenados : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.Sql(@"CREATE PROCEDURE Generos_ObtenerPorId
                                   @Id INT
                                    AS BEGIN
                                    SET NOCOUNT ON;

                                    SELECT * FROM Generos
                                    WHERE Identificador = @Id;
                                    END");

            migrationBuilder.Sql(@"CREATE PROCEDURE Generos_Insertar
                                   @Nombre NVARCHAR(150),
                                   @Id INT OUTPUT
                                   AS BEGIN
                                   SET NOCOUNT ON;

                                   INSERT INTO Generos(Nombre)
                                   VALUES (@Nombre);

                                   SELECT @Id = SCOPE_IDENTITY();
                                   END");
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[Generos_ObtenerPorId]");
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[Generos_Insertar]");
        }
    }
}
