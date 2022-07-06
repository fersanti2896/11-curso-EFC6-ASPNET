using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasWebAPI.Entidades;

namespace PeliculasWebAPI.Controllers {
    [ApiController]
    [Route("api/pagos")]
    public class PagosController : ControllerBase {
        private readonly ApplicationDBContext context;

        public PagosController(ApplicationDBContext context) {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pago>>> Get() {
            return await context.Pagos.ToListAsync();
        }

        [HttpGet("tarjetas")]
        public async Task<ActionResult<IEnumerable<PagoTarjeta>>> GetTarjetas() { 
            return await context.Pagos
                                .OfType<PagoTarjeta>()
                                .ToListAsync();
        }

        [HttpGet("paypal")]
        public async Task<ActionResult<IEnumerable<PagoPaypal>>> GetPaypal() { 
            return await context.Pagos
                                .OfType<PagoPaypal>()
                                .ToListAsync();
        }
    }
}
