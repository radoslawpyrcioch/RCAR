using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RCAR.Models;
using RCAR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> News()
        {

            //dodaj inne county, zmien model i czy trzeba dac z serwisu model czy mozna to zrobic w home?

            var currentUserId = User.Claims.ElementAt(0).Value;
            var model = await _homeService.GetAllServiceAsync(currentUserId);
            if (model.Count() > 0)
            {
                var allService = 0;
                var acceptedService = 0;
                var startedService = 0;
                var doneService = 0;
                _homeService.CountService(model, ref allService, ref acceptedService, ref startedService, ref doneService);
                ViewBag.TotalServiceCount = allService;
                ViewBag.AcceptedServiceCount = acceptedService;
                ViewBag.StartedServiceCount = startedService;
                ViewBag.DoneServiceCount = doneService;
            }
            return View();
        }

    }
}
