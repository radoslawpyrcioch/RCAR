using Castle.Core.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RCAR.Domain.Entities;
using RCAR.Extension;
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

        public async Task<IActionResult> Delete(string id)
        {
            var user = (User)await _adminPanelService.GetUserByEmailAsync(id);
            var result = await _adminPanelService.DeleteUserAsync(user);
            if (result.Succeeded)
            {
                _logger.LogInformation("Delete user: {0}", id);
                return RedirectToAction("Index", "Admin");
            }
            _logger.LogError("Delete user: {0} errors: {1}", id, result.Errors);
            return RedirectToAction("Index", "Message", new { Message = IdMessage.AdminDeleteAccountError });
        }
    }
}
