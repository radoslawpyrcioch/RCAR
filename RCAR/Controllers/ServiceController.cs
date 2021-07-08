using Microsoft.AspNetCore.Mvc;
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
    }
}
