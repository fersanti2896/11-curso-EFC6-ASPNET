using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System.Collections.ObjectModel;

namespace PeliculasWebAPI.Entidades {
    public class Cine : Notificacion {
        public int Id { get; set; }

        private string _nombre;
        public string Nombre { get => _nombre; set => Set(value, ref _nombre); }

        private Point _ubicacion;
        public Point Ubicacion { get => _ubicacion; set => Set(value, ref _ubicacion); }

        /* Propiedades de navegación, entidades de relación */
        private CineOferta _cineOfera;
        public CineOferta CineOferta { get => _cineOfera; set => Set(value, ref _cineOfera); }

        /* HashSet es una colección */
        public ObservableCollection<SalaCine> SalaCine { get; set; }

        /* Propieda de navegación */
        private CineDetalle _cineDetalle;
        public CineDetalle CineDetalle { get => _cineDetalle; set => Set(value, ref _cineDetalle); }

        /* Entidad de propiedad */
        private Direccion _direccion;
        public Direccion Direccion { get => _direccion; set => Set(value, ref _direccion); }
    }
}
