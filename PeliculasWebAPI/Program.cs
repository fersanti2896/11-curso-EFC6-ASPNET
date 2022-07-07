using Microsoft.EntityFrameworkCore;
using PeliculasWebAPI;
using PeliculasWebAPI.Servicios;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                .AddJsonOptions(opc => 
                        opc.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
                );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDBContext>(
                    opciones => {
                        opciones.UseSqlServer(
                            connectionString, sqlServer => sqlServer.UseNetTopologySuite()
                        );
                        /* Solo lectura, comportamiento del query */
                        opciones.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                        /* Configurando Lazy Loading */
                        //opciones.UseLazyLoadingProxies();
                    }
                 );

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IEventosDbContextService, EventosDbContextService>();
builder.Services.AddScoped<IActualizadorObservableCollectionService, ActualizadorObservableCollectionService>();
builder.Services.AddSingleton<Singleton>();

/* Agregando AutoMapper */
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// /* Crea un contexto que instancia al DbContext */
// using (var scope = app.Services.CreateScope()) {
//     /* Se tiene una instancia al DbContext */
//     var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
//     /* Ejecuta las migraciones al carga la aplicaci�n */
//     applicationDbContext.Database.Migrate();
// }

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
