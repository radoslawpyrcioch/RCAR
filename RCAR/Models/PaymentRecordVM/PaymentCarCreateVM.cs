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
    public class PaymentCarCreateVM
    {
        public PaymentCarCreateVM() => IsRemoved = false;

        public int PaymentRecordId { get; set; }


        public int CarId { get; set; }

        [Required(ErrorMessage = "Numer jest wymagany.")]
        [Display(Name = "Numer")]
        public string Name { get; set; } //PaymentNo

        [Required(ErrorMessage = "Opis jest wymagany.")]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Podatek")]
        public decimal Tax { get; set; }

        [Display(Name = "Kwota netto")]
        public decimal NetAmount { get; set; }

        [Required(ErrorMessage = "Kwota brutto jest wymagana.")]
        [Display(Name = "Kwota brutto")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Rabat jest wymagany.")]
        [Display(Name = "Rabat")]
        public decimal Discount { get; set; }
        public bool IsRemoved { get; set; }

        [Required(ErrorMessage = "Status jest wymagany.")]
        [Display(Name = "Status")]
        public string Status { get; set; }

    }
}
