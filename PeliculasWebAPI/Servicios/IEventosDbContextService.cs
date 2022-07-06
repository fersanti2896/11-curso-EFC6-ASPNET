using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PeliculasWebAPI.Servicios {
    public interface IEventosDbContextService {
        public void ManejarTracked(object sender, EntityTrackedEventArgs args);
        public void ManejarStateChange(object sender, EntityStateChangedEventArgs args);
        public void ManejarSavingChanges(object sender, SavingChangesEventArgs args);
        public void ManejarSavedChanges(object sender, SavedChangesEventArgs args);
        public void ManejarSavedChangesFailed(object sender, SaveChangesFailedEventArgs args);

    }
}
