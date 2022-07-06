using AutoMapper;
using System.Collections.ObjectModel;

namespace PeliculasWebAPI.Servicios {
    public class ActualizadorObservableCollectionService : IActualizadorObservableCollectionService {
        private readonly IMapper mapper;

        public ActualizadorObservableCollectionService(IMapper mapper) {
            this.mapper = mapper;
        }

        public void Actualizar<ENT, DTO>(ObservableCollection<ENT> entidades, IEnumerable<DTO> dts)
        where ENT : IId
        where DTO : IId {
            var dicEntidades = entidades.ToDictionary(x => x.Id);
            var dicDTOs      = dts.ToDictionary(x => x.Id);

            var idsEntidades = dicEntidades.Select(x => x.Key);
            var idsDTOs      = dicDTOs.Select(x => x.Key);

            /* Aquellas entidades que están en DTOs que no están en el listado, las van a crear */
            var crear = idsDTOs.Except(idsEntidades);

            /* Lo que está en entidades que no está en DTOs */
            var borrar = idsEntidades.Except(idsDTOs);

            /* Interseccion de entidades con DTOs */
            var actualizar = idsEntidades.Intersect(idsDTOs);

            foreach (var id in crear) {
                var entidad = mapper.Map<ENT>(dicDTOs[id]);
                entidades.Add(entidad);
            }

            foreach (var id in borrar) {
                var entidad = dicEntidades[id];
                entidades.Remove(entidad);
            }

            foreach (var id in actualizar) {
                var dto     = dicDTOs[id];
                var entidad = dicEntidades[id];

                entidad = mapper.Map(dto, entidad);
            }
        }
    }
}
