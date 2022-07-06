using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasWebAPI.Entidades;

namespace PeliculasWebAPI.Controllers {
    [ApiController]
    [Route("api/personas")]
    public class PersonasController : ControllerBase {
        private readonly ApplicationDBContext context;

        public PersonasController(ApplicationDBContext context) {
            this.context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Persona>> Get(int id) {
            return await context.Personas
                                .Include(p => p.MensajesEnviados)
                                .Include(p => p.MensajesRecibidos)
                                .FirstOrDefaultAsync(p => p.Id == id);
        }    
    }
}
