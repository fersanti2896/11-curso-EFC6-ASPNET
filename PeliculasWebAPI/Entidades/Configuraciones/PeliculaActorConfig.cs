using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PeliculasWebAPI.Entidades.Configuraciones {
    public class PeliculaActorConfig : IEntityTypeConfiguration<PeliculaActor> { 
        public void Configure(EntityTypeBuilder<PeliculaActor> builder) {
            /* Propiedades de la Tabla Intermedia PeliculaActor */
            builder.HasKey(prop => new {
                        prop.PeliculaId,
                        prop.ActorId
                    });

            builder.Property(prop => prop.Personaje)
                   .HasMaxLength(150);

            /* Doble configuración uno a muchos para ser muchos a muchos */
            builder.HasOne(pa => pa.Actor)
                   .WithMany(a => a.PeliculasActores)
                   .HasForeignKey(pa => pa.ActorId);

            builder.HasOne(pa => pa.Pelicula)
                   .WithMany(p => p.PeliculasActores)
                   .HasForeignKey(pa => pa.PeliculaId);      
        }
    }
}
