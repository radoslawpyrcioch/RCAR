using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCAR.Domain.Entities
{
    public class Service
    {
        //Quick service without member created
        public int ServiceId { get; set; }
        public string ServiceNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime ServiceSince { get; set; }
        public DateTime? ServiceTo { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public bool IsRemoved { get; set; }
        public string Status { get; set; }

        public virtual User User { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<PaymentRecord> PaymentRecords { get; set; }

    }
}
