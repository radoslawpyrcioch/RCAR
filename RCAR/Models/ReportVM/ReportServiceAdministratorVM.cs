using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.ReportVM
{
    public class ReportServiceAdministratorVM
    {
        public int ServiceId { get; set; }

        [Display(Name = "Numer")]
        public string ServiceNo { get; set; }

        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

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

        [Display(Name = "Suma płatności netto")]
        public decimal PaymentNetAmount { get; set; }


        public IEnumerable<ReportPaymentVM> Payment { get; set; }
    }
}
