using AutoMapper;
using MagicVila_Web.Models;
using MagicVila_Web.Models.Dto.VilaDto;
using MagicVila_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MagicVila_Web.Controllers
{
    public class VilaController : Controller
    {
        private readonly IVilaService _vilaService;
        private readonly IMapper _mapper;
        public VilaController(IVilaService vilaService, IMapper mapper)
        {
            _vilaService = vilaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexVila()
        {
            List<VilaDto> list = new();
            var response = await _vilaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VilaDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> CreateVila()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVila(VilaCreateDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _vilaService.CreateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVila));
                }
            }
            return View(model);
        }
    }
}
