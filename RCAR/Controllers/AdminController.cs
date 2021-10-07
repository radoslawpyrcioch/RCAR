using Castle.Core.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RCAR.Models.AdminPanelVM;
using RCAR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly IAdminPanelService _adminPanelService;
        private readonly IEmailService _emailService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IAdminPanelService adminPanelService, IEmailService emailService, ILogger<AdminController> logger)
        {
            _adminPanelService = adminPanelService;
            _emailService = emailService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new AdminIndexVM()
            {
                Users = await _adminPanelService.GetAllUserAsync()
            };
            return View(model);
        }
    }
}
