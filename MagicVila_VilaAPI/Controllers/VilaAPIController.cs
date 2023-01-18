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
        public ActionResult<IEnumerable<VilaDto>> GetVilas()
        {
            _logger.Log("Getting all vilas", "");
            return Ok(_db.Vilas.ToList());
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
                _logger.Log($"Vila with id: {id} was not found", "error");
                return BadRequest();
            }
            var vila = _db.Vilas.FirstOrDefault(u => u.Id == id);
            if (vila == null) return NotFound();
            _logger.Log($"Getting vila with id: {id}","");
            return Ok(vila);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<VilaDto> CreateVila([FromBody] VilaDto vilaDto)
        {
            if (_db.Vilas.FirstOrDefault(u => u.Name.ToLower() == vilaDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Vila already exists!");
                return BadRequest(ModelState);
            }
            if (vilaDto == null) return BadRequest(vilaDto);
            if (vilaDto.Id > 0) return BadRequest(vilaDto);
            Vila model = new Vila()
            {
                Amenity= vilaDto.Amenity,
                Details = vilaDto.Details,
                Id = vilaDto.Id,
                ImageUrl = vilaDto.ImageUrl,
                Name = vilaDto.Name,
                Occupancy = vilaDto.Occupancy,
                Rate = vilaDto.Rate,
                Sqft = vilaDto.Sqft
            };
            _db.Vilas.Add(model);
            _db.SaveChanges();
            return CreatedAtRoute("GetVila", new { id = vilaDto.Id }, vilaDto);
        }

        [HttpDelete("{id:int}", Name = "DeleteVila")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteVila(int id)
        {
            if (id == 0) return BadRequest();
            var vila = _db.Vilas.FirstOrDefault(u => u.Id == id);
            if (vila == null) return NotFound();
            _db.Vilas.Remove(vila);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "DeleteVila")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateVila(int id, [FromBody] VilaDto vilaDto)
        {
            if (vilaDto == null | id != vilaDto.Id )
            {
                return BadRequest();
            }
            var vila = _db.Vilas.AsNoTracking().FirstOrDefault(u => u.Id == id);
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
            _db.SaveChanges();

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
            var vila = _db.Vilas.AsNoTracking().FirstOrDefault(u => u.Id == id);
            if (vila == null) return NotFound();
            VilaDto modelDto = new VilaDto()
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
            _db.SaveChanges();
            return NoContent();
        }

    }
}
