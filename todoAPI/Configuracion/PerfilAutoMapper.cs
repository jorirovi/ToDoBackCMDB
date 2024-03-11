using AutoMapper;
using todoAPI.Comunes.Clases.Contracts;
using todoAPI.models;

namespace todoAPI.Mapper
{
    public class PerfilAutoMapper : Profile
    {
        public PerfilAutoMapper()
        {
            CreateMap<TodoEntity, TodoContract>().ReverseMap();
            CreateMap<TodoEntity,TodoDTOContract>().ReverseMap();
            CreateMap<UsuarioEntity, UsuarioContract>().ReverseMap();
            CreateMap<UsuarioEntity, UsuarioTDOContract>().ReverseMap();
            CreateMap<TipoProductoEntity, TipoProductoContract>().ReverseMap();
            CreateMap<TipoProductoEntity, TipoProductoDTOContract>().ReverseMap();
        }
        
    }
}