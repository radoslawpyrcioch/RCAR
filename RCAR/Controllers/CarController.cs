using Microsoft.AspNetCore.Mvc;
using RCAR.Models.CarVM;
using RCAR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        public IActionResult Create()
        {
            var model = new CarCreateVM();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarCreateVM carCreateVM, int id)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            if (ModelState.IsValid)
            {
                var carCreate = await _carService.CreateCarAsync(carCreateVM, id, currentUserId);
                if (carCreate)
                    return RedirectToAction("Index", "Member");
                ModelState.AddModelError("", "Niestety nie udało się usunąć.");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            if(ModelState.IsValid)
            {
                var result = await _carService.RemoveCarAsync(id);
                if (result)
                    return RedirectToAction("Index", "Member");
                ModelState.AddModelError("", "Niestety nie udało się usunąć.");
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Done(int id)
        {
            if(ModelState.IsValid)
            {
                var result = await _carService.DoneCarSeriveAsync(id);
                if (result)
                    return RedirectToAction("Index", "Member");
                ModelState.AddModelError("", "Niestety nie udało się zmienić statusu.");
            }
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Detail(int id)
        {
            var model = await _carService.DetailCarAsync(id);
            if (model.PaymentRecords.Count() > 0)
            {
                var count = 0;
                decimal totalPayment = 0;
                _carService.CountPayment(model.PaymentRecords, ref count, ref totalPayment);
                ViewBag.TotalInvoicePayment = totalPayment;
                ViewBag.TotalCount = count;
            }
            if (model == null)
                return RedirectToAction("Index", "Member");
            return View(model);
        }

        public async Task<IActionResult> InProgressStatus(int id)
        {
            if (ModelState.IsValid)
            {
                var result = await _carService.InProgressCarStatusAsync(id);
                if (result)
                    return RedirectToAction("Index", "Member");
                ModelState.AddModelError("", "Niestety nie udało się zmienić statusu.");
            }
            return RedirectToAction("Index", "Member");
        }
 
    }
}
