﻿using Microsoft.AspNetCore.Mvc;
using RCAR.Helper;
using RCAR.Models.ServiceVM;
using RCAR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var currentUserId = User.Claims.ElementAt(0).Value; //user id

            var model = new ServiceIndexVM()
            {
                Services = await _serviceService.GetAllServiceAsync(currentUserId)
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Removed()
        {
            var currentUserId = User.Claims.ElementAt(0).Value;

            var model = new ServiceIndexVM()
            {
                Services = await _serviceService.GetAllServiceRemovedAsync(currentUserId)
            };
            return View(model);
        }

        public IActionResult Create()
        {
            var model = new ServiceCreateVM();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind(include: "ServiceCreateVM")]ServiceIndexVM model)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            if(ModelState.IsValid)
            {
                var result = await _serviceService.CreateServiceAsync(model.ServiceCreateVM, currentUserId);
                if (result)
                    return RedirectToAction("Index");
                ModelState.AddModelError("", "Serwis o podanym numerze już istnieje.");
            }
            model.Services = await _serviceService.GetAllServiceAsync(currentUserId);
            return View("Index", model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _serviceService.RemoveServiceAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var model = await _serviceService.DetailServiceAsync(id, currentUserId);
            return View(model);
        }

    }
}
