using MagicVila_VilaAPI.Models.Dto;

namespace MagicVila_VilaAPI.Data
{
    public static class VilaStore
    {
        public static List<VilaDto> vilaList = new List<VilaDto>{
                new VilaDto{Id=1,Name="Pool View"},
                new VilaDto{Id=2,Name="Hotel View"}
            };
    }
}
