using Microsoft.AspNetCore.Mvc;
using RCAR.Models.ReportVM;
using RCAR.Models.ServiceVM;
using RCAR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DetailService(string sortOrder)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var model = new ReportVM()
            {
                ReportPayment = await _reportService.GetAllServiceWithLastPaymentAsync(sortOrder, currentUserId),
            };
            return View(model);
        }

    }
}
