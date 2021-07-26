using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.PaymentRecordVM
{
    public class PaymentIndexVM
    {
        public IEnumerable<PaymentVM> Payments { get; set; }
    }
}
