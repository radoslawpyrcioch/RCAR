using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.ReportVM
{
    public class ReportServiceAdministratorExcelVM
    {
       //public int ServiceId { get; set; }

        [DisplayName("Numer")]
        public string ServiceNo { get; set; }

        [DisplayName("Imię")]
        public string FirstName { get; set; }

        [DisplayName("Nazwisko")]
        public string LastName { get; set; }

        [DisplayName("Telefon")]
        public string Phone { get; set; }

        [Column(TypeName = "date"), DisplayName("Data przyjęcia")]
        public DateTime ServiceSince { get; set; }

        [Column(TypeName = "date"), DisplayName("Data zakończenia")]
        public DateTime ServiceTo { get; set; }

        [DisplayName("Opis naprawy")]
        public string Description { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        [DisplayName("Suma płatności netto")]
        public decimal PaymentNetAmount { get; set; }

    }
}
