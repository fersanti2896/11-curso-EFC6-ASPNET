using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq.Expressions;

namespace PeliculasWebAPI.Entidades.Conversiones {
    public class MonedaSimboloConvert : ValueConverter<Moneda, string>
    {
        public MonedaSimboloConvert() : base(
            valor => MapeoMonedaString(valor),
            valor => MapeoStringMoneda(valor)
        ) { }

        private static string MapeoMonedaString(Moneda moneda) {
            return moneda switch {
                Moneda.PesoMex => "$",
                Moneda.DolarEUA => "$$",
                Moneda.Euro => "€",
                _ => ""
            };
        }

        private static Moneda MapeoStringMoneda(string valor) {
            return valor switch {
                "$" => Moneda.PesoMex,
                "$$" => Moneda.DolarEUA,
                "€" => Moneda.Euro,
                _ => Moneda.Desconocida
            };
        }
    }
}
