using AutoMapper;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using PeliculasWebAPI.DTOs;
using PeliculasWebAPI.Entidades;

namespace PeliculasWebAPI.Servicios {
    public class AutoMapperProfiles : Profile {
        public AutoMapperProfiles() {
            CreateMap<Actor, ActorDTO>();

            CreateMap<Cine, CineDTO>()
                    .ForMember(dto => dto.Latitud, 
                               ent => ent.MapFrom(prop => prop.Ubicacion.Y))
                    .ForMember(dto => dto.Longitud, 
                               ent => ent.MapFrom(prop => prop.Ubicacion.X));

            CreateMap<Genero, GeneroDTO>();

            /* Sin ProjectTo */
            CreateMap<Pelicula, PeliculaDTO>()
                    .ForMember(dto => dto.Cines, 
                               ent => ent.MapFrom(
                                            prop => prop.SalasCines
                                                        .Select(s => s.Cine)
                                           )
                              )
                    .ForMember(dto => dto.Actores, 
                               ent => ent.MapFrom(
                                            prop => prop.PeliculasActores
                                                        .Select(a => a.Actor)
                                          )
                              );

            /* Con ProjectTo */
            /* CreateMap<Pelicula, PeliculaDTO>()
                    .ForMember(dto => dto.Generos,
                               ent => ent.MapFrom(
                                            prop => prop.Generos
                                                        .OrderByDescending(g => g.Nombre)
                                          )
                              )
                    .ForMember(dto => dto.Cines, 
                               ent => ent.MapFrom(
                                            prop => prop.SalasCines
                                                        .Select(s => s.Cine)
                                           )
                              )
                    .ForMember(dto => dto.Actores, 
                               ent => ent.MapFrom(
                                            prop => prop.PeliculasActores
                                                        .Select(a => a.Actor)
                                          )
                              );*/

            var geoFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            
            CreateMap<CineCreacionDTO, Cine>()
                     .ForMember(ent => ent.SalaCine, opc => opc.Ignore())
                     .ForMember(ent => ent.Ubicacion,
                                dto => dto.MapFrom(
                                                campo => geoFactory.CreatePoint(
                                                    new Coordinate(campo.Longitud, campo.Latitud)))
                                                );

            CreateMap<CineOfertaDTO, CineOferta>();
            CreateMap<SalaCineDTO, SalaCine>();

            CreateMap<PeliculaCreacionDTO, Pelicula>()
                     .ForMember(ent => ent.Generos,
                                dto => dto.MapFrom(
                                                campo => campo.Generos
                                                              .Select(id => new Genero() {
                                                                                Identificador = id 
                                                                            }
                                                               )
                                           )
                      )
                     .ForMember(ent => ent.SalasCines,
                                dto => dto.MapFrom(
                                                campo => campo.SalasCine
                                                              .Select(id => new SalaCine() { 
                                                                                Id = id
                                                                            }
                                                              )
                                           )
                      );

            CreateMap<PeliculaActorCreacionDTO, PeliculaActor>();

            CreateMap<ActorCreacionDTO, Actor>();
            CreateMap<GeneroActualizacionDTO, Genero>();
        }
    }
}
