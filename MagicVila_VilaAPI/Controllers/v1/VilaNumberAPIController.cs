using AutoMapper;
using MagicVila_VilaAPI.Logging;
using MagicVila_VilaAPI.Models;
using MagicVila_VilaAPI.Models.Dto;
using MagicVila_VilaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVila_VilaAPI.Controllers.v1
{
    // [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/VilaNumberAPI")]
    [ApiController]
    [ApiVersion("1.0")]
    public class VilaNumberAPIController : ControllerBase
    {
        private readonly ILogging _logger;
        private readonly IVilaNumberRepository _dbVilaNumber;
        private readonly IVilaRepository _dbVila;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public VilaNumberAPIController(ILogging logger, IVilaNumberRepository dbVilaNumber, IVilaRepository dbVila, IMapper mapper)
        {
            _logger = logger;
            _dbVilaNumber = dbVilaNumber;
            _dbVila = dbVila;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        //[MapToApiVersion("1.0")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<APIResponse>> GetVilaNumbers()
        {
            try
            {
                _logger.Log("Getting all vilas", "");
                IEnumerable<VilaNumber> vilaList = await _dbVilaNumber.GetAllAsync(includeProperties: "Vila");
                _response.Result = _mapper.Map<List<VilaNumberDto>>(vilaList);
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
            return BadRequest(_response);
        }

        [HttpGet("{id:int}", Name = "GetVilaNumber")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<APIResponse>> GetVilaNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.Log($"VilaNumber with id: {id} was not found", "error");
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var VilaNumber = await _dbVilaNumber.GetAsync(u => u.VilaNo == id);
                if (VilaNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _logger.Log($"Getting VilaNumber with id: {id}", "");
                _response.Result = _mapper.Map<VilaNumberDto>(VilaNumber);
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
            return BadRequest(_response);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<APIResponse>> CreateVilaNumber([FromBody] VilaNumberCreateDto createDto)
        {
            try
            {
                if (await _dbVilaNumber.GetAsync(u => u.VilaNo == createDto.VilaNo) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "VilaNumber already exists");
                    return BadRequest(ModelState);
                }

                if (await _dbVila.GetAsync(u => u.Id == createDto.VilaID) == null)
                {
                    ModelState.AddModelError("ErrorMessages", $"No Vila with ID: {createDto.VilaID}");
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);

                }
                VilaNumber VilaNumber = _mapper.Map<VilaNumber>(createDto);
                await _dbVilaNumber.CreateAsync(VilaNumber);
                _response.Result = _mapper.Map<VilaNumberDto>(VilaNumber);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVilaNumber", new { id = VilaNumber.VilaNo }, _response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return BadRequest(_response);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilaNumber")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> DeleteVilaNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var VilaNumber = await _dbVilaNumber.GetAsync(u => u.VilaNo == id);
                if (VilaNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _dbVilaNumber.RemoveAsync(VilaNumber);
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
            return BadRequest(_response);
        }

        [HttpPut("{id:int}", Name = "UpdateVilaNumber")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> UpdateVilaNumber(int id, [FromBody] VilaNumberDto updateVila)
        {
            try
            {
                if (updateVila == null || id != updateVila.VilaNo)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                if (await _dbVila.GetAsync(u => u.Id == updateVila.VilaID) == null)
                {
                    ModelState.AddModelError("ErrorMessages", $"No Vila with ID: {updateVila.VilaID}");
                    return BadRequest(ModelState);
                }

                var VilaNumber = await _dbVilaNumber.GetAsync(u => u.VilaNo == id, tracked: false);
                if (VilaNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                VilaNumber model = _mapper.Map<VilaNumber>(updateVila);
                await _dbVilaNumber.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                return CreatedAtRoute("GetVilaNumber", new { id = VilaNumber.VilaNo }, _response); ;
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return BadRequest(_response);
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilaNumber")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> UpdatePartialVilaNumber(int id, JsonPatchDocument<VilaNumberUpdateDto> patchDto)
        {
            try
            {
                // https://jsonpatch.com/
                if (patchDto == null | id == 0) return BadRequest();
                var VilaNumber = await _dbVilaNumber.GetAsync(u => u.VilaNo == id, tracked: false);
                if (VilaNumber == null) return NotFound();
                VilaNumberUpdateDto modelDto = _mapper.Map<VilaNumberUpdateDto>(VilaNumber);
                patchDto.ApplyTo(modelDto, ModelState);
                VilaNumber model = _mapper.Map<VilaNumber>(modelDto);
                if (!ModelState.IsValid) return BadRequest(ModelState);
                await _dbVilaNumber.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return NoContent();
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

    }
}
