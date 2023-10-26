using MagicVila_VilaAPI.Models.Dto.VilaDto;

namespace MagicVila_VilaAPI.Data
{
    public static class VilaStore
    {
        public static List<VilaDto> vilaList = new List<VilaDto>{
                new VilaDto{Id=1,Name="Pool View",Occupancy=4,Sqft=100},
                new VilaDto{Id=2,Name="Hotel View",Occupancy=3,Sqft=300}
            };
    }
}
