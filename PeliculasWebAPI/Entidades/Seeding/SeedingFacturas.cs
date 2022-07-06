using Microsoft.EntityFrameworkCore;

namespace PeliculasWebAPI.Entidades.Seeding {
    public class SeedingFacturas {
        public static void Seed(ModelBuilder modelBuilder) {
            var fact1 = new Factura() { Id = 2, FechaCreacion = new DateTime(2022, 6, 9) };
            var deta1 = new List<FacturaDetalle>() {
                new FacturaDetalle() { Id = 3, FacturaId = fact1.Id, Precio = 250.99m },
                new FacturaDetalle() { Id = 4, FacturaId = fact1.Id, Precio = 10 },
                new FacturaDetalle() { Id = 5, FacturaId = fact1.Id, Precio = 45.50m }
            };

            var fact2 = new Factura() { Id = 3, FechaCreacion = new DateTime(2022, 6, 9) };
            var deta2 = new List<FacturaDetalle>() {
                new FacturaDetalle() { Id = 6, FacturaId = fact2.Id, Precio = 17.99m },
                new FacturaDetalle() { Id = 7, FacturaId = fact2.Id, Precio = 14 },
                new FacturaDetalle() { Id = 8, FacturaId = fact2.Id, Precio = 45 },
                new FacturaDetalle() { Id = 9, FacturaId = fact2.Id, Precio = 100 }
            };

            var fact3 = new Factura() { Id = 4, FechaCreacion = new DateTime(2022, 6, 9) };
            var deta3 = new List<FacturaDetalle>() {
                new FacturaDetalle() { Id = 10, FacturaId = fact3.Id, Precio = 371 },
                new FacturaDetalle() { Id = 11, FacturaId = fact3.Id, Precio = 114.99m },
                new FacturaDetalle() { Id = 12, FacturaId = fact3.Id, Precio = 425 },
                new FacturaDetalle() { Id = 13, FacturaId = fact3.Id, Precio = 1000 },
                new FacturaDetalle() { Id = 14, FacturaId = fact3.Id, Precio = 5 },
                new FacturaDetalle() { Id = 15, FacturaId = fact3.Id, Precio = 2.99m }
            };

            var fact4 = new Factura() { Id = 5, FechaCreacion = new DateTime(2022, 6, 9) };
            var deta4 = new List<FacturaDetalle>() {
                new FacturaDetalle() { Id = 16, FacturaId = fact4.Id, Precio = 50 }
            };

            modelBuilder.Entity<Factura>().HasData(fact1, fact2, fact3, fact4);
            modelBuilder.Entity<FacturaDetalle>().HasData(deta1);
            modelBuilder.Entity<FacturaDetalle>().HasData(deta2);
            modelBuilder.Entity<FacturaDetalle>().HasData(deta3);
            modelBuilder.Entity<FacturaDetalle>().HasData(deta4);
        }
    }
}
