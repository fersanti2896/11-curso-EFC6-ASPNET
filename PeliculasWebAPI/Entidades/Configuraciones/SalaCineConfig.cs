using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeliculasWebAPI.Entidades.Conversiones;

namespace PeliculasWebAPI.Entidades.Configuraciones {
    public class SalaCineConfig : IEntityTypeConfiguration<SalaCine> {
        public void Configure(EntityTypeBuilder<SalaCine> builder) {
            /* Propiedades de la Tabla SalaCine */
            builder.Property(prop => prop.Precio)
                   .HasPrecision(9, scale: 3);

            builder.Property(prop => prop.TipoSalaCine)
                   .HasDefaultValue(TipoSalaCine.DosD)
                   .HasConversion<string>();

            builder.Property(prop => prop.Moneda)
                   .HasConversion<MonedaSimboloConvert>();
        }
    }
}
