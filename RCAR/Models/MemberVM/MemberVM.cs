using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.MemberVM
{
    public class MemberVM
    {
        public int MemberId { get; set; }

        [Display(Name = "Numer")]
        public string MemberNo { get; set; }

        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Display(Name = "Imię i Nazwisko")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Date), Display(Name = "Data przyjęcia")]
        public DateTime DateJoined { get; set; } = DateTime.UtcNow;

        [DataType(DataType.Date), Display(Name = "Data odejścia")]
        public DateTime DateLeaves { get; set; } = DateTime.UtcNow;

        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Kod pocztowy")]
        public string PostCode { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }



    }
}
