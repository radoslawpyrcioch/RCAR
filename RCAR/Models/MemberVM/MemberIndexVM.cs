using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.MemberVM
{
    public class MemberIndexVM
    {
        public MemberCreateVM MemberCreateVM { get; set; }
        public IEnumerable<MemberVM> Members { get; set; }
    }
}
