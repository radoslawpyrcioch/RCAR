using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RCAR.Models.MemberVM;
using RCAR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var model = new MemberIndexVM()
            {
                Members = await _memberService.GetAllMemberAsync(currentUserId)
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind(include: "MemberCreateVM")]MemberIndexVM model)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            if(ModelState.IsValid)
            {
                var result = await _memberService.CreateMemberAsync(model.MemberCreateVM, currentUserId);
                if (result)
                    return RedirectToAction("Index");
                ModelState.AddModelError("", "Członek o podanym numerze już istnieje.");
            }
            model.Members = await _memberService.GetAllMemberAsync(currentUserId);
            return View("Index", model);
        }
    }
}
