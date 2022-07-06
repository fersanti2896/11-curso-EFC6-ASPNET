using System.ComponentModel.DataAnnotations;

namespace PeliculasWebAPI.Entidades {
    public class Factura {
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int NumFactura { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
    }
}
