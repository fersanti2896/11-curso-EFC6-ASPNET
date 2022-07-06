namespace PeliculasWebAPI.DTOs {
    public class PeliculaFiltroDTO {
        public string Titulo { get; set; }
        public int GeneroId { get; set; }
        public bool Cartelera { get; set; }
        public bool ProxEstrenos { get; set; }
    }
}
