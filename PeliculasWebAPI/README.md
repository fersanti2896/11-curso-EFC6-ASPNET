# Resumen de la sección 11: EF Core y ASP.NET Core
___

1. __Tiempo de vida de los servicios.__
2. __Instanciando el DbContext en un Singleton.__
3. __Programación Asíncrona.__

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
