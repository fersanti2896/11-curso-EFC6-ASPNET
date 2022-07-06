using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeliculasWebAPI.Entidades {
    // [Table("TablaGeneros", Schema = "peliculas")]
    // [Index(nameof(Nombre), IsUnique = true)]
    public class Genero : EntidadAuditable {
        public int Identificador { get; set; }
        //public int Id { get; set; }

        /* Forma de darle un limite a una propiedad */
        // [MaxLength(150)]
        
        /* Forma de que el campo no sea nulo */
        //[Required]

        /* Verifica que la data no haya sido actualizada por esta persona al mismo tiempo */
        [ConcurrencyCheck]
        public string Nombre { get; set; }
        public HashSet<Pelicula> Peliculas { get; set; }
        public bool EstaBorrado { get; set; }
        //public string Ejemplo { get; set; }
        //public string Ejemplo2 { get; set; }
    }
}
