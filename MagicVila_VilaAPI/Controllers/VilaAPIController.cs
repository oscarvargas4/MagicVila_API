using MagicVila_VilaAPI.Data;
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
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<VilaDto>> GetVilas()
        {
            return Ok(VilaStore.vilaList);
        }

        [HttpGet("{id:int}", Name = "GetVila")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        // another way: [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<VilaDto> GetVila(int id)
        {
            if (id == 0) return BadRequest();
            var vila = VilaStore.vilaList.FirstOrDefault(u => u.Id == id);
            if (vila == null) return NotFound();
            return Ok(vila);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<VilaDto> CreateVila([FromBody] VilaDto vilaDto)
        {
            if (vilaDto == null) return BadRequest(vilaDto);
            if (vilaDto.Id > 0) return BadRequest(vilaDto);
            vilaDto.Id = VilaStore.vilaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            VilaStore.vilaList.Add(vilaDto);
            return CreatedAtRoute("GetVila", new { id = vilaDto.Id }, vilaDto);
        }
    }
}
