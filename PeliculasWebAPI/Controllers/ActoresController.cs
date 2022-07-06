using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasWebAPI.DTOs;
using PeliculasWebAPI.Entidades;

namespace PeliculasWebAPI.Controllers {
    [ApiController]
    [Route("api/actores")]
    public class ActoresController : ControllerBase {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public ActoresController(ApplicationDBContext context, IMapper mapper) {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ActorDTO>> Get() {
            var actores = await context.Actores
                                /* Con Select devolvemos solo las prop que nos interesan */
                                /* Envés de que mapee a un tipo anonimo, mapea a ActorDTO
                                 * sin el mapper */
                                /* .Select(a => new ActorDTO {
                                    Id     = a.Id,
                                    Nombre = a.Nombre,
                                })*/

                                /* Con Mapper */
                                .ProjectTo<ActorDTO>(mapper.ConfigurationProvider)
                                .ToListAsync();

            return actores;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ActorCreacionDTO actorCreacionDTO) {
            var actor = mapper.Map<Actor>(actorCreacionDTO);
            context.Add(actor);

            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutConectado(ActorCreacionDTO actorCreacionDTO, int id) { 
            var actorDB = await context.Actores
                                       .AsTracking()
                                       .FirstOrDefaultAsync(act => act.Id == id);

            if (actorDB is null) {
                return NotFound();
            }

            /* Automapper nos permite mapear de un objeto a otro */
            actorDB = mapper.Map(actorCreacionDTO, actorDB);

            /* Observa los valores del DbContext respecto a la entidad */
            var entry = context.Entry(actorDB);

            //await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("desconectado/{id:int}")]
        public async Task<ActionResult> PutDesconectado(ActorCreacionDTO actorCreacionDTO, int id) {
            var existActor = await context.Actores
                                    .AnyAsync(act => act.Id == id);

            if (!existActor) { 
                return NoContent();
            }

            var actor = mapper.Map<Actor>(actorCreacionDTO);
            actor.Id = id;

            /* Se actualiza todas las propiedades del objeto actor */
            //context.Update(actor);
            context.Entry(actor).Property(a => a.Nombre)
                                .IsModified = true;
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
