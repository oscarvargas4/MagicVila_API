using MagicVila_VilaAPI.Data;
using MagicVila_VilaAPI.Logging;
using MagicVila_VilaAPI.Models;
using MagicVila_VilaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVila_VilaAPI.Controllers
{
    // [Route("api/[controller]")]
    [Route("api/VilaAPI")]
    [ApiController]
    public class VilaAPIController : ControllerBase
    {
        private readonly ILogging _logger;
        private readonly ApplicationDbContext _db;

        public VilaAPIController(ILogging logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<VilaDto>>> GetVilas()
        {
            _logger.Log("Getting all vilas", "");
            return Ok(await _db.Vilas.ToListAsync());
        }

        [HttpGet("{id:int}", Name = "GetVila")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        // another way: [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<VilaDto>> GetVila(int id)
        {
            if (id == 0)
            {
                _logger.Log($"Vila with id: {id} was not found", "error");
                return BadRequest();
            }
            var vila = await _db.Vilas.FirstOrDefaultAsync(u => u.Id == id);
            if (vila == null) return NotFound();
            _logger.Log($"Getting vila with id: {id}","");
            return Ok(vila);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<VilaDto>> CreateVila([FromBody] VilaCreateDto vilaDto)
        {
            if (await _db.Vilas.FirstOrDefaultAsync(u => u.Name.ToLower() == vilaDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Vila already exists!");
                return BadRequest(ModelState);
            }
            if (vilaDto == null) return BadRequest(vilaDto);
            //if (vilaDto.Id > 0) return BadRequest(vilaDto);
            Vila model = new Vila()
            {
                Amenity= vilaDto.Amenity,
                Details = vilaDto.Details,
                //Id = vilaDto.Id,
                ImageUrl = vilaDto.ImageUrl,
                Name = vilaDto.Name,
                Occupancy = vilaDto.Occupancy,
                Rate = vilaDto.Rate,
                Sqft = vilaDto.Sqft
            };
            await _db.Vilas.AddAsync(model);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetVila", new { id = model.Id }, model);
        }

        [HttpDelete("{id:int}", Name = "DeleteVila")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteVila(int id)
        {
            if (id == 0) return BadRequest();
            var vila = await _db.Vilas.FirstOrDefaultAsync(u => u.Id == id);
            if (vila == null) return NotFound();
            _db.Vilas.Remove(vila);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "DeleteVila")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateVila(int id, [FromBody] VilaUpdateDto vilaDto)
        {
            if (vilaDto == null | id != vilaDto.Id )
            {
                return BadRequest();
            }
            var vila = await _db.Vilas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            if (vila == null) return NotFound();
            Vila model = new Vila()
            {
                Amenity = vilaDto.Amenity,
                Details = vilaDto.Details,
                Id = vilaDto.Id,
                ImageUrl = vilaDto.ImageUrl,
                Name = vilaDto.Name,
                Occupancy = vilaDto.Occupancy,
                Rate = vilaDto.Rate,
                Sqft = vilaDto.Sqft
            };
            _db.Vilas.Update(model);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVila")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdatePartialVila(int id, JsonPatchDocument<VilaUpdateDto> patchDto)
        {
            // https://jsonpatch.com/
            if (patchDto == null | id == 0) return BadRequest();
            var vila = await _db.Vilas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            if (vila == null) return NotFound();
            VilaUpdateDto modelDto = new ()
            {
                Amenity = vila.Amenity,
                Details = vila.Details,
                Id = vila.Id,
                ImageUrl = vila.ImageUrl,
                Name = vila.Name,
                Occupancy = vila.Occupancy,
                Rate = vila.Rate,
                Sqft = vila.Sqft
            };
            patchDto.ApplyTo(modelDto, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Vila model = new Vila()
            {
                Amenity = modelDto.Amenity,
                Details = modelDto.Details,
                Id = modelDto.Id,
                ImageUrl = modelDto.ImageUrl,
                Name = modelDto.Name,
                Occupancy = modelDto.Occupancy,
                Rate = modelDto.Rate,
                Sqft = modelDto.Sqft
            };
            _db.Vilas.Update(model);
            await _db.SaveChangesAsync();
            return NoContent();
        }

    }
}
