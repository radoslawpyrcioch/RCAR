using RCAR.Models.ReportVM;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RCAR.Services.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<ReportServiceVM>> GetAllServiceWithLastPaymentAsync(string filterService, string userId);
        Task<IEnumerable<ReportServiceAdministratorVM>> GetAllServiceWithTotalPaymentNetAmount(string filterService, string userId);
    }
}
