using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.Account
{
    public class ResetPasswordVM
    {
        [Required(ErrorMessage = "Adres email jest wymagany")]
        [Display(Name = "Adres Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Potwierdzenie hasła")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasła nie sa takie same.")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}
