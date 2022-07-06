using PeliculasWebAPI.Entidades;
using PeliculasWebAPI.Servicios;

namespace PeliculasWebAPI.DTOs {
    public class SalaCineDTO : IId {
        public int Id { get; set; }
        public decimal Precio { get; set; }
        public TipoSalaCine TipoSalaCine { get; set; }
    }
}
