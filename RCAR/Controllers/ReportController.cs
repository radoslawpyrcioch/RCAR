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
        private readonly IExcelService _excelService;
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService, IExcelService excelService)
        {
            _excelService = excelService;
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

        [HttpGet]
        public async Task<IActionResult> ExportToExcel()
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var model = await _excelService.GenerateReportServiceExcel(currentUserId);
            string fileName = string.Format("RaportSerwisow_{0}.xlsx", DateTime.Today.ToShortDateString());
            string fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(model, fileType, fileName);
        }

    }
}
