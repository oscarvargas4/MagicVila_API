using MagicVila_VilaAPI.Models;
using MagicVila_VilaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MagicVila_VilaAPI.Controllers
{
    // [Route("api/[controller]")]
    [Route("api/VilaAPI")]
    [ApiController]
    public class VilaAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<VilaDto> GetVilas()
        {
            return new List<VilaDto>{
                new VilaDto{Id=1,Name="Pool View"},
                new VilaDto{Id=2,Name="Hotel View"}
            };
        }
    }
}
