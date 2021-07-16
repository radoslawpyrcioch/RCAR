using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.MemberVM
{
    public class MemberEditVM
    {
        public int MemberId { get; set; }

        [Required(ErrorMessage = "Numer jest wymagany.")]
        [Display(Name = "Numer")]
        public string MemberNo { get; set; }

        [Required(ErrorMessage = "Imię jest wymagany.")]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagany.")]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Telefon jest wymagany.")]
        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email jest wymagany.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Data przyjęcia jest wymagana.")]
        [DataType(DataType.Date), Display(Name = "Data przyjęcia")]
        public DateTime DateJoined { get; set; } = DateTime.UtcNow;

        [DataType(DataType.Date), Display(Name = "Data odejścia")]
        public DateTime DateLeaves { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Adres jest wymagany.")]
        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Miasto jest wymagany.")]
        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Required(ErrorMessage = "Kod pocztowy jest wymagany.")]
        [Display(Name = "Kod pocztowy")]
        public string PostCode { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

    }
}
