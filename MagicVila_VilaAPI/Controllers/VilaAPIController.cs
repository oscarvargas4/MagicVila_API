using MagicVila_VilaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MagicVila_VilaAPI.Controllers
{
    [Route("api/VilaAPI")]
    [ApiController]
    public class VilaAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Vila> GetVilas()
        {
            return new List<Vila>{
                new Vila{Id=1,Name="Pool View"},
                new Vila{Id=2,Name="Pool View"}
            };
        }
    }
}
