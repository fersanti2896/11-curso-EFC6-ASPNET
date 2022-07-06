using PeliculasWebAPI.Servicios;

namespace PeliculasAPIPruebas {
    [TestClass]
    public class ServicioUsuarioPruebas {
        [TestMethod]
        public void ObtenerUsuarioId_NoTraeNulloOVacio() {
            /* Preparación de la Prueba */
            var serviceUsuario = new UsuarioService();

            /* Prueba */
            var result = serviceUsuario.ObtenerUsuarioId();

            /* Verificación de la prueba */
            Assert.AreNotEqual("", result);
            Assert.IsNotNull(result);
        }
    }
}