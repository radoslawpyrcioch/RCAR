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

        public IActionResult Index()
        {
            return View();
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


    }
}
