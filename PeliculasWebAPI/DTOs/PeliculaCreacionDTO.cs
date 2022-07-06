namespace PeliculasWebAPI.DTOs {
    public class PeliculaCreacionDTO {
        public string Titulo { get; set; }
        public bool Cartelera { get; set; }
        public DateTime FechaEstreno { get; set; }
        public List<int> Generos { get; set; }
        public List<int> SalasCine { get; set; }
        public List<PeliculaActorCreacionDTO> PeliculasActores { get; set; }
    }
}
