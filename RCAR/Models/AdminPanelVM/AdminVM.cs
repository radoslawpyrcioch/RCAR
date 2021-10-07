using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.AdminPanelVM
{
    public class AdminVM
    {
        [Display(Name = "Adres Email")]
        public string Email { get; set; }

        [Display(Name = "Potwierdzenie Email")]
        public bool EmailConfirmed { get; set; }

    }
}
