using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PeliculasWebAPI.Entidades.Configuraciones {
    public class FacturaConfig : IEntityTypeConfiguration<Factura> {
        public void Configure(EntityTypeBuilder<Factura> builder){
            builder.ToTable(name: "Facturas", opc => {
                opc.IsTemporal(t => {
                    t.HasPeriodStart("Desde");
                    t.HasPeriodEnd("Hasta");
                    t.UseHistoryTable("FacturasHistorico");
                });
            });

            /* Configura columna de tipo Desde */
            builder.Property<DateTime>("Desde")
                   .HasColumnType("datetime2");

            /* Configura la columna de tipo Hasta */
            builder.Property<DateTime>("Hasta")
                   .HasColumnType("datetime2");

            builder.HasMany(typeof(FacturaDetalle))
                   .WithOne();

            builder.Property(f => f.NumFactura)
                   .HasDefaultValueSql("NEXT VALUE FOR factura.NumFactura");

            /** builder.Property(f => f.Version)
                   .IsRowVersion(); */
        }
    }
}
