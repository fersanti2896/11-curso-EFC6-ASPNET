using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PeliculasWebAPI.Entidades.Configuraciones {
    public class CineOfertaConfig : IEntityTypeConfiguration<CineOferta> {
        public void Configure(EntityTypeBuilder<CineOferta> builder) {
            /* Propiedades de la Tabla CineOferta */
            builder.Property(prop => prop.DescuentoPorcentaje)
                   .HasPrecision(precision: 5, scale: 2);
        }
    }
}
