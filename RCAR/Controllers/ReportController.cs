using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IExcelService _excelService;
        private readonly IReportService _reportService;
        private const string kowalskiUserId = "7b4d87a6-44be-4ef5-962f-7913bd97039c";

        private static string Filter;

        public ReportController(IReportService reportService, IExcelService excelService)
        {
            _excelService = excelService;
            _reportService = reportService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet, HttpPost]
        public async Task<IActionResult> DetailService(string filterService)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;

            var model = new ReportVM()
            {
                ReportPayment = await _reportService.GetAllServiceWithLastPaymentAsync(filterService, currentUserId)
               
            };
            Filter = filterService;
            if (currentUserId == kowalskiUserId)
            {
                return View("DetailReportServiceKowalski", model);
            }
            return View(model);
        }

        [HttpGet, HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DetailReportServiceAdministrator(string filterService)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var model = new ReportVM()
            {
                ReportAdministrator = await _reportService.GetAllServiceWithTotalPaymentNetAmount(filterService, currentUserId)
            };
            Filter = filterService;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ExportToExcel(string exportService)
        {
            exportService = Filter;
            var currentUserId = User.Claims.ElementAt(0).Value;
            var model = await _excelService.GenerateReportServiceExcel(exportService, currentUserId);
            string fileName = string.Format("RaportSerwisow_{0}.xlsx", DateTime.Today.ToShortDateString());
            string fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(model, fileType, fileName);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ExportToExcelAdministratorListService(string exportService)
        {
            exportService = Filter;
            var currentUserId = User.Claims.ElementAt(0).Value;
            var model = await _excelService.GenerateReportServiceAdministratorExcel(exportService, currentUserId);
            string fileName = string.Format("RaportSerwisowAdministrator_{0}.xlsx", DateTime.Today.ToShortDateString());
            string fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(model, fileType, fileName);
        }

    }
}
