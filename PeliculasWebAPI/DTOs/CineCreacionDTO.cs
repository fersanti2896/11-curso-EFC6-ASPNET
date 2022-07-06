using System.ComponentModel.DataAnnotations;

namespace PeliculasWebAPI.DTOs {
    public class CineCreacionDTO {
        [Required]
        public string Nombre { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public CineOfertaDTO CineOferta { get; set; }
        public SalaCineDTO[] SalasCines { get; set; }
    }
}
