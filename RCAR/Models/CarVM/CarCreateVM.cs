using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.CarVM
{
    public class CarCreateVM
    {
        public CarCreateVM() => IsRemoved = false;

        public int CarId { get; set; }

        public int MemberId { get; set; }

        [Required(ErrorMessage = "Marka jest wymagana")]
        [Display(Name = "Marka")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Model jest wymagany")]
        [Display(Name = "Model")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Opis jest wymagany")]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Data rozpoczecia ostatniego serwisu jest wymagana")]
        [Display(Name = "Ostatni serwis od")]
        public DateTime ServiceSince { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Data zakończenia ostatniego serwisu jest wymagana")]
        [Display(Name = "Ostatni serwis do")]
        public DateTime ServiceTo { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Status jest wymagany")]
        [Display(Name = "Status")]
        public string Status { get; set; }
        public bool IsRemoved { get; set; }
    }
}
