using System.ComponentModel.DataAnnotations;

namespace PeliculasWebAPI.DTOs {
    public class CineOfertaDTO {
        [Range(1, 10)]
        public double DescuentoPorcentaje { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
