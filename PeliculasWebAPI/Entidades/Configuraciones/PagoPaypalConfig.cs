using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PeliculasWebAPI.Entidades.Configuraciones {
    public class PagoPaypalConfig : IEntityTypeConfiguration<PagoPaypal> {
        public void Configure(EntityTypeBuilder<PagoPaypal> builder) {
            builder.Property(p => p.Email)
                    .HasMaxLength(150)
                    .IsRequired();

            /* Tiene data de prueba */
            var pago1 = new PagoPaypal { 
                Id               = 3,
                FechaTransaccion = new DateTime(2022, 5, 25),
                Monto            = 9.99m,
                TipoPago         = TipoPago.Paypal,
                Email            = "prueba@prueba.com"
            };

            var pago2 = new PagoPaypal { 
                Id               = 4,
                FechaTransaccion = new DateTime(2022, 5, 25),
                Monto            = 7.99m,
                TipoPago         = TipoPago.Paypal,
                Email            = "prueba2@prueba.com"
            };

            builder.HasData(pago1, pago2);
        }
    }
}
