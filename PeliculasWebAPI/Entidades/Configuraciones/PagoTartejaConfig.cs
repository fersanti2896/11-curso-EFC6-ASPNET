using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PeliculasWebAPI.Entidades.Configuraciones {
    public class PagoTartejaConfig : IEntityTypeConfiguration<PagoTarjeta> {
        public void Configure(EntityTypeBuilder<PagoTarjeta> builder) {
            builder.Property(p => p.FourDigits)
                   .HasColumnType("char(4)")
                   .IsRequired();

            /* Tiene data de prueba */
            var pago1 = new PagoTarjeta { 
                Id               = 1,
                FechaTransaccion = new DateTime(2022, 5, 25),
                Monto            = 9.99m,
                TipoPago         = TipoPago.Tarjeta,
                FourDigits       = "5678"
            };

            var pago2 = new PagoTarjeta { 
                Id               = 2,
                FechaTransaccion = new DateTime(2022, 5, 25),
                Monto            = 7.99m,
                TipoPago         = TipoPago.Tarjeta,
                FourDigits       = "1234"
            };

            builder.HasData(pago1, pago2);
        }
    }
}
