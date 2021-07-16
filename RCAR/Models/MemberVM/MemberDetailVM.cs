using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.MemberVM
{
    public class MemberDetailVM
    {
        public int MemberId { get; set; }
        public string MemberNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime DateLeaves { get; set; } = DateTime.UtcNow;
        public string Address { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public bool IsRemoved { get; set; }
        public string Status { get; set; }
    }
}
