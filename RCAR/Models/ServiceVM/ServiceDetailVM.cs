using RCAR.Models.PaymentRecordVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.ServiceVM
{
    public class ServiceDetailVM
    {
        public PaymentCreateVM PaymentCreateVM { get; set; }
        public int ServiceId { get; set; }

        [Display(Name = "Numer")]
        public string ServiceNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Display(Name = "Imię i nazwisko")]
        public string FullName 
        { get
            {
                return FirstName + " " + LastName;
            } 
        }

        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [Display(Name = "Data przyjęcia")]
        public DateTime ServiceSince { get; set; }

        [Display(Name = "Data zakończenia")]
        public DateTime ServiceTo { get; set; }

        [Display(Name = "Marka")]
        public string Brand { get; set; }

        [Display(Name = "Model")]
        public string Model { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        public IEnumerable<PaymentVM> PaymentRecords { get; set; }
    }
}
