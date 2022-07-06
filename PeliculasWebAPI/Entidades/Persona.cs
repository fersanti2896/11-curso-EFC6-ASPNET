using System.ComponentModel.DataAnnotations.Schema;

namespace PeliculasWebAPI.Entidades {
    public class Persona {
        public int Id { get; set; }
        public string Nombre { get; set; }

        /* Propiedades de Navegacion */
        [InverseProperty("Emisor")]
        public List<Mensaje> MensajesEnviados { get; set; }
        [InverseProperty("Receptor")]
        public List<Mensaje> MensajesRecibidos { get; set; }
    }
}
