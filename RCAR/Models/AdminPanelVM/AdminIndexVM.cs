using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.AdminPanelVM
{
    public class AdminIndexVM
    {
        public IEnumerable<AdminVM> Users { get; set; }
    }
}
