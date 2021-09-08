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
        private readonly IAttachmentService _attachmentService;
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService, IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
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

        [HttpGet]
        public async Task<IActionResult> Removed()
        {
            var currenUserId = User.Claims.ElementAt(0).Value;
            var model = new MemberIndexVM()
            {
                Members = await _memberService.GetAllMemberRemovedAsync(currenUserId)
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

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var model = await _memberService.DetailMemberAsync(id, currentUserId);
            if (model.Cars.Count() > 0)
            {
                var count = 0;
                _memberService.CountCars(model.Cars, ref count);
                ViewBag.TotalCountCar = count;
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var model = await _memberService.GetMemberForEditAsync(id, currentUserId);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MemberEditVM model)
        {
            //var currentUserId = User.Claims.ElementAt(0).Value; // it will be use late

            if (ModelState.IsValid)
            {
                var result = await _memberService.EditMemberAsync(model);
                if (result)
                    return RedirectToAction("Index");
                ModelState.AddModelError("", "Niestety nie udało się dokonać zmian");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var result = await _memberService.RemoveServiceAsync(id);
                if (result)
                    return RedirectToAction("Index");
                ModelState.AddModelError("", "Niestety nie udało się dokonać zmian");
            }
            return View();
        }

        public async Task<IActionResult> BackToDraft(int id)
        {
            if (ModelState.IsValid)
            {
                var result = await _memberService.BackToDraftAsync(id);
                if (result)
                    return RedirectToAction("Index");
                ModelState.AddModelError("", "Niestety nie udało się dokonać zmian");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DownloadAttachment()
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var fileContents = await _attachmentService.GenerateActualMemberListAsync(currentUserId);
            var fileName = string.Format("SpisWszystkichCzlonkow_{0}.pdf", DateTime.Today.ToShortDateString());

            return File(fileContents, "SpisWszystkichCzlonkow/pdf", fileName);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadMemberCar(int id)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var fileContents = await _attachmentService.GenerateMemberCars(id, currentUserId);
            var fileName = string.Format("SpisWszystkichSamochodow_{0}.pdf", DateTime.Today.ToShortDateString());

            return File(fileContents, "application/pdf", fileName);
        }

    }
}
