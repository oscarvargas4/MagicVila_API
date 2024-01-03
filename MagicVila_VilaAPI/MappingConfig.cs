using AutoMapper;
using MagicVila_VilaAPI.Models;
using MagicVila_VilaAPI.Models.Dto;

namespace MagicVila_VilaAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Vila, VilaDto>().ReverseMap();
            CreateMap<Vila, VilaCreateDto>().ReverseMap();
            CreateMap<Vila, VilaUpdateDto>().ReverseMap();

            CreateMap<VilaNumber, VilaNumberDto>().ReverseMap();
            CreateMap<VilaNumber, VilaNumberCreateDto>().ReverseMap();
            CreateMap<VilaNumber, VilaNumberUpdateDto>().ReverseMap();
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
        }
    }
}
