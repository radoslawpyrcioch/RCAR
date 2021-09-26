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

        public ReportController(IReportService reportService, IExcelService excelService)
        {
            _excelService = excelService;
            _reportService = reportService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DetailService(string filterService)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var model = new ReportVM()
            {
                ReportPayment = await _reportService.GetAllServiceWithLastPaymentAsync(filterService, currentUserId),
            };
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DetailReportServiceAdministrator(string filterService)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var model = new ReportVM()
            {
                ReportAdministrator = await _reportService.GetAllServiceWithTotalPaymentNetAmount(filterService, currentUserId)
            };
            return View(model);
        }


        public async Task<IActionResult> ExportToExcel(string exportService)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var model = await _excelService.GenerateReportServiceExcel(exportService, currentUserId);
            string fileName = string.Format("RaportSerwisow_{0}.xlsx", DateTime.Today.ToShortDateString());
            string fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(model, fileType, fileName);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ExportToExcelAdministratorListService(string exportService)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var model = await _excelService.GenerateReportServiceAdministratorExcel(exportService, currentUserId);
            string fileName = string.Format("RaportSerwisowAdministrator_{0}.xlsx", DateTime.Today.ToShortDateString());
            string fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(model, fileType, fileName);
        }

    }
}
