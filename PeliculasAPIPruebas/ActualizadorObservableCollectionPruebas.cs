using PeliculasAPIPruebas.Mocks;
using PeliculasWebAPI.Servicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeliculasAPIPruebas {
    [TestClass]
    public class ActualizadorObservableCollectionPruebas  {
        /* El método actualizar recibe entidades, sin son vacías, entonces sus DTOs pasan a entidades */
        [TestMethod]
        public void Actualizar_Prueba1() {
            /* Preparacion */
            var mapeador                         = new Mapeador();
            var actualizadorObservableCollection = new ActualizadorObservableCollectionService(mapeador);
            var entidades                        = new ObservableCollection<ConId>();
            var dtos                             = new List<ConId>() { new ConId { Id = 1 }, new ConId { Id = 2 } };

            /* Prueba */
            actualizadorObservableCollection.Actualizar(entidades, dtos);

            /* Verificación */
            Assert.AreEqual(2, entidades.Count);
            Assert.AreEqual(1, entidades[0].Id);
            Assert.AreEqual(2, entidades[1].Id);

        }

        /* Si DTO es vacío, entonces todas las entidades deben ser removidas.  */
        [TestMethod]
        public void Actualizar_Prueba2() {
            /* Preparación */
            var mapeador                          = new Mapeador();
            var actualizadorObservableCollection  = new ActualizadorObservableCollectionService(mapeador);
            var entidades                         = new ObservableCollection<ConId>() 
                                                       { new ConId { Id = 1 }, new ConId { Id = 2 } };
            var dtos                              = new List<ConId>();

            /* Prueba */
            actualizadorObservableCollection.Actualizar(entidades, dtos);

            /* Verificación */
            Assert.AreEqual(0, entidades.Count);
        }

        /* El método actualiza si las entidades y DTOs tiene los mismo objetos, entonces las colecciones no se modifican */
        [TestMethod]
        public void Actualizar_Prueba3() {
            /* Preparación */
            var mapeador                         = new Mapeador();
            var actualizadorObservableCollection = new ActualizadorObservableCollectionService(mapeador);
            var entidades                        = new ObservableCollection<ConId>()
                                                        { new ConId { Id = 1 }, new ConId { Id = 2 } };
            var dtos                             = new List<ConId>() { new ConId { Id = 1 }, new ConId { Id = 2 } };

            /* Prueba */
            actualizadorObservableCollection.Actualizar(entidades, dtos);

            /* Verificar */
            Assert.AreEqual(2, entidades.Count);
            Assert.AreEqual(2, dtos.Count);
        }
    }
}
