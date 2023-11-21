using AutoMapper;
using MagicVila_Web.Models.Dto;

namespace MagicVila_Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<VilaDto, VilaCreateDto>().ReverseMap();
            CreateMap<VilaDto, VilaUpdateDto>().ReverseMap();

            CreateMap<VilaNumberDto, VilaNumberCreateDto>().ReverseMap();
            CreateMap<VilaNumberDto, VilaNumberUpdateDto>().ReverseMap();
        }
    }
}
