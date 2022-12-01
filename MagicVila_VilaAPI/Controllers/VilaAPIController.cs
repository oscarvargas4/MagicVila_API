using MagicVila_VilaAPI.Data;
using MagicVila_VilaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVila_VilaAPI.Controllers
{
    // [Route("api/[controller]")]
    [Route("api/VilaAPI")]
    [ApiController]
    public class VilaAPIController : ControllerBase
    {
        private readonly ILogger<VilaAPIController> _logger;

        public VilaAPIController(ILogger<VilaAPIController> logger)
        {
            this._logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<VilaDto>> GetVilas()
        {
            _logger.LogInformation("Getting all vilas");
            return Ok(VilaStore.vilaList);
        }

        [HttpGet("{id:int}", Name = "GetVila")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        // another way: [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<VilaDto> GetVila(int id)
        {
            if (id == 0) 
            {
                _logger.LogError($"Vila with id: {id} was not found");
                return BadRequest();
            } 
            var vila = VilaStore.vilaList.FirstOrDefault(u => u.Id == id);
            if (vila == null) return NotFound();
            _logger.LogInformation($"Getting vila with id: {id}");
            return Ok(vila);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<VilaDto> CreateVila([FromBody] VilaDto vilaDto)
        {
            if (VilaStore.vilaList.FirstOrDefault(u => u.Name.ToLower() == vilaDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Vila already exists!");
                return BadRequest(ModelState);
            }
            if (vilaDto == null) return BadRequest(vilaDto);
            if (vilaDto.Id > 0) return BadRequest(vilaDto);
            vilaDto.Id = VilaStore.vilaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            VilaStore.vilaList.Add(vilaDto);
            return CreatedAtRoute("GetVila", new { id = vilaDto.Id }, vilaDto);
        }

        [HttpDelete("{id:int}", Name = "DeleteVila")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteVila(int id)
        {
            if (id == 0) return BadRequest();
            var vila = VilaStore.vilaList.FirstOrDefault(u => u.Id == id);
            if (vila == null) return NotFound();
            VilaStore.vilaList.Remove(vila);
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "DeleteVila")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateVila(int id, [FromBody] VilaDto vilaDto)
        {
            if (vilaDto == null | id != vilaDto.Id)
            {
                return BadRequest();
            }
            var vila = VilaStore.vilaList.FirstOrDefault(u => u.Id == id);
            vila.Name = vilaDto.Name;
            vila.Sqft = vilaDto.Sqft;
            vila.Occupancy = vilaDto.Occupancy;
            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVila")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePartialVila(int id, JsonPatchDocument<VilaDto> patchDto)
        {
            // https://jsonpatch.com/
            if (patchDto == null | id == 0) return BadRequest();
            var vila = VilaStore.vilaList.FirstOrDefault(u => u.Id == id);
            if (vila == null) return NotFound();
            patchDto.ApplyTo(vila, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }

    }
}
