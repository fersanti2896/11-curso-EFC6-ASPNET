using Microsoft.EntityFrameworkCore;

namespace PeliculasWebAPI.Entidades.Seeding {
    public static class SeedingPersonaMensaje {
        public static void Seed(ModelBuilder modelBuilder) { 
            var fernando = new Persona() { Id = 1, Nombre = "Fernando" };
            var maria    = new Persona() { Id = 2, Nombre = "Maria" };

            var msg1 = new Mensaje() { 
                Id         = 1, 
                Contenido  = "Hola Maria!", 
                EmisorId   = fernando.Id,
                ReceptorId = maria.Id,
            };

            var msg2 = new Mensaje() { 
                Id         = 2, 
                Contenido  = "Hola Fernando! ¿cómo estás?", 
                EmisorId   = maria.Id,
                ReceptorId = fernando.Id,
            };

            var msg3 = new Mensaje() { 
                Id         = 3, 
                Contenido  = "Me encuentro bien, ´¿cómo te encuentras?", 
                EmisorId   = fernando.Id,
                ReceptorId = maria.Id,
            };

            var msg4 = new Mensaje() { 
                Id         = 4, 
                Contenido  = "Muy bien!", 
                EmisorId   = maria.Id,
                ReceptorId = fernando.Id,
            };

            modelBuilder.Entity<Persona>().HasData(fernando, maria);
            modelBuilder.Entity<Mensaje>().HasData(msg1, msg2, msg3, msg4);
        }
    }
}
