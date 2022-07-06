using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasWebAPI.Entidades;
using PeliculasWebAPI.Entidades.Funciones;

namespace PeliculasWebAPI.Controllers {
    [ApiController]
    [Route("api/facturas")]
    public class FacturaController : ControllerBase {
        private readonly ApplicationDBContext context;
        private readonly ILogger<FacturaController> logger;

        public FacturaController(ApplicationDBContext context, ILogger<FacturaController> logger) {
            this.context = context;
            this.logger = logger;
        }

        [HttpGet("ObtenerFactura")]
        public async Task<ActionResult<Factura>> ObtenerFactura(int id) {
            var factura = await context.Facturas
                                       .FirstOrDefaultAsync(f => f.Id == id);

            if (factura is null) {
                return NotFound();
            }

            return factura;
        }

        [HttpPut("ActualizarFactura")]
        public async Task<ActionResult> ActualizarFactura(Factura factura) {
            context.Update(factura);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("Func_Esca")]
        public async Task<IActionResult> GetFuncEsc() {
            var facturas = await context.Facturas
                                        .Select(f => new {
                                            Id       = f.Id,
                                            Total    = context.FacturaDetalleSuma(f.Id),
                                            Promedio = Escalares.FacturaDetallePromedio(f.Id)
                                        })
                                        .OrderByDescending(f => context.FacturaDetalleSuma(f.Id))
                                        .ToListAsync();

            return Ok(facturas);
        }

        [HttpPost]
        public async Task<ActionResult> Post() {
            using var transaccion = await context.Database.BeginTransactionAsync();
            
            try {
                var factura = new Factura() {
                    FechaCreacion = DateTime.Now
                };

                context.Add(factura);
                await context.SaveChangesAsync();

                /* Simula Error */
                //throw new ApplicationException("Esto es una prueba");

                var facturaDetalle = new List<FacturaDetalle>() {
                                        new FacturaDetalle() {
                                            Producto  = "Producto A",
                                            Precio    = 123,
                                            FacturaId = factura.Id
                                        },
                                        new FacturaDetalle() {
                                            Producto  = "Producto B",
                                            Precio    = 456,
                                            FacturaId = factura.Id
                                        }
                                      };

                context.AddRange(facturaDetalle);
                await context.SaveChangesAsync();
                await transaccion.CommitAsync();

                return Ok("Todo bien");
            } catch (Exception e) {
                return BadRequest("Hubo un error, se revirtió la operación");
            }  
        }

        [HttpGet("{id:int}/detalle")]
        public async Task<ActionResult<IEnumerable<FacturaDetalle>>> GetDetalle(int id) { 
            return await context.FacturaDetalles
                                .Where(f => f.FacturaId == id)
                                .OrderByDescending(f => f.Total)
                                .ToListAsync();
        }

        [HttpPost("Concurrencia_Fila")]
        public async Task<ActionResult> ConcurrenciaFila() {
            var facturaId = 2;

            var factura   = await context.Facturas
                                         .AsTracking()
                                         .FirstOrDefaultAsync(f => f.Id == facturaId);

            factura.FechaCreacion = DateTime.Now;

            await context.Database
                         .ExecuteSqlInterpolatedAsync(@$"UPDATE Facturas 
                                                         SET FechaCreacion = GetDate()
                                                         WHERE Id = {facturaId}" );

            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("Concurrencia_Fila_Manejando_Error")]
        public async Task<ActionResult> ConcurrenciaFilaManejandoError() {
            var facturaId = 2;

            try {
                // Fernando
                var factura = await context.Facturas
                                           .AsTracking()
                                           .FirstOrDefaultAsync(f => f.Id == facturaId);

                factura.FechaCreacion = DateTime.Now.AddDays(-10);

                // Claudia
                await context.Database
                             .ExecuteSqlInterpolatedAsync(@$"UPDATE Facturas 
                                                             SET FechaCreacion = GetDate()
                                                             WHERE Id = {facturaId}");
                // Fernando
                await context.SaveChangesAsync();

                return Ok();
            } catch (DbUpdateConcurrencyException ex) {
                var entry = ex.Entries.Single();

                var facturaActual = await context.Facturas
                                                 .AsNoTracking()
                                                 .FirstOrDefaultAsync(f => f.Id == facturaId);

                /* Itera todas las propiedades de la entidad */
                foreach (var propiedad in entry.Metadata.GetProperties()) {
                    var valorIntentado = entry.Property(propiedad.Name)
                                              .CurrentValue;
                    var valorDBActual  = context.Entry(facturaActual)
                                                .Property(propiedad.Name)
                                                .CurrentValue;
                    var valorAnterior  = entry.Property(propiedad.Name)
                                              .OriginalValue;

                    if (valorDBActual.ToString() == valorIntentado.ToString()) {
                        // Esta propiedad no fue modificada
                        continue;
                    }

                    logger.LogInformation($"--- Propiedad {propiedad.Name} ---");
                    logger.LogInformation($"Valor intentado {valorIntentado}");
                    logger.LogInformation($"Valor en la base de datos {valorDBActual}");
                    logger.LogInformation($"Valor anterior {valorAnterior}");

                    // hacer algo...
                }

                return BadRequest("El registro no pudo ser actualizado pues fue modificado por otra persona!");
            }

        }
    }
}
