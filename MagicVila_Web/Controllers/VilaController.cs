using AutoMapper;
using MagicVila_Web.Models;
using MagicVila_Web.Models.Dto;
using MagicVila_Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateVila()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVila(VilaCreateDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _vilaService.CreateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Vila created successfully";
                    return RedirectToAction(nameof(IndexVila));
                }
            }
            TempData["error"] = "Error encountered";
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateVila(int vilaId)
        {
            var response = await _vilaService.GetAsync<APIResponse>(vilaId);
            if (response != null && response.IsSuccess)
            {
                VilaDto model = JsonConvert.DeserializeObject<VilaDto>(Convert.ToString(response.Result));
                return View(_mapper.Map<VilaUpdateDto>(model));
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVila(VilaUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _vilaService.UpdateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Vila updated successfully";
                    return RedirectToAction(nameof(IndexVila));
                }
            }
            TempData["error"] = "Error encountered";
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteVila(int vilaId)
        {
            var response = await _vilaService.GetAsync<APIResponse>(vilaId);
            if (response != null && response.IsSuccess)
            {
                VilaDto model = JsonConvert.DeserializeObject<VilaDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVila(VilaDto model)
        {
            var response = await _vilaService.DeleteAsync<APIResponse>(model.Id);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Vila deleted successfully";
                return RedirectToAction(nameof(IndexVila));
            }
            TempData["error"] = "Error encountered";
            return View(model);
        }
    }
}
