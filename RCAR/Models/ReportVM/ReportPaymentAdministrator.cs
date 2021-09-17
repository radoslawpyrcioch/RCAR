using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.ReportVM
{
    public class ReportPaymentAdministrator
    {
        public int PaymentRecordId { get; set; }

        public int ServiceId { get; set; }
        public ReportServiceVM Service { get; set; }


        [Display(Name = "Suma płatności netto")]
        public decimal NetAmount { get; set; }

    }
}
