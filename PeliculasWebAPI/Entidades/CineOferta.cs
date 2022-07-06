namespace PeliculasWebAPI.Entidades {
    public class CineOferta {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal DescuentoPorcentaje { get; set; }

        /* CineId es un FK y no puede ser nulo, por lo cual por el momento 
           la relación es requerida */

        /* Si se quiere cambiar a relación opcional se usa ? */
        public int? CineId { get; set; }
    }
}
