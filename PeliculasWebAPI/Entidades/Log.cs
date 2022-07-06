using System.ComponentModel.DataAnnotations.Schema;

namespace PeliculasWebAPI.Entidades {
    public class Log {
        /* Generando la llave primaria */
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string Mensaje { get; set; }
    }
}
