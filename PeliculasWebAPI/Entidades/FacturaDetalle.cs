﻿using Microsoft.EntityFrameworkCore;

namespace PeliculasWebAPI.Entidades {
    public class FacturaDetalle {
        public int Id { get; set; }
        public int FacturaId { get; set; }
        public string Producto { get; set; }

        [Precision(18, 2)]
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        [Precision(18, 2)]
        public decimal Total { get; set; }
    }
}
