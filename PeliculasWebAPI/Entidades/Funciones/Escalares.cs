using Microsoft.EntityFrameworkCore;

namespace PeliculasWebAPI.Entidades.Funciones {
    public static class Escalares {
        /* Segunda Forma de utilizar las funciones definidas por el Usuario desde EF */
        public static void RegistrarFunciones(ModelBuilder modelBuilder) {
            modelBuilder.HasDbFunction(() => FacturaDetallePromedio(0));
        }

        public static decimal FacturaDetallePromedio(int facturaId) {
            return 0;
        }
    }
}
