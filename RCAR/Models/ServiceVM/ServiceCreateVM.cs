using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.ServiceVM
{
    public class ServiceCreateVM
    {
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "Numer jest wymagany.")]
        [Display(Name = "Numer")]
        public string ServiceNo { get; set; }

        [Required(ErrorMessage = "Imię jest wymagany.")]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagany.")]
        [Display(Name = "Nazwisko")]
        public string MiddleName { get; set; } // change for LastName

        public string FullName 
        {
            get
            {
                return FirstName + " " + MiddleName;
            }
        }


        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [DataType(DataType.Date), Display(Name = "Data przyjęcia")]
        public DateTime ServiceSince { get; set; } = DateTime.UtcNow;

        [DataType(DataType.Date), Display(Name = "Data zakończenia")]
        public DateTime? ServiceTo { get; set; } = DateTime.UtcNow;

        [Display(Name = "Zdjęcie")]
        public IFormFile ImageUrl { get; set; }

        [Required(ErrorMessage = "Marka samochodu jest wymagana.")]
        [Display(Name = "Marka samochodu")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Model samochodu jest wymagany.")]
        [Display(Name = "Model samochodu")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Opis jest wymagany.")]
        [Display(Name = "Opis naprawy / uwagi")]
        public string Description { get; set; }
    }
}
