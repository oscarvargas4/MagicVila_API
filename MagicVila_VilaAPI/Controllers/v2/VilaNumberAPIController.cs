using AutoMapper;
using MagicVila_VilaAPI.Logging;
using MagicVila_VilaAPI.Models;
using MagicVila_VilaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace MagicVila_VilaAPI.Controllers.v2
{
    // [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/VilaNumberAPI")]
    [ApiController]
    [ApiVersion("2.0")]
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
        //[MapToApiVersion("2.0")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

    }
}
