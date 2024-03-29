﻿using AutoMapper;
using MagicVila_Utility;
using MagicVila_Web.Models;
using MagicVila_Web.Models.Dto;
using MagicVila_Web.Models.ViewModel;
using MagicVila_Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
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
            var response = await _vilaNumberService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VilaNumberDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateVilaNumber()
        {
            VilaNumberCreateViewModel vilaNumberViewModel = new();
            var response = await _vilaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                vilaNumberViewModel.VilaList = JsonConvert.DeserializeObject<List<VilaDto>>(Convert.ToString(response.Result)).Select(i => new SelectListItem {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
            }
            return View(vilaNumberViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilaNumber(VilaNumberCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _vilaNumberService.CreateAsync<APIResponse>(model.VilaNumber, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Vila Number created successfully";
                    return RedirectToAction(nameof(IndexVilaNumber));
                }
                else
                {
                    if(response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var resp = await _vilaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.VilaList = JsonConvert.DeserializeObject<List<VilaDto>>(Convert.ToString(resp.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
            }
            TempData["error"] = "Error encountered";
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateVilaNumber(int vilaNo)
        {
            VilaNumberUpdateViewModel vilaNumberViewModel = new();
            var response = await _vilaNumberService.GetAsync<APIResponse>(vilaNo, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                VilaNumberDto model = JsonConvert.DeserializeObject<VilaNumberDto>(Convert.ToString(response.Result));
                vilaNumberViewModel.VilaNumber = _mapper.Map<VilaNumberUpdateDto>(model);
            }

            response = await _vilaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                vilaNumberViewModel.VilaList = JsonConvert.DeserializeObject<List<VilaDto>>(Convert.ToString(response.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                return View(vilaNumberViewModel);
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilaNumber(VilaNumberUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _vilaNumberService.UpdateAsync<APIResponse>(model.VilaNumber, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Vila Number updated successfully";
                    return RedirectToAction(nameof(IndexVilaNumber));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var resp = await _vilaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.VilaList = JsonConvert.DeserializeObject<List<VilaDto>>(Convert.ToString(resp.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
            }
            TempData["error"] = "Error encountered";
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteVilaNumber(int vilaNo)
        {
            VilaNumberDeleteViewModel vilaNumberViewModel = new();
            var response = await _vilaNumberService.GetAsync<APIResponse>(vilaNo, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                VilaNumberDto model = JsonConvert.DeserializeObject<VilaNumberDto>(Convert.ToString(response.Result));
                vilaNumberViewModel.VilaNumber = model;
            }

            response = await _vilaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                vilaNumberViewModel.VilaList = JsonConvert.DeserializeObject<List<VilaDto>>(Convert.ToString(response.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                return View(vilaNumberViewModel);
            }

            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVilaNumber(VilaNumberDeleteViewModel model)
        {
            var response = await _vilaNumberService.DeleteAsync<APIResponse>(model.VilaNumber.VilaNo, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {

                TempData["success"] = "Vila Number deleted successfully";
                return RedirectToAction(nameof(IndexVilaNumber));
            }
            TempData["error"] = "Error encountered";
            return View(model);
        }
    }
}
