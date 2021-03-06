using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCAR.Domain.Entities
{
    public class Member
    {
        public int MemberId { get; set; }
        public string MemberNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime DateLeaves { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public bool IsRemoved { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<PaymentRecord> PaymentRecords { get; set; }
        public virtual User User { get; set; }
        public string UserId { get; set; }
    }
}
