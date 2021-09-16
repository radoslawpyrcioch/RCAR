using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.ReportVM
{
    public class ReportPaymentExcelVM
    {
        public int PaymentRecordId { get; set; }

        public int ServiceId { get; set; }
        public ReportServiceVM Service { get; set; }

        public string Name { get; set; }

        public decimal NetAmount { get; set; }

        public string Status { get; set; }
    }
}
