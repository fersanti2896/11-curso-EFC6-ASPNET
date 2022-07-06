using Microsoft.EntityFrameworkCore;

namespace PeliculasWebAPI.Entidades.SinLlaves {
    // [Keyless]
    public class CineSinUbicacion {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
