using System.ComponentModel.DataAnnotations;

namespace PeliculasWebAPI.Entidades {
    public class CineDetalle {
        public int Id { get; set; }
        [Required]
        public string Historia { get; set; }
        public string Valores { get; set; }
        public string Misiones { get; set; }
        public string CodigoEtica { get; set; }
        
        /* Propieda de Navegación */
        public Cine Cine { get; set; }
    }
}
