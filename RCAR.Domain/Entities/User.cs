using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCAR.Domain.Entities
{
    public class User : IdentityUser
    {
        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<Member> Members { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
