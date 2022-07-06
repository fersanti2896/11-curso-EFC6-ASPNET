using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PeliculasWebAPI.Entidades.Configuraciones {
    public class PagoConfig : IEntityTypeConfiguration<Pago> {
        public void Configure(EntityTypeBuilder<Pago> builder) {
            /* Va elegir si es en paypal o tarjeta */
            builder.HasDiscriminator(p => p.TipoPago)
                   .HasValue<PagoPaypal>(TipoPago.Paypal)
                   .HasValue<PagoTarjeta>(TipoPago.Tarjeta);

            builder.Property(p => p.Monto)
                   .HasPrecision(18, 2);
        }
    }
}
