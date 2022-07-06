using System.ComponentModel.DataAnnotations.Schema;

namespace PeliculasWebAPI.Entidades {
    public class Actor {
        public int Id { get; set; }
        private string _nombre;
        public string Nombre {
            get { return _nombre; } 
            set {
                _nombre = string.Join(' ', value.Split(' ')
                                .Select(x => x[0].ToString()
                                                 .ToUpper() +
                                             x.Substring(1)
                                              .ToLower()
                                       )
                                 .ToArray()
                                );
                }
        }
        public string Biografia { get; set; }

        //[Column(TypeName = "Date")]
        public DateTime? FechaNac { get; set; }
        public HashSet<PeliculaActor> PeliculasActores { get; set; }
        public string FotoURL { get; set; }

        /* Evitamos mapear la propiedad en la tabla */
        [NotMapped]
        public int? Edad { 
            get {
                if(!FechaNac.HasValue)
                    return null;

                var fechaNac = FechaNac.Value;
                var edad     = DateTime.Today.Year - fechaNac.Year;

                if(new DateTime(DateTime.Today.Year, fechaNac.Month, fechaNac.Day) > DateTime.Today) {
                    edad--;
                }

                return edad;
            }
        }

        /* Entidad de propiedad */
        public Direccion DireccionHogar { get; set; }
        public Direccion BillingAddress { get; set; }
    }
}
