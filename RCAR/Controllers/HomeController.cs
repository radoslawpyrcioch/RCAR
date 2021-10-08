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
            var currentUserId = User.Claims.ElementAt(0).Value;

            var modelService = await _homeService.GetAllServiceAsync(currentUserId);
            if (modelService.Count() > 0)
            {
                var allService = 0;
                var acceptedService = 0;
                var startedService = 0;
                var doneService = 0;
                var backService = 0;
                _homeService.CountService(modelService, ref allService, ref acceptedService, ref startedService, ref doneService, ref backService);
                ViewBag.TotalServiceCount = allService;
                ViewBag.AcceptedServiceCount = acceptedService;
                ViewBag.StartedServiceCount = startedService;
                ViewBag.DoneServiceCount = doneService;
                ViewBag.BackServiceCount = backService;
            }

            var modelMember = await _homeService.GetAllMeberAsync(currentUserId);
            if (modelMember.Count() > 0)
            {
                var allMember = 0;
                var activeMember = 0;
                var inactiveMember = 0;
                var backMember = 0;
                _homeService.CountMember(modelMember, ref allMember, ref activeMember, ref inactiveMember, ref backMember);
                ViewBag.TotalMemberCount = allMember;
                ViewBag.ActiveMemberCount = activeMember;
                ViewBag.InactiveMemberCount = inactiveMember;
                ViewBag.BackMemberCount = backMember;
            }
            return View();
        }

    }
}
