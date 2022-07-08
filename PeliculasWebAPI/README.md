# Resumen de la sección 11: EF Core y ASP.NET Core
___

1. __Tiempo de vida de los servicios.__
2. __Instanciando el DbContext en un Singleton.__
3. __Programación Asíncrona.__
4. __Reciclando el DbContext.__

#### Tiempo de vida de los servicios

Cuando se habla de servicios nos referimos a interfaces o clases registradas en el sistema de inyección de dependencias. 

Nuestro `ApplicationDbContext` lo registramos como un servicio utilizando la función `AddDbContext`.

    builder.Services AddDbContext<ApplicationDBContext>();

No todos los servicios son iguales, pues estos se diferencia por su tiempo de vida, existen 3 tipos de vida para un servicio.

- Transient. Una nueva instancia del servicio, es decir, es el menor tiempo de vida posible de un servicio. 

- Scoped. Es cuando se nos da la misma instancia dentro del contexto `http`.

- Singleton. Es cuando se le da la misma instancia a todos los usuarios de un servicio. 

El tiempo de vida del `DbContext` por defecto es `Scoped`, de esa manera un usuario solo va trabajar con un único `DbContext` por petición `http`, lo que no va afectar las operaciones en memoria con otros usuarios. 


#### Instanciando el DbContext en un Singleton

 Creamos un singleton que instancie al DbContext, es decir, `Singleton.cs` el cual creará un contexto artificial. 

 ![singleton](/PeliculasWebAPI/images/singleton.png)

 Instanciamos el servicio en nuestro `Program.cs`.

    builder.Services.AddSingleton<Singleton>();

#### Programación Asíncrona

Es buena práctica utilizar programación asíncrona cuando realizamos operaciones I/O en ASP.NET Core. 

Una operación I/O es cuando nuestro sistema se comunica con otros sistemas, por ejemplo, es cuando nuestra aplicación se comunica con una base de datos y esta operación tiene un tiempo de espera mientras transcurre ese tiempo al usar la programación asíncrona el hilo que realizó la petición es liberado y esta puede hacer otras tareas. 

En programación web esto es útil, usar programación asíncrona para dar escabilidad a nuetra aplicación, esto se le conoce como escalabilidad vertical. 

Es decir, se debe preferir esto:

    [HttpGet]
    public async Task<IEnumerable<Genero>> Get() {
        return await context.Generos.ToListAsync();
    }

Que esto: 

    [HttpGet]
    public IEnumerable<Genero> Get() {
        return context.Generos.ToList();
    }

#### Reciclando el DbContext

El DbContext es un objeto rápido de instancia y eliminar, podemos reutilizar dichas instancias por toda nuestra aplicación. 

Pero el `Dbcontext` para a ser entonces un servicio Singleton y no podemos inyectar servicios en el `ApplicationDbContex`.

#### Factoría de DbContexts

En ocasiones es posible que queramos instanciar el `DbContext` manualmente y al mismo tiempo tener su configuración centralizada en el archivo `Program.cs`. 

Para eso podemos hacer uso del método `AddDbContextFactory<>();`, el cual la funcionalidad de ese método es permitir registrar una factoría a partir de la cual vamos a poder instanciar nuestro `DbContext` manualmente utilizando la configuración en `Program.cs`.

Aunque también se puede hacer uso del `AddPooledDbContextFactory<>();` el cual se usa de la misma manera que el método anterior. 

#### Blazor Server

Normalmente, las aplicaciones web son sin estado, así dos peticiones `http` reciben dos aplicacionnes `DbContext`. 

Blazor Server es un framework con estado, lo que recomienda Microsoft es que utilicemos una instancia del `DbContext` por operación, lo que nos conviene utilizar en Blazor la factoría de contextos. 