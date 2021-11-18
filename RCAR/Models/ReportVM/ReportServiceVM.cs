using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.ReportVM
{
    public class ReportServiceVM
    {
        public int ServiceId { get; set; }

        [Display(Name = "Numer")]
        public string ServiceNo { get; set; }

        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Display(Name = "Imię i nazwisko")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }


        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [DataType(DataType.Date), Display(Name = "Data przyjęcia")]
        public DateTime ServiceSince { get; set; }

        [DataType(DataType.Date), Display(Name = "Data zakończenia")]
        public DateTime ServiceTo { get; set; }


        [Display(Name = "Opis naprawy")]
        public string Description { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Kwota Netto")]
        public decimal PaymentNetAmount { get; set; }

        [Display(Name = "Numer")]
        public string PaymentNumber { get; set; }

        [Display(Name = "Status")]
        public string PaymentStatus { get; set; }

        [Display(Name = "Suma płatności netto")]
        public string TotalNetPayment { get; set; }


        public IEnumerable<ReportPaymentVM> Payment { get; set; }
    }
}
