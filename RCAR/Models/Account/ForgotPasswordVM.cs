using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.Account
{
    public class ForgotPasswordVM
    {
        [Required(ErrorMessage = "Adres email jest wymagany.")]
        [Display(Name = "Adres Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
