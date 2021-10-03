using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public UserController(IUserService userService)
        {
            _userService = userService;
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
    }
}
