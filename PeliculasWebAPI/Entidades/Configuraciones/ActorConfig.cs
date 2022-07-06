using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PeliculasWebAPI.Entidades.Configuraciones {
    public class ActorConfig : IEntityTypeConfiguration<Actor> {
        public void Configure(EntityTypeBuilder<Actor> builder) {
            /* Propiedades de la Tabla Actor */
            builder.Property(prop => prop.Nombre)
                   .HasMaxLength(150)
                   .IsRequired();

            /* Mapeo Flexible */
            builder.Property(x => x.Nombre).HasField("_nombre");

            /* Lo omitimos ya que ya está en ConfigureConventions para que le de
             * la propiedad de "date" */
            /* builder.Property(prop => prop.FechaNac)
                      .HasColumnType("date"); */

            //builder.Ignore(a => a.Edad);
            //builder.Ignore(a => a.DireccionHogar);

            builder.OwnsOne(a => a.DireccionHogar, dir => {
                dir.Property(d => d.Calle).HasColumnName("Calle");
                dir.Property(d => d.Provincia).HasColumnName("Provincia");
                dir.Property(d => d.Pais).HasColumnName("Pais");
            });
        }
    }
}
