using Microsoft.AspNetCore.Mvc;
using RCAR.Models.PaymentRecordVM;
using RCAR.Models.ServiceVM;
using RCAR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Controllers
{
    public class PaymentRecordController : Controller
    {
        private readonly IPaymentRecordService _paymentRecordService;

        public PaymentRecordController(IPaymentRecordService paymentRecordService)
        {
            _paymentRecordService = paymentRecordService;
        }

        public IActionResult Create()
        {
            var model = new PaymentCreateVM();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PaymentCreateVM paymentCreateVM, int id)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var paymentCreate = await _paymentRecordService.CreatePaymentAsync(paymentCreateVM, id, currentUserId);
            if (paymentCreate)
                return RedirectToAction("Index", "Service");
            else
                return BadRequest("Brak możliwości dodania płatności");
        }

        public IActionResult CreateCar()
        {
            var model = new PaymentCarCreateVM();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar(PaymentCarCreateVM paymentCarCreateVM, int id)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var paymentCreate = await _paymentRecordService.CreateCarPaymentAsync(paymentCarCreateVM, id, currentUserId);
            if (paymentCreate)
                return RedirectToAction("Index", "Member");
            else
                return BadRequest("Brak możliwości dodania płatności");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if(ModelState.IsValid)
            {
                var result = await _paymentRecordService.RemovePaymentAsync(id);
                if (result)
                    return RedirectToAction("Index", "Service");
                ModelState.AddModelError("", "Niestety nie udało się usunąć.");
            }
            return View();
        }

        public async Task<IActionResult> Paid(int id)
        {
            if(ModelState.IsValid)
            {
                var result = await _paymentRecordService.PaidPaymentAsync(id);
                if (result)
                    return RedirectToAction("Index", "Service");
                ModelState.AddModelError("", "Niestety nie udało się zmienić statusu.");
            }
            return View();
        }


    }
}
