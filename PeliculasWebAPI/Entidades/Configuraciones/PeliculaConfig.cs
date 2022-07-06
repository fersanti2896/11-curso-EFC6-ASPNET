using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PeliculasWebAPI.Entidades.Configuraciones {
    public class PeliculaConfig : IEntityTypeConfiguration<Pelicula> {
        public void Configure(EntityTypeBuilder<Pelicula> builder) {
            /* Propiedades de la Tabla Pelicula */
            builder.Property(prop => prop.Titulo)
                   .HasMaxLength(250)
                   .IsRequired();

            builder.Property(prop => prop.PosterURL)
                   .HasMaxLength(500)
                   .IsUnicode(false);

            /* Configurando relación muchos a muchos sin entidad */
            /* builder.HasMany(p => p.Generos)
                   .WithMany(g => g.Peliculas)
                   .UsingEntity(j => j.ToTable("GenerosPeliculas")
                                      .HasData(new { PeliculasId = 1, GenerosIdentificador = 7 })
                    ); */
        }
    }
}
