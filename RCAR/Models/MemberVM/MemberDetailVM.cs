using RCAR.Models.CarVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.MemberVM
{
    public class MemberDetailVM
    {
        public int MemberId { get; set; }

        [Display(Name = "Numer")]
        public string MemberNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Display(Name = "Imię i nazwisko")]
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

        [Display(Name = "Data przyjęcia")]
        public DateTime DateJoined { get; set; }

        [Display(Name = "Data zakończenia")]
        public DateTime DateLeaves { get; set; }

        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Kod pocztowy")]
        public string PostCode { get; set; }
        public bool IsRemoved { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
        public IEnumerable<CarsVM> Cars { get; set; }
    }
}
