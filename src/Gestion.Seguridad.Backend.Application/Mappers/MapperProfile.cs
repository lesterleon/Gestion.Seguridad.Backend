using AutoMapper;
using Gestion.Seguridad.Backend.Domain.DTOs.Usuario;
using Gestion.Seguridad.Backend.Domain.Entities;

namespace Gestion.Seguridad.Backend.Application.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CrearUsuarioDto, Usuario>().ReverseMap();
            CreateMap<EditarUsuarioDto, Usuario>().ReverseMap();
            CreateMap<ListarUsuarioDto, Usuario>().ReverseMap();
            CreateMap<ObtenerUsuarioDto, Usuario>().ReverseMap();
        }
    }
}
