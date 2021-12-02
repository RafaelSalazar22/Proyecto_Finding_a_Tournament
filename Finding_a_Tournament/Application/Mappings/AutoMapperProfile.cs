
using System.Reflection.Metadata.Ecma335;
using System;
using AutoMapper;
using Finding_a_Tournament.Domain.dto.Responses;
using Finding_a_Tournament.Domain.dto.Requests;
using Finding_a_Tournament.Domain.dto;
using Finding_a_Tournament.Domain.Entities;
using Domain.dto;

namespace Finding_a_Tournament.Application.Mappings
{
      public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Club, ClubsResponse>()
            .ForMember(dest => dest.NombreClub, opt => opt.MapFrom(src => src.NombreClub))
            .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion))
            .ForMember(dest => dest.TelefonoContacto, opt => opt.MapFrom(src => src.TelefonoContacto))
            .ForMember(dest => dest.Diciplina, opt => opt.MapFrom(src => src.ClubServicio == null ? "N/A" : src.ClubServicio.Diciplina));

            CreateMap<ClubsCreateRequest, Club>()
            .ForPath(dest => dest.ClubServicio.Diciplina, opt => opt.MapFrom(src => src.Diciplina))
            .ForPath(dest => dest.ClubServicio.HorarioDiciplina, opt => opt.MapFrom(src => src.HorarioDiciplina))
            .ForPath(dest => dest.ClubServicio.CantidadPer, opt => opt.MapFrom(src => src.CantidadPer))
            .ForPath(dest => dest.ClubServicio.PersHabilidadesDiferentes, opt => opt.MapFrom(src => src.PersHabilidadesDiferentes));      
            CreateMap<Torneo, TorneosResponse>()
            .ForMember(dest => dest.NombreTorneo, opt => opt.MapFrom(src => src.NombreTorneo))
            .ForMember(dest => dest.TipoTorneo, opt => opt.MapFrom(src => src.TipoTorneo))
            .ForMember(dest => dest.CantidadParticipantes, opt => opt.MapFrom(src => src.CantidadParticipantes))
            .ForMember(dest => dest.CantidadEquipos, opt => opt.MapFrom(src => src.CantidadEquipos))
            .ForMember(dest => dest.AcepHabilidadesdistintas, opt => opt.MapFrom(src => src.AcepHabilidadesdistintas));

            CreateMap<TorneoCreateRequest, Torneo>()
            .ForPath(dest => dest.NombreTorneo, opt => opt.MapFrom(src => src.NombreTorneo))
            .ForPath(dest => dest.TipoTorneo, opt => opt.MapFrom(src => src.TipoTorneo))
            .ForPath(dest => dest.CantidadParticipantes, opt => opt.MapFrom(src => src.CantidadParticipantes))
            .ForPath(dest => dest.CantidadEquipos, opt => opt.MapFrom(src => src.CantidadEquipos))
            .ForPath(dest => dest.AcepHabilidadesdistintas, opt => opt.MapFrom(src => src.AcepHabilidadesdistintas));       
        }
    }
}