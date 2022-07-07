using Microsoft.EntityFrameworkCore;
using PeliculasWebAPI.Entidades;

namespace PeliculasWebAPI.Servicios {
    public class Singleton {
        private readonly IServiceProvider serviceProvider;

        public Singleton(IServiceProvider serviceProvider) {
            this.serviceProvider = serviceProvider;
        }

        public async Task<IEnumerable<Genero>> ObtenerGeneros() {
            /* Creando un contexto artificial */
            await using (var scope = serviceProvider.CreateAsyncScope()) {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();

                return await context.Generos.ToListAsync();
            }
        }
    }
}
