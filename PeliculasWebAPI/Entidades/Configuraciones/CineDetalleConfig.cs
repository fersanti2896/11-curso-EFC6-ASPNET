using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PeliculasWebAPI.Entidades.Configuraciones {
    public class CineDetalleConfig : IEntityTypeConfiguration<CineDetalle> {
        public void Configure(EntityTypeBuilder<CineDetalle> builder) {
            builder.ToTable("Cines");   
        }
    }
}
