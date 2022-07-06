using Microsoft.EntityFrameworkCore;

namespace PeliculasWebAPI.Entidades {
    public class Pelicula {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public bool Cartelera { get; set; }
        public DateTime FechaEstreno { get; set; }

        //[Unicode(false)]
        public string PosterURL { get; set; }
        public List<Genero> Generos { get; set; }
        public List<SalaCine> SalasCines { get; set; }
        public List<PeliculaActor> PeliculasActores { get; set; }
    }
}
