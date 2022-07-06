using Microsoft.EntityFrameworkCore;
using PeliculasWebAPI.Entidades;
using PeliculasWebAPI.Entidades.Configuraciones;
using PeliculasWebAPI.Entidades.Funciones;
using PeliculasWebAPI.Entidades.Seeding;
using PeliculasWebAPI.Entidades.SinLlaves;
using PeliculasWebAPI.Servicios;
using System.Reflection;

namespace PeliculasWebAPI {
    public class ApplicationDBContext : DbContext {
        private readonly IUsuarioService usuarioService;
        //private readonly IEventosDbContextService eventosDbContextService;

        /* Al usar DbContextOptions podemos usar la inyección de dependencias */
        public ApplicationDBContext(DbContextOptions options,
            IUsuarioService usuarioService,
            IEventosDbContextService eventosDbContextService) : base(options) {
            this.usuarioService = usuarioService;

            if (eventosDbContextService is not null) {
                //ChangeTracker.Tracked      += eventosDbContextService.ManejarTracked;
                //ChangeTracker.StateChanged += eventosDbContextService.ManejarStateChange;
                SavingChanges              += eventosDbContextService.ManejarSavingChanges;
                SavedChanges               += eventosDbContextService.ManejarSavedChanges;
                SaveChangesFailed          += eventosDbContextService.ManejarSavedChangesFailed;
            }
        }

        public ApplicationDBContext() {
        }

        /* Itera todas las entidades que van estar salvadas */
        private void ProcesarSalvado() {
            foreach (var item in ChangeTracker.Entries()
                                             .Where(e => e.State == EntityState.Added
                                                && e.Entity is EntidadAuditable)) { 
                var entidad = item.Entity as EntidadAuditable;
                entidad.UsuarioCreacion     = usuarioService.ObtenerUsuarioId();
                entidad.UsuarioModificacion = usuarioService.ObtenerUsuarioId();
            }

            foreach (var item in ChangeTracker.Entries()
                                             .Where(e => e.State == EntityState.Modified
                                                && e.Entity is EntidadAuditable)) { 
                var entidad = item.Entity as EntidadAuditable;
                entidad.UsuarioModificacion = usuarioService.ObtenerUsuarioId();
                item.Property(nameof(entidad.UsuarioCreacion)).IsModified = false;
            }
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default) {
            ProcesarSalvado();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer("name-DefaultConnection", opc => {
                    opc.UseNetTopologySuite();
                })
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }   
        }

        /* Sirve para configurar una propiedad manual y no por defecto */
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) {
            configurationBuilder.Properties<DateTime>()
                                .HaveColumnType("date");
        }

        /* API Fluente */
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            /* Implementando la configuracion de la clase Genero */
            /* Aunque este es una forma, pero si tenemos varias configuraciones, sería
             * muchas lineas de código */
            // modelBuilder.ApplyConfiguration(new GeneroConfig());

            /* La forma correcta es por Assembly, el cual scanea en el proyecto
             * todas las configuraciones que heredan de IEntityTypeConfiguration */
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if (!Database.IsInMemory()) {
                SeedingModuloConsulta.Seed(modelBuilder);
                SeedingPersonaMensaje.Seed(modelBuilder);
                SeedingFacturas.Seed(modelBuilder);
            }     
            
            /* Registro de Clase Auxiliar de Funciones Definidas por el Usuario */
            Escalares.RegistrarFunciones(modelBuilder);

            /* Creamos la secuencia numero de factura */
            modelBuilder.HasSequence<int>("NumFactura", "factura");

            /* modelBuilder.Entity<Log>()
                        .Property(l => l.Id)
                        .ValueGeneratedNever(); */

            /* Ignorando una clase */
            //modelBuilder.Ignore<Direccion>();

            modelBuilder.Entity<CineSinUbicacion>()
                        .HasNoKey() /* Hace que la entidad no tenga llave primaria */
                        .ToSqlQuery("SELECT Id, Nombre FROM Cines")
                        .ToView(null); /* Evita que se agruege la tabla con el esquema a la BD */

            /* Retorna la vista de PeliculasConteos */
            /* modelBuilder.Entity<PeliculaConteos>()
                        .HasNoKey()
                        .ToView("PeliculasConteos"); */

            /* Centralizando el querie arbitrario */
            /* modelBuilder.Entity<PeliculaConteos>()
                        .ToSqlQuery(@"SELECT Id, Titulo,
                                    (SELECT COUNT(*) FROM GeneroPelicula
                                     WHERE PeliculasId = Peliculas.Id)
                                     AS CantidadGeneros,
                                    (SELECT COUNT(DISTINCT CineId) FROM PeliculaSalaCine
                                    INNER JOIN SalasCines
                                    ON SalasCines.Id = PeliculaSalaCine.SalasCinesId
                                    WHERE PeliculasId = Peliculas.Id)
                                    AS CantidadCines,
                                    (SELECT COUNT(*) FROM PeliculasActores
                                    WHERE PeliculaId = Peliculas.Id)
                                    AS CantidadActores
                                    FROM Peliculas"); */

            /* Para usar la función definida por el usuario */
            modelBuilder.Entity<PeliculaConteos>().HasNoKey()
                                                  .ToTable(name: null);

            modelBuilder.HasDbFunction(() => PeliculaConteo(0));

            /* Configuracion para una tipo de dato URL */
            foreach (var tipoEntidad in modelBuilder.Model.GetEntityTypes()) {
                foreach (var prop in tipoEntidad.GetProperties()) { 
                    if(prop.ClrType == typeof(string) && prop.Name.Contains("URL", StringComparison.CurrentCultureIgnoreCase)) {
                        prop.SetIsUnicode(false);
                        prop.SetMaxLength(600);
                    }
                }
            }

            modelBuilder.Entity<Mercancia>().ToTable("Mercancia");
            modelBuilder.Entity<PeliculaAlquilable>().ToTable("PeliculasAlquilables");

            var pelicula1 = new PeliculaAlquilable() { 
                Id         = 1,
                Nombre     = "Dr Strange",
                PeliculaId = 6,
                Precio     = 5.99m
            };

            var mercancia1 = new Mercancia() {
                Id                   = 2,
                DisponibleInventario = true,
                EsRopa               = true,
                Nombre               = "Taza coleccionable",
                Peso                 =  1,
                Volumen              = 1, 
                Precio               = 11
            };

            modelBuilder.Entity<Mercancia>().HasData(mercancia1);
            modelBuilder.Entity<PeliculaAlquilable>().HasData(pelicula1);
        }

        /* Primera Forma de utilizar las funciones definidas por el Usuario desde EF */
        [DbFunction]
        public int FacturaDetalleSuma(int facturaId) {
            return 0;
        }

        /* Función con Valores de Tablas */
        public IQueryable<PeliculaConteos> PeliculaConteo(int peliculaId) {
            return FromExpression(() => PeliculaConteo(peliculaId));
        }

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actores { get; set; }
        public DbSet<Cine> Cines { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<CineOferta> CinesOfertas { get; set; }
        public DbSet<SalaCine> SalasCines { get; set; }
        public DbSet<PeliculaActor> PeliculasActores { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<CineSinUbicacion> CineSinUbicacion { get; set; }
        public DbSet<PeliculaConteos> PeliculasConteos { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Mensaje> Mensajes { get; set; }
        public DbSet<CineDetalle> CineDetalle { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<FacturaDetalle> FacturaDetalles { get; set; }
    }
}
