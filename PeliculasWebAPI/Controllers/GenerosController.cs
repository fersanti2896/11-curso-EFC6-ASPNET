using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PeliculasWebAPI.DTOs;
using PeliculasWebAPI.Entidades;

namespace PeliculasWebAPI.Controllers {
    [ApiController]
    [Route("api/generos")]
    public class GenerosController : ControllerBase {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public GenerosController(ApplicationDBContext context, IMapper mapper) {
            this.context = context;
            this.mapper  = mapper;
        }

        /* endpoint */
        [HttpGet]
        public async Task<IEnumerable<Genero>> Get() {
            context.Logs.Add(new Log {
                                Id = Guid.NewGuid(),
                                Mensaje = "Ejecutando el método GenerosController.Get()"
                        });

            await context.SaveChangesAsync();

            return await context.Generos
                                .OrderBy(g => g.Nombre)
                                .ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Genero>> PorId(int id) {
            var genero = await context.Generos
                                      .AsTracking()
                                      .FirstOrDefaultAsync(p => p.Identificador == id);

            /* Querie Arbitrario Forma 1 */
            /**var genero = await context.Generos
                                      .FromSqlRaw("SELECT * FROM Generos WHERE Identificador = {0}", id)
                                      .IgnoreQueryFilters()
                                      .FirstOrDefaultAsync(); **/

            /* Querie Arbitrario Forma 2 */
            /* var genero = await context.Generos
                                      .FromSqlInterpolated($"SELECT * FROM Generos WHERE Identificador = {id}")
                                      .IgnoreQueryFilters()
                                      .FirstOrDefaultAsync(); */

            if (genero is null) {
                return NotFound();
            }

            /* Accediendo a la fecha de creacion del genero */
            var fechaCreacion = context.Entry(genero)
                                       .Property<DateTime>("FechaCreacion")
                                       .CurrentValue;

            var periodStar    = context.Entry(genero)
                                       .Property<DateTime>("PeriodStart")
                                       .CurrentValue;

            var periodEnd     = context.Entry(genero)
                                       .Property<DateTime>("PeriodEnd")
                                       .CurrentValue;

            return Ok(new { 
                        Id     = genero.Identificador,
                        Nombre = genero.Nombre,
                        fechaCreacion, 
                        periodStar,
                        periodEnd
                   });
        }

        /* Filtración con Ordenamiento */
        [HttpGet("filtrar")]
        public async Task<IEnumerable<Genero>> FiltrarPorFrase(string nombre) {
            return await context.Generos
                                .Where(g => g.Nombre.Contains(nombre))
                                .OrderBy(g => g.Nombre)
                                .ToListAsync();
        }

        [HttpGet("paginacion")]
        public async Task<ActionResult<IEnumerable<Genero>>> GetPacionacion(int page = 1) {
            var registrosPagina = 2;
            var genero = await context.Generos
                                      .Skip((page - 1) * registrosPagina) /* Salta el primer registro */
                                      .Take(registrosPagina) /* Toma dos registros */
                                      .ToListAsync();
            if (genero is null) {
                NotFound();
            }

            return genero;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Genero genero) {
            var existeGeNom = await context.Generos
                                           .AnyAsync(g => g.Nombre == genero.Nombre);

            if (existeGeNom) {
                return BadRequest("Ya existe un Genero con ese nombre: " + genero.Nombre);
            }

            /* Guarda el status */
            //context.Add(genero);
            //context.Entry(genero).State = EntityState.Added;
            await context.Database
                         .ExecuteSqlInterpolatedAsync($@"INSERT INTO Generos(Nombre)
                                                         VALUES({ genero.Nombre })");

            /* Agrega el estado genero a la tabla Generos */
            await context.SaveChangesAsync();
            
            return Ok();
        }

        [HttpPost("variosGeneros")]
        public async Task<ActionResult> Post(Genero[] generos) { 
            context.AddRange(generos);

            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("agregar2")]
        public async Task<ActionResult> Agregar2(int id) {
            var genero = await context.Generos
                                      .AsTracking()
                                      .FirstOrDefaultAsync(g => g.Identificador == id);

            if (genero is null) {
                return NotFound();
            }

            genero.Nombre += " 2";
            await context.SaveChangesAsync();

            return Ok();
        }

        /* Borrado normal */
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id) {
            var genero = await context.Generos
                                      .FirstOrDefaultAsync(gen => gen.Identificador == id);

            if (genero is null) {
                return NotFound();
            }

            context.Remove(genero);
            await context.SaveChangesAsync();

            return Ok();
        }

        /* Borrado lógico, se refiere a que no remueve el registro de la tabla
           sino solo se marca como un status de borrado */
        [HttpDelete("borradoLog/{id:int}")]
        public async Task<ActionResult> DeleteSuave(int id) {
            var genero = await context.Generos
                                      .AsTracking()
                                      .FirstOrDefaultAsync(gen => gen.Identificador == id);

            if (genero is null) {
                return NotFound();
            }

            genero.EstaBorrado = true;
            await context.SaveChangesAsync();

            return Ok();
        }

        /* Restaura un elemento borrado de manera temporal */
        [HttpPost("restaurar/{id:int}")]
        public async Task<ActionResult> PostRestaurar(int id) {
            var genero = await context.Generos
                                      .IgnoreQueryFilters()
                                      .AsTracking()
                                      .FirstOrDefaultAsync(gen => gen.Identificador == id);

            if (genero is null) {
                return NotFound();
            }

            genero.EstaBorrado = false;
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(GeneroActualizacionDTO generoActualizacionDTO) {
            var genero = mapper.Map<Genero>(generoActualizacionDTO);

            context.Update(genero);
            context.Entry(genero)
                   .Property(g => g.Nombre)
                   .OriginalValue = generoActualizacionDTO.Nombre_Original;

            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("ProcAlm/{id:int}")]
        public async Task<ActionResult<Genero>> GetPA(int id) {
            var generos = context.Generos
                                 .FromSqlInterpolated($"EXEC Generos_ObtenerPorId { id }")
                                 .IgnoreQueryFilters()
                                 .AsAsyncEnumerable();

            await foreach (var genero in generos) {
                return genero;
            }

            return NotFound();
        }

        [HttpPost("Proc_Alm")]
        public async Task<ActionResult> PostPA(Genero genero) {
            var existeGeNom = await context.Generos
                                           .AnyAsync(g => g.Nombre == genero.Nombre);

            if (existeGeNom){
                return BadRequest("Ya existe un Genero con ese nombre: " + genero.Nombre);
            }

            var outputId = new SqlParameter();
            outputId.ParameterName = "@id";
            outputId.SqlDbType = System.Data.SqlDbType.Int;
            outputId.Direction = System.Data.ParameterDirection.Output;

            await context.Database
                         .ExecuteSqlRawAsync("EXEC Generos_Insertar @Nombre = {0}, @Id = {1} OUTPUT",
                                             genero.Nombre, outputId);

            var id = (int)outputId.Value;

            return Ok(id);
        }

        [HttpPost("concurrency_token")]
        public async Task<ActionResult> ConcurrencyToken() {
            var generoId = 1;

            // Fernando lee el registro de la BD.
            var genero    = await context.Generos
                                         .AsTracking()
                                         .FirstOrDefaultAsync(g => g.Identificador == generoId);
            genero.Nombre = "Fernando estuvo aquí";

            // Claudia actualiza el registro en la BD.
            await context.Database
                         .ExecuteSqlInterpolatedAsync(@$"UPDATE Generos 
                                                         SET Nombre = 'Claudia estuvo aquí' 
                                                         WHERE Identificador = {generoId}");
            // Fernando intenta actualizar.
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("ModificaVariasVeces")]
        public async Task<ActionResult> ModificarVariasVeces() {
            var id     = 3;
            var genero = await context.Generos
                                      .AsTracking()
                                      .FirstOrDefaultAsync(g => g.Identificador == id);

            genero.Nombre = "Comedia 2";
            await context.SaveChangesAsync();
            await Task.Delay(2000);

            genero.Nombre = "Comedia 3";
            await context.SaveChangesAsync();
            await Task.Delay(2000);

            genero.Nombre = "Comedia 4";
            await context.SaveChangesAsync();
            await Task.Delay(2000);

            genero.Nombre = "Comedia 5";
            await context.SaveChangesAsync();
            await Task.Delay(2000);

            genero.Nombre = "Comedia 6";
            await context.SaveChangesAsync();
            await Task.Delay(2000);

            genero.Nombre = "Comedia Actual";
            await context.SaveChangesAsync();
            await Task.Delay(2000);

            return Ok();
        }

        [HttpGet("TemporalAll/{id:int}")]
        public async Task<ActionResult> GetTemporalAll(int id) {
            var generos = await context.Generos
                                       .TemporalAll()
                                       .AsTracking()
                                       .Where(g => g.Identificador == id)
                                       .Select(g => new {
                                            Id          = g.Identificador,
                                            Nombre      = g.Nombre,
                                            PeriodStart = EF.Property<DateTime>(g, "PeriodStart"),
                                            PeriodEnd   = EF.Property<DateTime>(g, "PeriodEnd")
                                       })
                                       .ToListAsync();

            return Ok(generos);
        }

        [HttpGet("TemporalAsOf/{id:int}")]
        public async Task<ActionResult> GetTemporalAsOf(int id, DateTime fecha) {
            var genero = await context.Generos
                                      .TemporalAsOf(fecha)
                                      .AsTracking()
                                      .Where(g => g.Identificador == id)
                                      .Select(g => new {
                                            Id          = g.Identificador,
                                            Nombre      = g.Nombre,
                                            PeriodStart = EF.Property<DateTime>(g, "PeriodStart"),
                                            PeriodEnd   = EF.Property<DateTime>(g, "PeriodEnd")
                                      })
                                      .FirstOrDefaultAsync();

            return Ok(genero);
        }

        [HttpGet("TemporalFromTo/{id:int}")]
        public async Task<ActionResult> GetTemporalFromTo(int id, DateTime desde, DateTime hasta) {
            var generos = await context.Generos
                                       .TemporalFromTo(desde, hasta)
                                       .AsTracking()
                                       .Where(g => g.Identificador == id)
                                       .Select(g => new {
                                            Id          = g.Identificador,
                                            Nombre      = g.Nombre,
                                            PeriodStart = EF.Property<DateTime>(g, "PeriodStart"),
                                            PeriodEnd   = EF.Property<DateTime>(g, "PeriodEnd")
                                       })
                                       .ToListAsync();

            return Ok(generos);
        }

        [HttpGet("TemporalContainedIn/{id:int}")]
        public async Task<ActionResult> GetTemporalContainedIn(int id, DateTime desde, DateTime hasta) {
            var generos = await context.Generos
                                       .TemporalContainedIn(desde, hasta)
                                       .AsTracking()
                                       .Where(g => g.Identificador == id)
                                       .Select(g => new {
                                            Id          = g.Identificador,
                                            Nombre      = g.Nombre,
                                            PeriodStart = EF.Property<DateTime>(g, "PeriodStart"),
                                            PeriodEnd   = EF.Property<DateTime>(g, "PeriodEnd")
                                       })
                                       .ToListAsync();

            return Ok(generos);
        }

        [HttpGet("TemporalBetween/{id:int}")]
        public async Task<ActionResult> GetTemporalBetween(int id, DateTime desde, DateTime hasta) {
            var generos = await context.Generos
                                       .TemporalBetween(desde, hasta)
                                       .AsTracking()
                                       .Where(g => g.Identificador == id)
                                       .Select(g => new{
                                            Id          = g.Identificador,
                                            Nombre      = g.Nombre,
                                            PeriodStart = EF.Property<DateTime>(g, "PeriodStart"),
                                            PeriodEnd   = EF.Property<DateTime>(g, "PeriodEnd")
                                       })
                                       .ToListAsync();

            return Ok(generos);
        }

        [HttpPost("Restaurar_Borrado/{id:int}")]
        public async Task<ActionResult> RestaurarBorrado(int id, DateTime fecha) {
            var genero = await context.Generos
                                      .TemporalAsOf(fecha)
                                      .AsTracking()
                                      .IgnoreQueryFilters()
                                      .FirstOrDefaultAsync(g => g.Identificador == id);

            if (genero is null) {
                return NotFound();
            }

            /* Restauramos el registro con el id con un query arbitrario */
            try {
                await context.Database
                             .ExecuteSqlInterpolatedAsync($@"
                                SET IDENTITY_INSERT Generos ON;
                                INSERT INTO Generos (Identificador, Nombre)
                                VALUES ({ genero.Identificador }, { genero.Nombre })
                                SET IDENTITY_INSERT Generos OFF;"
                             );
            } finally {
                await context.Database
                             .ExecuteSqlRawAsync("SET IDENTITY_INSERT Generos OFF;");
            }

            return Ok();
        }
    }
}
