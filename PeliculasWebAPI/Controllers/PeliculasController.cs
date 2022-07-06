using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasWebAPI.DTOs;
using PeliculasWebAPI.Entidades;
using PeliculasWebAPI.Entidades.SinLlaves;

namespace PeliculasWebAPI.Controllers {
    [ApiController]
    [Route("api/peliculas")]
    public class PeliculasController : ControllerBase {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public PeliculasController(ApplicationDBContext context, IMapper mapper) {
            this.context = context;
            this.mapper = mapper;
        }

        /* Eager Loading */
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PeliculaDTO>> Get(int id) {
            var pelicula = await context.Peliculas
                                        /* Eager Loading Cargando Data Relacionada */
                                        .Include(p => p.Generos
                                                       /* Ordena de Menor a Mayor por nombre */
                                                       .OrderByDescending(ord => ord.Nombre)
                                                )
                                        .Include(s => s.SalasCines)
                                            .ThenInclude(s => s.Cine)
                                        .Include(a => a.PeliculasActores
                                                       /* Filtra por año de nac mayor a 1980 */
                                                       .Where(a => a.Actor.FechaNac.Value.Year >= 1980) 
                                                )
                                            .ThenInclude(a => a.Actor)
                                        .FirstOrDefaultAsync(p => p.Id == id);
            if (pelicula is null) {
                return NotFound();
            }

            var peliculaDTO = mapper.Map<PeliculaDTO>(pelicula);

            /* Para evitar cines repetidos */
            peliculaDTO.Cines = peliculaDTO.Cines.DistinctBy(c => c.Id).ToList();

            return peliculaDTO;
        }

        [HttpGet("projectto/{id:int}")]
        public async Task<ActionResult<PeliculaDTO>> GetProjectTo(int id) {
            var pelicula = await context.Peliculas
                                        .ProjectTo<PeliculaDTO>(mapper.ConfigurationProvider)
                                        .FirstOrDefaultAsync(p => p.Id == id);
            if (pelicula is null) {
                return NotFound();
            }

            /* Para evitar cines repetidos */
            pelicula.Cines = pelicula.Cines
                                     .DistinctBy(c => c.Id)
                                     .ToList();

            return pelicula;
        }

        /* Select Loading */
        [HttpGet("cargadoSelectivo/{id:int}")]
        public async Task<ActionResult> GetSelectivo(int id) {
            var pelicula = await context.Peliculas
                                        .Select(p => new {
                                            Id      = p.Id,
                                            Titulo  = p.Titulo,
                                            Generos = p.Generos
                                                       .OrderByDescending(g => g.Nombre)
                                                       .Select(g => g.Nombre)
                                                       .ToList(),
                                           NumActor = p.PeliculasActores.Count(),
                                           NumCines = p.SalasCines
                                                       .Select(s => s.CineId)
                                                       .Distinct()
                                                       .Count()
                                        })
                                        .FirstOrDefaultAsync(p => p.Id == id);

            if (pelicula is null) {
                return NotFound();
            }

            return Ok(pelicula);
        }

        /* Explicit Loading */
        [HttpGet("cargadoExplicito/{id:int}")]
        public async Task<ActionResult<PeliculaDTO>> GetExplicito(int id) {
            var pelicula = await context.Peliculas
                                        .AsTracking()
                                        .FirstOrDefaultAsync(p => p.Id == id);

            await context.Entry(pelicula).Collection(g => g.Generos).LoadAsync();
            
            if(pelicula is null) {
                return NotFound();
            }

            var peliculaDTO = mapper.Map<PeliculaDTO>(pelicula);

            return peliculaDTO;
        }

        /* Lazy Loading */
        [HttpGet("lazyLoading/{id:int}")]
        public async Task<ActionResult<PeliculaDTO>> GetLazyLoading(int id) {
            var pelicula = await context.Peliculas
                                        .AsTracking()
                                        .FirstOrDefaultAsync(p => p.Id == id);

            if(pelicula is null) {
                return NotFound();
            }

            var peliculaDTO = mapper.Map<PeliculaDTO>(pelicula);
            peliculaDTO.Cines = peliculaDTO.Cines
                                           .DistinctBy(c => c.Id)
                                           .ToList();

            return peliculaDTO;
        }

        /* Group By */
        [HttpGet("agrupadasPorEstreno")]
        public async Task<ActionResult> GetAgrupadasCartelera() {
            var peliculasGroup = await context.Peliculas
                                              .GroupBy(p => p.Cartelera)
                                              .Select(g => new {
                                                  Cartelera = g.Key,
                                                  NumGurupo = g.Count(),
                                                  Peliculas = g.ToList()
                                              })
                                              .ToListAsync();

            return Ok(peliculasGroup);
        }

        /* Group By */
        [HttpGet("agrupadasPorGenero")]
        public async Task<ActionResult> GetAgrupadasGenero() {
            var peliculasGroupGen = await context.Peliculas
                                                 .GroupBy(p => p.Generos.Count())
                                                 .Select(g => new {
                                                     Conteo = g.Key,
                                                     Titulos = g.Select(t => t.Titulo),
                                                     Generos = g.Select(x => x.Generos)
                                                                .SelectMany(gen => gen)
                                                                .Select(gen => gen.Nombre)
                                                                .Distinct()
                                                 })
                                                 .ToListAsync();
            return Ok(peliculasGroupGen);
        }

        /* Filtro por parametro - Ejecucion Diferida */
        [HttpGet("filtro")]
        public async Task<ActionResult<List<PeliculaDTO>>> Filtro([FromQuery] PeliculaFiltroDTO peliculaFiltroDTO) {
            var peliculasQuery = context.Peliculas
                                        .AsQueryable();

            if (!string.IsNullOrEmpty(peliculaFiltroDTO.Titulo)) {
                peliculasQuery = peliculasQuery.Where(p => p.Titulo
                                                            .Contains(peliculaFiltroDTO.Titulo)
                                                     );
            }

            if (peliculaFiltroDTO.Cartelera) {
                peliculasQuery = peliculasQuery.Where(p => p.Cartelera);
            }

            if (peliculaFiltroDTO.ProxEstrenos) {
                var hoy = DateTime.Today;
                peliculasQuery = peliculasQuery.Where(p => p.FechaEstreno > hoy);
            }

            if (peliculaFiltroDTO.GeneroId != 0) {
                peliculasQuery = peliculasQuery.Where(p => p.Generos
                                                            .Select(g => g.Identificador)
                                                            .Contains(peliculaFiltroDTO.GeneroId)
                                                     );
            }   

            var pelicula = await peliculasQuery.Include(i => i.Generos).ToListAsync();
            
            return mapper.Map<List<PeliculaDTO>>(pelicula);
        }

        [HttpPost]
        public async Task<ActionResult> Post(PeliculaCreacionDTO peliculaCreacionDTO) { 
            var pelicula = mapper.Map<Pelicula>(peliculaCreacionDTO);

            /* Generos de consulta sin modificacion por eso del Unchanged */
            pelicula.Generos.ForEach(g => context.Entry(g).State = EntityState.Unchanged);
            pelicula.SalasCines.ForEach(s => context.Entry(s).State = EntityState.Unchanged);

            if(pelicula.PeliculasActores is not null) {
                for (int i = 0; i < pelicula.PeliculasActores.Count; i++) {
                    pelicula.PeliculasActores[i].Orden = i + 1;
                }  
            }

            context.Add(pelicula);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("peliculasConteos")]
        public async Task<ActionResult<IEnumerable<PeliculaConteos>>> GetPeliculasConteos() {
            return await context.PeliculasConteos.ToListAsync();
        }

        [HttpGet("peliculasConteos/{id:int}")]
        public async Task<ActionResult<PeliculaConteos>> GetPeliculasConteos(int id) {
            var result = await context.PeliculaConteo(id)
                                      .FirstOrDefaultAsync();

            if (result is null) {
                return NotFound();
            }

            return result;
        }
    }
}
