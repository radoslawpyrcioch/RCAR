using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RCAR.Extension;
using RCAR.Models.UserVM;
using RCAR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var model = await _userService.GetUserEmailByIdAsync(currentUserId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword([Bind(include: "OldPassword, NewPassword, ConfirmNewPassword")]UserManageVM model)
        {
            if (ModelState.IsValid)
            {
                var currentuserId = User.Claims.ElementAt(0).Value;
                var result = await _userService.ChangePasswordAsync(model, currentuserId);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Message", new { Message = IdMessage.ChangePasswordSucces });
                result.Errors.ToList().ForEach(e => ModelState.AddModelError("ChangePassword", e.Description));
            }
            return View("Index", model);
        }

        public async Task<IActionResult> Delete([Bind(include: "Email")] UserManageVM model)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var result = await _userService.DeleteUserAsync(model.Email, currentUserId);
            if (result.Succeeded)
            {
                _logger.LogInformation("User with email: {0} delete account.", model.Email);
                return RedirectToAction("Index", "Message", new { Message = IdMessage.AdminDeleteAccountSucces });
            }
            _logger.LogError("User with email: {0} delete account errors: {1}", model.Email, result.Errors);
            return View(model);
        }
    }
}
