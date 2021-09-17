using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.ReportVM
{
    public class ReportVM
    {
        public IEnumerable<ReportServiceVM> ReportPayment{ get; set; }
        public IEnumerable<ReportPaymentVM> Payment{ get; set; }
        public IEnumerable<ReportServiceExcelVM> ExcelPayment{ get; set; }
        public IEnumerable<ReportServiceAdministratorVM> ReportAdministrator{ get; set; }
    }
}
