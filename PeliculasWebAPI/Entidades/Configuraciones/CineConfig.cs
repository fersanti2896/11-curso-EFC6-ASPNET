using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PeliculasWebAPI.Entidades.Configuraciones {
    public class CineConfig : IEntityTypeConfiguration<Cine> {
        public void Configure(EntityTypeBuilder<Cine> builder) {
            /* Propiedades de la Tabla Cine */
            builder.Property(prop => prop.Nombre)
                   .HasMaxLength(150)
                   .IsRequired();

            /* Indica que tiene uno Cine un CineOferta */
            builder.HasOne(c => c.CineOferta)
                    .WithOne()
                    .HasForeignKey<CineOferta>(co => co.CineId);

            /* Indica que un Cine tiene muchas Salas de Cine */
            builder.HasMany(c => c.SalaCine)
                   .WithOne(s => s.Cine)
                   .HasForeignKey(s => s.CineId)
                   .OnDelete(DeleteBehavior.Cascade);

            /* Indicamos que Cine y CineDetalle apunta a la misma tabla */
            builder.HasOne(c => c.CineDetalle)
                   .WithOne(cd => cd.Cine)
                   .HasForeignKey<CineDetalle>(cd => cd.Id);

            builder.OwnsOne(c => c.Direccion, dir => {
                dir.Property(d => d.Calle).HasColumnName("Calle");
                dir.Property(d => d.Provincia).HasColumnName("Provincia");
                dir.Property(d => d.Pais).HasColumnName("Pais");
            });

            builder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangedNotifications);
        }
    }
}
