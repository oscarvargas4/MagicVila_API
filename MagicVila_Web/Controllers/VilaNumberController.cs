using AutoMapper;
using MagicVila_Web.Models;
using MagicVila_Web.Models.Dto;
using MagicVila_Web.Services;
using MagicVila_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVila_Web.Controllers
{
    public class VilaNumberController : Controller
    {
        private readonly IVilaNumberService _vilaNumberService;
        private readonly IMapper _mapper;
        public VilaNumberController(IVilaNumberService vilaNumberService, IMapper mapper)
        {
            _vilaNumberService = vilaNumberService;
            _mapper = mapper;
        }
        public async Task<IActionResult> IndexVilaNumber()
        {
            List<VilaNumberDto> list = new();
            var response = await _vilaNumberService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VilaNumberDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
    }
}
