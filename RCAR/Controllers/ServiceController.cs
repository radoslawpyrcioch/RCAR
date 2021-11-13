using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using RCAR.Domain.Entities;
using RCAR.Helper;
using RCAR.Models.ServiceVM;
using RCAR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Controllers
{
    [Authorize]
    public class ServiceController : Controller
    {
        private readonly IAttachmentService _attachmentService;
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService, IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
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
        public async Task<IActionResult> Create([Bind(include: "ServiceCreateVM")] ServiceIndexVM model)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            if (ModelState.IsValid)
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
            if (ModelState.IsValid)
            {
                var result = await _serviceService.RemoveServiceAsync(id);
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
                var result = await _serviceService.BackServiceAsync(id);
                if (result)
                    return RedirectToAction("Index");
                ModelState.AddModelError("", "Niestety nie udało się dokonać zmian");
            }
            return View();
        }

        public async Task<IActionResult> ChangeStatus(int id, string status)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            if (ModelState.IsValid)
            {
                var result = await _serviceService.ChangeStatusAsync(currentUserId, id, status);
                if (result)
                    return RedirectToAction("Index");
                ModelState.AddModelError("", "Nie udało się zmienić statusu.");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var model = await _serviceService.DetailServiceAsync(id, currentUserId);
            if (model.PaymentRecords.Count() > 0)
            {
                decimal totalAmount = 0;
                var count = 0;
                _serviceService.CountPayment(model.PaymentRecords, ref count, ref totalAmount);
                ViewBag.TotalCount = count;
                ViewBag.TotalInvoiceAmount = totalAmount;
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var model = await _serviceService.GetServiceForEditAsync(id, currentUserId);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ServiceEditVM model)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            if (ModelState.IsValid)
            {
                var result = await _serviceService.EditServiceAsync(model);
                if (result)
                    return RedirectToAction("Index");
                ModelState.AddModelError("", "Niestety nie udało się dokonać zmian.");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadAttachment()
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var fileContents = await _attachmentService.GenerateDoneServiceListAttachmentAsync(currentUserId);
            var fileName = string.Format("SpisWykonanychSerwisow_{0}.pdf", DateTime.Today.ToShortDateString());

            return File(fileContents, "spisWykonanychSerwisow/pdf", fileName);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadInvoice(int id)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var fileContents = await _attachmentService.GenerateServiceInvoice(id, currentUserId);
            var fileName = string.Format("FakturaZaSerwis_{0}.pdf", DateTime.Today.ToShortDateString());

            return File(fileContents, "application/pdf", fileName);
        }

        [HttpPost]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;

            if (ModelState.IsValid)
            {
                await _serviceService.ImportExcelServiceFromFile(file, currentUserId);
                return RedirectToAction("Index", "Service");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
