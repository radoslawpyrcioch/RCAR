using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.ReportVM
{
    public class ReportPaymentVM
    {
        public int PaymentRecordId { get; set; }

        public int ServiceId { get; set; }
        public ReportServiceVM Service { get; set; }

        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Kwota netto")]
        public decimal NetAmount { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
    }
}
