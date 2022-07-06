using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PeliculasWebAPI;
using PeliculasWebAPI.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeliculasAPIPruebas {
    public class BasePruebas {
        /* Es un método que crea un DbContext para pruebas */
        protected ApplicationDBContext ConstruirContext(string nombreDB) {
            var opc         = new DbContextOptionsBuilder<ApplicationDBContext>()
                                    .UseInMemoryDatabase(nombreDB).Options;

            var serviceUser = new UsuarioService();
            var dbContext   = new ApplicationDBContext(opc, serviceUser, eventosDbContextService: null);

            return dbContext;
        }

        /* Configurando Automapper */
        protected IMapper ConfigurarAutoMapper() {
            var config = new MapperConfiguration(opc => {
                /* AutoMapperProfiles es la clase de configuraciones del mapeo de automapper */
                opc.AddProfile(new AutoMapperProfiles());
            });

            return config.CreateMapper();
        }
    }
}
