using RCAR.Domain.Entities;
using RCAR.Models.ServiceVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.PaymentRecordVM
{
    public class PaymentCreateVM
    {
        public PaymentCreateVM() => IsRemoved = false;

        public int PaymentRecordId { get; set; }


        public int ServiceId { get; set; }
        //public Service Service { get; set; }

        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Podatek")]
        public decimal Tax { get; set; }

        [Display(Name = "Kwota netto")]
        public decimal NetAmount { get; set; }

        [Display(Name = "Kwota brutto")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "Rabat")]
        public decimal Discount { get; set; }
        public bool IsRemoved { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

    }
}
