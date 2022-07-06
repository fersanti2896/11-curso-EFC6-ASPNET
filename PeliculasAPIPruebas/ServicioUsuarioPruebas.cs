using PeliculasWebAPI.Servicios;

namespace PeliculasAPIPruebas {
    [TestClass]
    public class ServicioUsuarioPruebas {
        [TestMethod]
        public void ObtenerUsuarioId_NoTraeNulloOVacio() {
            /* Preparaci�n de la Prueba */
            var serviceUsuario = new UsuarioService();

            /* Prueba */
            var result = serviceUsuario.ObtenerUsuarioId();

            /* Verificaci�n de la prueba */
            Assert.AreNotEqual("", result);
            Assert.IsNotNull(result);
        }
    }
}