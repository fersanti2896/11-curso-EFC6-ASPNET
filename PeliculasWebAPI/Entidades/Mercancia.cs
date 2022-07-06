namespace PeliculasWebAPI.Entidades {
    public class Mercancia : Producto {
        public bool DisponibleInventario { get; set; }
        public double Peso { get; set; }
        public double Volumen { get; set; }
        public bool EsRopa { get; set; }
        public bool EsColeccionable { get; set; }
    }
}
