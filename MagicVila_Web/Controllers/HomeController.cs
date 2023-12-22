using AutoMapper;
using MagicVila_Utility;
using MagicVila_Web.Models;
using MagicVila_Web.Models.Dto;
using MagicVila_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MagicVila_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVilaService _vilaService;
        private readonly IMapper _mapper;
        public HomeController(IVilaService vilaService, IMapper mapper)
        {
            _vilaService = vilaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<VilaDto> list = new();
            var response = await _vilaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VilaDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
    }
}