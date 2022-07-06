using Microsoft.EntityFrameworkCore;
using PeliculasWebAPI.Controllers;
using PeliculasWebAPI.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeliculasAPIPruebas{
    [TestClass]
    public class GenerosControllerPruebas : BasePruebas {
        [TestMethod]
        /* Prueba si dos generos se envían por POST ambos se insertan */
        public async Task POST_EnvioInserccion() {
            /* Preparación */
            var nombreDB          = Guid.NewGuid().ToString();
            var contexto1         = ConstruirContext(nombreDB);
            var generosController = new GenerosController(contexto1, mapper: null);
            var generos           = new Genero[] {
                new Genero(){Nombre = "Genero 1"},
                new Genero(){Nombre = "Genero 2"}
            };

            /* Prueba */
            await generosController.Post(generos);

            /* Verificación */
            /* Aplicamos un segundo contexto porque puede que los datos se encuentren en memoria
             * y nos pueda dar un falso-positivo en la prueba */
            var contexto2 = ConstruirContext(nombreDB);
            var generosDB = await contexto2.Generos.ToListAsync();

            Assert.AreEqual(2, generosDB.Count);

            var existeGenero1 = generosDB.Any(g => g.Nombre == "Genero 1");
            Assert.IsTrue(existeGenero1, message: "El genero 1 no fue encontrado");

            var existeGenero2 = generosDB.Any(g => g.Nombre == "Genero 2");
            Assert.IsTrue(existeGenero2, message: "El genero 2 no fue encontrado");
        }

        [TestMethod]
        /* Si se manda un genero con NombreOriginal incorrecto, una excepcion es arrojada */
        public async Task PUT_EnvioExcepcion() {
            /* Preparación */
            var nombreDB     = Guid.NewGuid().ToString();
            var contexto1    = ConstruirContext(nombreDB);
            var mapper       = ConfigurarAutoMapper();
            var generoPrueba = new Genero() { Nombre = "Genero 1" };

            contexto1.Add(generoPrueba);
            await contexto1.SaveChangesAsync();

            var contexto2         = ConstruirContext(nombreDB);
            var generosController = new GenerosController(contexto2, mapper);

            /* Prueba y Verificación */
            await Assert.ThrowsExceptionAsync<DbUpdateConcurrencyException>(() =>
                generosController.Put(new PeliculasWebAPI.DTOs.GeneroActualizacionDTO() {
                    Identificador   = generoPrueba.Identificador,
                    Nombre          = "Genero 2",
                    Nombre_Original = "Nombre incorrecto"
                })
            );
        }

        [TestMethod]
        /* Si envio un genero con nombreOriginal correcto, entonces actualiza el genero */
        public async Task PUT_EnvioCorrecto() {
            /* Preparación */
            var nombreDB     = Guid.NewGuid().ToString();
            var contexto1    = ConstruirContext(nombreDB);
            var mapper       = ConfigurarAutoMapper();
            var generoPrueba = new Genero() { Nombre = "Genero 1" };

            contexto1.Add(generoPrueba);
            await contexto1.SaveChangesAsync();

            var contexto2         = ConstruirContext(nombreDB);
            var generosController = new GenerosController(contexto2, mapper);

            /* Prueba */

            await generosController.Put(new PeliculasWebAPI.DTOs.GeneroActualizacionDTO() {
                Identificador   = generoPrueba.Identificador,
                Nombre          = "Genero 2",
                Nombre_Original = "Genero 1"
            });

            /* Verificación */

            var contexto3 = ConstruirContext(nombreDB);
            var generoDB  = await contexto3.Generos.SingleAsync();

            Assert.AreEqual(generoPrueba.Identificador, generoDB.Identificador);
            Assert.AreEqual("Genero 2", generoDB.Nombre);
        }
    }
}
