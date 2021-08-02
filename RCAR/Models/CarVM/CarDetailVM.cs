using RCAR.Models.PaymentRecordVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.CarVM
{
    public class CarDetailVM
    {
        public int CarId { get; set; }

        [Display(Name = "Marka")]
        public string Brand { get; set; }

        [Display(Name = "Model")]
        public string Model { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Ostatni serwis od")]
        public DateTime ServiceSince { get; set; }

        [Display(Name = "Ostatni serwis do")]
        public DateTime ServiceTo { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        public IEnumerable<PaymentVM> PaymentRecords { get; set; }
    }
}
