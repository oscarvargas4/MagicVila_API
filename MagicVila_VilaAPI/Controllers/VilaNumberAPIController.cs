using AutoMapper;
using MagicVila_VilaAPI.Data;
using MagicVila_VilaAPI.Logging;
using MagicVila_VilaAPI.Models;
using MagicVila_VilaAPI.Models.Dto.VilaNumberDto;
using MagicVila_VilaAPI.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVila_VilaAPI.Controllers
{
    // [Route("api/[controller]")]
    [Route("api/VilaNumberAPI")]
    [ApiController]
    public class VilaNumberAPIController : ControllerBase
    {
        private readonly ILogging _logger;
        private readonly IVilaNumberRepository _dbVilaNumber;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public VilaNumberAPIController(ILogging logger, IVilaNumberRepository dbVilaNumber, IMapper mapper)
        {
            _logger = logger;
            _dbVilaNumber = dbVilaNumber;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<APIResponse>> GetVilaNumbers()
        {
            try
            {
                _logger.Log("Getting all vilas", "");
                IEnumerable<VilaNumber> vilaList = await _dbVilaNumber.GetAllAsync();
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
            return _response;
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
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<APIResponse>> CreateVilaNumber([FromBody] VilaNumberCreateDto createDto)
        {
            try
            {
                if (await _dbVilaNumber.GetAsync(u => u.VilaNo == createDto.VilaNo) != null)
                {
                    ModelState.AddModelError("CustomError", "VilaNumber already exists!");
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
                return CreatedAtRoute("GetVila", new { id = VilaNumber.VilaNo }, _response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteVilaNumber")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> DeleteVilaNumber(int id)
        {
            try
            {
                if (id == 0) {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var VilaNumber = await _dbVilaNumber.GetAsync(u => u.VilaNo == id);
                if (VilaNumber == null) {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _dbVilaNumber.RemoveAsync(VilaNumber);
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

        [HttpPut("{id:int}", Name = "UpdateVilaNumber")]
        [ProducesResponseType(204)]
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
                var VilaNumber = await _dbVilaNumber.GetAsync(u => u.VilaNo == id, tracked: false);
                if (VilaNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                VilaNumber model = _mapper.Map<VilaNumber>(updateVila);
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

        [HttpPatch("{id:int}", Name = "UpdatePartialVilaNumber")]
        [ProducesResponseType(204)]
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
