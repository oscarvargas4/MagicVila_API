using AutoMapper;
using MagicVila_VilaAPI.Data;
using MagicVila_VilaAPI.Logging;
using MagicVila_VilaAPI.Models;
using MagicVila_VilaAPI.Models.Dto;
using MagicVila_VilaAPI.Repository.IRepository;
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
        private readonly IVilaRepository _dbVila;
        private readonly IMapper _mapper;

        public VilaAPIController(ILogging logger, IVilaRepository dbVila, IMapper mapper)
        {
            _logger = logger;
            _dbVila = dbVila;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<VilaDto>>> GetVilas()
        {
            _logger.Log("Getting all vilas", "");
            IEnumerable<Vila> vilaList = await _dbVila.GetAllAsync();
            return Ok(_mapper.Map<List<VilaDto>>(vilaList));
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
            var vila = await _dbVila.GetAsync(u => u.Id == id);
            if (vila == null) return NotFound();
            _logger.Log($"Getting vila with id: {id}","");
            return Ok(_mapper.Map<VilaDto>(vila));
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<VilaDto>> CreateVila([FromBody] VilaCreateDto createDto)
        {
            if (await _dbVila.GetAsync(u => u.Name.ToLower() == createDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Vila already exists!");
                return BadRequest(ModelState);
            }
            if (createDto == null) return BadRequest(createDto);
            //if (vilaDto.Id > 0) return BadRequest(vilaDto);
            Vila model = _mapper.Map<Vila>(createDto); // This line replace what old model does (commented code below)
            //Vila model = new Vila()
            //{
            //    Amenity= createDto.Amenity,
            //    Details = createDto.Details,
            //    //Id = createDto.Id,
            //    ImageUrl = createDto.ImageUrl,
            //    Name = createDto.Name,
            //    Occupancy = createDto.Occupancy,
            //    Rate = createDto.Rate,
            //    Sqft = createDto.Sqft
            //};
            await _dbVila.CreateAsync(model);
            return CreatedAtRoute("GetVila", new { id = model.Id }, model);
        }

        [HttpDelete("{id:int}", Name = "DeleteVila")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteVila(int id)
        {
            if (id == 0) return BadRequest();
            var vila = await _dbVila.GetAsync(u => u.Id == id);
            if (vila == null) return NotFound();
            await _dbVila.RemoveAsync(vila);
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateVila")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateVila(int id, [FromBody] VilaUpdateDto updateVila)
        {
            if (updateVila == null || id != updateVila.Id )
            {
                return BadRequest();
            }
            var vila = await _dbVila.GetAsync(u => u.Id == id, tracked: false);
            if (vila == null) return NotFound();
            Vila model = _mapper.Map<Vila>(updateVila);
            await _dbVila.UpdateAsync(model);

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
            var vila = await _dbVila.GetAsync(u => u.Id == id, tracked: false);
            if (vila == null) return NotFound();
            VilaUpdateDto modelDto = _mapper.Map<VilaUpdateDto>(vila);
            patchDto.ApplyTo(modelDto, ModelState);
            Vila model = _mapper.Map<Vila>(modelDto);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _dbVila.UpdateAsync(model);
            return NoContent();
        }

    }
}
