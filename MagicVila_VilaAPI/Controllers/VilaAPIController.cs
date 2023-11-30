using AutoMapper;
using MagicVila_VilaAPI.Data;
using MagicVila_VilaAPI.Logging;
using MagicVila_VilaAPI.Models;
using MagicVila_VilaAPI.Models.Dto;
using MagicVila_VilaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

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
        protected APIResponse _response;

        public VilaAPIController(ILogging logger, IVilaRepository dbVila, IMapper mapper)
        {
            _logger = logger;
            _dbVila = dbVila;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<APIResponse>> GetVilas()
        {
            try
            {
                _logger.Log("Getting all vilas", "");
                IEnumerable<Vila> vilaList = await _dbVila.GetAllAsync();
                _response.Result = _mapper.Map<List<VilaDto>>(vilaList);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetVila")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        // another way: [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVila(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.Log($"Vila with id: {id} was not found", "error");
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var vila = await _dbVila.GetAsync(u => u.Id == id);
                if (vila == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _logger.Log($"Getting vila with id: {id}", "");
                _response.Result = _mapper.Map<VilaDto>(vila);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<APIResponse>> CreateVila([FromBody] VilaCreateDto createDto)
        {
            try
            {
                if (await _dbVila.GetAsync(u => u.Name.ToLower() == createDto.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Vila already exists!");
                    return BadRequest(ModelState);
                }
                if (createDto == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);

                }
                Vila vila = _mapper.Map<Vila>(createDto);
                await _dbVila.CreateAsync(vila);
                _response.Result = _mapper.Map<VilaDto>(vila);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVila", new { id = vila.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteVila")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> DeleteVila(int id)
        {
            try
            {
                if (id == 0) {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var vila = await _dbVila.GetAsync(u => u.Id == id);
                if (vila == null) {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _dbVila.RemoveAsync(vila);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateVila")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> UpdateVila(int id, [FromBody] VilaUpdateDto updateVila)
        {
            try
            {
                if (updateVila == null || id != updateVila.Id)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var vila = await _dbVila.GetAsync(u => u.Id == id, tracked: false);
                if (vila == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                Vila model = _mapper.Map<Vila>(updateVila);
                await _dbVila.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                return Ok(_response); ;
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVila")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> UpdatePartialVila(int id, JsonPatchDocument<VilaUpdateDto> patchDto)
        {
            try
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
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return NoContent();
        }

    }
}
