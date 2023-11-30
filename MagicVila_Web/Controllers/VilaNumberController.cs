using AutoMapper;
using MagicVila_Web.Models;
using MagicVila_Web.Models.Dto;
using MagicVila_Web.Models.ViewModel;
using MagicVila_Web.Services;
using MagicVila_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace MagicVila_Web.Controllers
{
    public class VilaNumberController : Controller
    {
        private readonly IVilaNumberService _vilaNumberService;
        private readonly IVilaService _vilaService;
        private readonly IMapper _mapper;
        public VilaNumberController(IVilaNumberService vilaNumberService, IVilaService vilaService, IMapper mapper)
        {
            _vilaNumberService = vilaNumberService;
            _vilaService = vilaService;
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

        public async Task<IActionResult> CreateVilaNumber()
        {
            VilaNumberCreateViewModel vilaNumberViewModel = new();
            var response = await _vilaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                vilaNumberViewModel.VilaList = JsonConvert.DeserializeObject<List<VilaDto>>(Convert.ToString(response.Result)).Select(i => new SelectListItem {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
            }
            return View(vilaNumberViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilaNumber(VilaNumberCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _vilaNumberService.CreateAsync<APIResponse>(model.VilaNumber);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVilaNumber));
                }
            }

            var resp = await _vilaService.GetAllAsync<APIResponse>();
            if (resp != null && resp.IsSuccess)
            {
                model.VilaList = JsonConvert.DeserializeObject<List<VilaDto>>(Convert.ToString(resp.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
            }
            return View(model);
        }

        //public async Task<IActionResult> UpdateVila(int vilaId)
        //{
        //    var response = await _vilaNumberService.GetAsync<APIResponse>(vilaId);
        //    if (response != null && response.IsSuccess)
        //    {
        //        VilaNumberDto model = JsonConvert.DeserializeObject<VilaNumberDto>(Convert.ToString(response.Result));
        //        return View(_mapper.Map<VilaUpdateDto>(model));
        //    }
        //    return NotFound();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> UpdateVila(VilaNumberUpdateDto model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var response = await _vilaNumberService.UpdateAsync<APIResponse>(model);
        //        if (response != null && response.IsSuccess)
        //        {
        //            return RedirectToAction(nameof(IndexVilaNumber));
        //        }
        //    }
        //    return View(model);
        //}

        //public async Task<IActionResult> DeleteVila(int vilaId)
        //{
        //    var response = await _vilaNumberService.GetAsync<APIResponse>(vilaId);
        //    if (response != null && response.IsSuccess)
        //    {
        //        VilaNumberDto model = JsonConvert.DeserializeObject<VilaNumberDto>(Convert.ToString(response.Result));
        //        return View(model);
        //    }
        //    return NotFound();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteVila(VilaNumberDto model)
        //{
        //    var response = await _vilaNumberService.DeleteAsync<APIResponse>(model.Id);
        //    if (response != null && response.IsSuccess)
        //    {
        //        return RedirectToAction(nameof(IndexVilaNumber));
        //    }
        //    return View(model);
        //}
    }
}
