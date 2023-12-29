using AutoMapper;
using MagicVila_VilaAPI.Logging;
using MagicVila_VilaAPI.Models;
using MagicVila_VilaAPI.Models.Dto;
using MagicVila_VilaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVila_VilaAPI.Controllers
{
    // [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/VilaNumberAPI")]
    [ApiController]
    [ApiVersion("2.0")]
    public class VilaNumberAPIV2Controller : ControllerBase
    {
        private readonly ILogging _logger;
        private readonly IVilaNumberRepository _dbVilaNumber;
        private readonly IVilaRepository _dbVila;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public VilaNumberAPIV2Controller(ILogging logger, IVilaNumberRepository dbVilaNumber, IVilaRepository dbVila, IMapper mapper)
        {
            _logger = logger;
            _dbVilaNumber = dbVilaNumber;
            _dbVila = dbVila;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet]
        //[MapToApiVersion("2.0")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

    }
}
