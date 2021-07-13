using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.ServiceVM
{
    public class ServiceEditVM
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
        public string LastName { get; set; }

        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Data przyjęcia jest wymagana.")]
        [DataType(DataType.Date), Display(Name = "Data przyjęcia")]
        public DateTime ServiceSince { get; set; }

        [Required(ErrorMessage = "Data zakończenia jest wymagana.")]
        [DataType(DataType.Date), Display(Name = "Data zakończenia")]
        public DateTime? ServiceTo { get; set; }

        [Required(ErrorMessage = "Marka samochodu jest wymagana.")]
        [Display(Name = "Marka samochodu")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Model samochodu jest wymagany.")]
        [Display(Name = "Model samochodu")]
        public string CarModel { get; set; }

        [Required(ErrorMessage = "Opis jest wymagany.")]
        [Display(Name = "Opis naprawy / uwagi")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Status jest wymagany.")]
        public string Status { get; set; }

        //public bool IsRemoved { get; set; }
    }
}
