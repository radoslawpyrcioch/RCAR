using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.ServiceVM
{
    public class ServiceVM
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
        public DateTime ServiceSince { get; set; } = DateTime.UtcNow;

        [DataType(DataType.Date), Display(Name = "Data zakończenia")]
        public DateTime ServiceTo { get; set; } = DateTime.UtcNow;

        [Display(Name = "Zdjęcie")]
        public string ImageUrl { get; set; }

        [Display(Name = "Marka samochodu")]
        public string Brand { get; set; }

        [Display(Name = "Model samochodu")]
        public string Model { get; set; }

        [Display(Name = "Opis naprawy / uwagi")]
        public string Description { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
    }
}
