using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using PeliculasWebAPI.DTOs;
using PeliculasWebAPI.Entidades;
using PeliculasWebAPI.Entidades.SinLlaves;
using PeliculasWebAPI.Servicios;
using System.Collections.ObjectModel;

namespace PeliculasWebAPI.Controllers {
    [ApiController]
    [Route("api/cines")]
    public class CinesController : ControllerBase {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;
        private readonly IActualizadorObservableCollectionService actualizadorObservableCollectionService;

        public CinesController(ApplicationDBContext context, 
            IMapper mapper,
            IActualizadorObservableCollectionService actualizadorObservableCollectionService) {
            this.context = context;
            this.mapper = mapper;
            this.actualizadorObservableCollectionService = actualizadorObservableCollectionService;
        }

        public object NtsGeometryService { get; private set; }

        [HttpGet]
        public async Task<IEnumerable<CineDTO>> Get() {
            return await context.Cines
                                .ProjectTo<CineDTO>(mapper.ConfigurationProvider)
                                .ToListAsync();
        }

        [HttpGet("cercanos")]
        public async Task<ActionResult> Get(double lat, double lng) {
            var geoFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var miUbicacion = geoFactory.CreatePoint(new Coordinate(lat, lng));
            var distanciaMax = 2000;

            var cines = await context.Cines
                                     .OrderBy(c => c.Ubicacion.Distance(miUbicacion))
                                     .Where(c => c.Ubicacion.IsWithinDistance(miUbicacion, distanciaMax))
                                     .Select(c => new {
                                         Nombre = c.Nombre,
                                         Distancia = Math.Round(c.Ubicacion.Distance(miUbicacion))
                                     })
                                     .ToListAsync();

            return Ok(cines);
        }

        [HttpPost]
        public async Task<ActionResult> Post() {
            var geoFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var ubicacionCine = geoFactory.CreatePoint(new Coordinate(-69.896979, 18.476276));

            var cine = new Cine() {
                Nombre = "Cinemark con detalle TableSplitting",
                Ubicacion = ubicacionCine,
                CineDetalle = new CineDetalle() { 
                    Historia    = "Historia...",
                    CodigoEtica = "CodigoEtica...",
                    Misiones    = "Misiones..."
                },
                CineOferta = new CineOferta() {
                    DescuentoPorcentaje = 5,
                    FechaInicio = DateTime.Today,
                    FechaFin = DateTime.Today.AddDays(7)
                },
                SalaCine = new ObservableCollection<SalaCine>() {
                    new SalaCine() {
                        Precio       = 200,
                        Moneda       = Moneda.PesoMex,
                        TipoSalaCine = TipoSalaCine.DosD
                    },
                    new SalaCine() {
                        Precio       = 350,
                        Moneda       = Moneda.DolarEUA,
                        TipoSalaCine = TipoSalaCine.TresD
                    }
                }
            };

            context.Add(cine);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id) { 
            /*var cine = await context.Cines
                                    .AsTracking()
                                    .Include(c => c.SalaCine)
                                    .Include(c => c.CineOferta)
                                    .Include(c => c.CineDetalle)
                                    .FirstOrDefaultAsync(c => c.Id == id); */

            /* Querie Arbitrario */
            var cine = await context.Cines
                                    .FromSqlInterpolated($"SELECT * FROM Cines WHERE Id = {id}")
                                    .Include(c => c.SalaCine)
                                    .Include(c => c.CineOferta)
                                    .Include(c => c.CineDetalle)
                                    .FirstOrDefaultAsync();

            if (cine is null)
                return NotFound();

            cine.Ubicacion = null;
            return Ok(cine);

        }

        [HttpPost("conDTO")]
        public async Task<ActionResult> PostConDTO(CineCreacionDTO cineCreacionDTO) {
            var cine = mapper.Map<Cine>(cineCreacionDTO);
            context.Add(cine);

            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("sinUbicacion")]
        public async Task<IEnumerable<CineSinUbicacion>> GetCinesUbicacion(){
            /* Propiedad set permite crear un DbContext en tiempo real */
            // return await context.Set<CineSinUbicacion>().ToListAsync();
            return await context.CineSinUbicacion.ToListAsync();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(CineCreacionDTO cineCreacionDTO, int id) {
            var cineDB = await context.Cines.AsTracking()
                                                   .Include(c => c.SalaCine)
                                                   .Include(c => c.CineOferta)
                                                   .FirstOrDefaultAsync(c => c.Id == id);

            if (cineDB is null) {
                return NotFound();
            }

            cineDB = mapper.Map(cineCreacionDTO, cineDB);
            actualizadorObservableCollectionService.Actualizar(cineDB.SalaCine, cineCreacionDTO.SalasCines);

            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("cineOferta")]
        public async Task<ActionResult> PutCineOferta(CineOferta cineOferta) {
            context.Update(cineOferta);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id) {
            var cine = await context.Cines
                                    .Include(c => c.SalaCine)
                                    .Include(c => c.CineOferta)
                                    .FirstOrDefaultAsync(x => x.Id == id);

            if(cine is null) {
                return NotFound();
            }

            context.RemoveRange(cine.SalaCine);
            await context.SaveChangesAsync();

            context.Remove(cine);
            await context.SaveChangesAsync();
            
            return Ok();       
        }

        [HttpDelete("opcional({id:int}")]
        public async Task<ActionResult> DeleteOpcional(int id) {
            var cine = await context.Cines
                                    .Include(c => c.CineOferta)
                                    .FirstOrDefaultAsync(x => x.Id == id);

            if(cine is null) {
                return NotFound();
            }

            context.Remove(cine);
            await context.SaveChangesAsync();
            
            return Ok();       
        }
    }
}
