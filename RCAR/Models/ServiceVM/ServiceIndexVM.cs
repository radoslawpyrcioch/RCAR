using RCAR.Models.PaymentRecordVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.ServiceVM
{
    public class ServiceIndexVM
    {
        public ServiceCreateVM ServiceCreateVM { get; set; }
        public PaymentCreateVM PaymentCreateVM { get; set; }
        public IEnumerable<ServiceVM> Services { get; set; }
    }
}
