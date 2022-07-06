using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PeliculasWebAPI.Entidades.Configuraciones {
    public class FacturaDetalleConfig : IEntityTypeConfiguration<FacturaDetalle> {
        public void Configure(EntityTypeBuilder<FacturaDetalle> builder) {
            builder.Property(f => f.Total)
                    /* stored guarda el valor en la columna */
                   .HasComputedColumnSql("Precio * Cantidad");
        }
    }
}
