using AutoMapper;
using MagicVila_VilaAPI.Models;
using MagicVila_VilaAPI.Models.Dto.VilaDto;

namespace MagicVila_VilaAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Vila, VilaDto>();
            CreateMap<VilaDto, Vila>();

            CreateMap<Vila, VilaCreateDto>();
            CreateMap<VilaCreateDto, Vila>();

            CreateMap<Vila, VilaUpdateDto>();
            CreateMap<VilaUpdateDto, Vila>();
        }
    }
}
