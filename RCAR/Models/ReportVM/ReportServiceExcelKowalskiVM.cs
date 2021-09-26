using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.ReportVM
{
    public class ReportServiceExcelKowalskiVM
    {

        [DisplayName("Numer")]
        public string ServiceNo { get; set; }

        [DisplayName("Imię")]
        public string FirstName { get; set; }

        [DisplayName("Nazwisko")]
        public string LastName { get; set; }

        [DisplayName("Telefon")]
        public string Phone { get; set; }

        [DataType(DataType.Date), DisplayName("Data przyjęcia")]
        public DateTime ServiceSince { get; set; }

        [DataType(DataType.Date), DisplayName("Data zakończenia")]
        public DateTime ServiceTo { get; set; }


        [DisplayName("Opis naprawy")]
        public string Description { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        [DisplayName("Suma płatności netto")]
        public string TotalNetPayment { get; set; }
    }
}
