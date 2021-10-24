using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Api.DTOs.ServiceDTOs
{
    public class ServiceCreateDTO
    {
        public ServiceCreateDTO() => IsRemoved = false;

        [Required]
        [MaxLength(4)]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Numer może zawierać tylko liczby od 0 - 9")]
        public string ServiceNo { get; set; }

        [Required(ErrorMessage = "Imię jest wymagany.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagany.")]
        public string LastName { get; set; }


        public string Phone { get; set; }

        [Required(ErrorMessage = "Data przyjęcia jest wymagana.")]
        [DataType(DataType.Date), Display(Name = "Data przyjęcia")]
        public DateTime ServiceSince { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Data zakończenia jest wymagana.")]
        [DataType(DataType.Date), Display(Name = "Data zakończenia")]
        public DateTime ServiceTo { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Marka samochodu jest wymagana.")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Model samochodu jest wymagany.")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Opis jest wymagany.")]
        public string Description { get; set; }

        [Required]
        [RegularExpression(@"^Przyjęty|Rozpoczęty|Zakończony$", ErrorMessage = "Status może zawierać tylko 'Przyjęty', 'Rozpoczęty', 'Zakończony'")]
        public string Status { get; set; }

        public bool IsRemoved { get; set; }
    }
}
