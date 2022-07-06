using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasWebAPI.Entidades;

namespace PeliculasWebAPI.Controllers {
    [ApiController]
    [Route("api/productos")]
    public class ProductosController : ControllerBase {
        private readonly ApplicationDBContext context;

        public ProductosController(ApplicationDBContext context) {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> Get() {
            return await context.Productos.ToListAsync();
        }

        [HttpGet("mercancia")]
        public async Task<ActionResult<IEnumerable<Mercancia>>> GetMercancia() { 
            return await context.Set<Mercancia>().ToListAsync();
        }

        [HttpGet("alquileres")]
        public async Task<ActionResult<IEnumerable<PeliculaAlquilable>>> GetAlquileres(){
            return await context.Set<PeliculaAlquilable>().ToListAsync();
        }
    }
}
