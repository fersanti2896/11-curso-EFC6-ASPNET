﻿using Microsoft.AspNetCore.Mvc;
using PeliculasWebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeliculasAPIPruebas {
    [TestClass]
    public class CinesControllerPruebas : BasePruebas {
        [TestMethod]
        /* Se manda lat y lng desde SD y se obtiene 2 cines cercanos */
        public async Task GET_Prueba() {
            var latitud  = 18.481139;
            var longitud = -69.938950;

            using (var context = LocalDbInicializador.GetDbContextLocalDb()) {
                var mapper       = ConfigurarAutoMapper();
                var controller   = new CinesController(context, mapper,
                                                       actualizadorObservableCollectionService: null);
                var respuesta    = await controller.Get(latitud, longitud);
                var objectResult = respuesta as ObjectResult;
                var cines        = (IEnumerable<object>)objectResult.Value;

                Assert.AreEqual(2, cines.Count());
            }
        }
    }
}
