using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.UserVM
{
    public class UserManageVM
    {
        public string Email { get; set; }

        [Required(ErrorMessage = "Stare hasło jest wymagane.")]
        [Display(Name = "Stare hasło")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Nowe hasło jest wymagane.")]
        [Display(Name = "Nowe hasło")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Potwierdzenie nowego hasło")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Hasła nie są takie same.")]
        public string ConfirmNewPassword { get; set; }

        [Display(Name = "Kod potwierdzający")]
        public string Token { get; set; }
    }
}
