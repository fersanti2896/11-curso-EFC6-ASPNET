using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PeliculasWebAPI.Servicios {
    public class EventosDbContextService : IEventosDbContextService {
        private readonly ILogger<EventosDbContextService> logger;

        public EventosDbContextService(ILogger<EventosDbContextService> logger){
            this.logger = logger;
        }

        public void ManejarTracked(object sender, EntityTrackedEventArgs args) {
            var msg = $"Entidad: { args.Entry.Entity } | Estado: { args.Entry.State }";
            logger.LogInformation(msg);
        }

        public void ManejarStateChange(object sender, EntityStateChangedEventArgs args) {
            var msg = $@"Entidad: { args.Entry.Entity } | Estado Anterior: { args.OldState } |
                         Estado Nuevo: { args.NewState }";
            logger.LogInformation(msg);
        }

        public void ManejarSavingChanges(object sender, SavingChangesEventArgs args) {
            var entidades = ((ApplicationDBContext)sender).ChangeTracker.Entries();

            foreach (var entidad in entidades) {
                var msg = $"Entidad: { entidad.Entity } var ser { entidad.State }.";
                logger.LogInformation(msg);
            }
        }

        public void ManejarSavedChanges(object sender, SavedChangesEventArgs args) {
            var msg = $"Fueron procesadas { args.EntitiesSavedCount } entidades.";
            logger.LogInformation(msg);
        }

        public void ManejarSavedChangesFailed(object sender, SaveChangesFailedEventArgs args) {
            logger.LogError(args.Exception, "Error en el SaveChanges");
        }
    }
}
