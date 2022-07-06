using System.Collections.ObjectModel;

namespace PeliculasWebAPI.Servicios {
    public interface IActualizadorObservableCollectionService {
        public void Actualizar<ENT, DTO>(ObservableCollection<ENT> entidades, IEnumerable<DTO> dts)
        where ENT : IId
        where DTO : IId;
    }
}
