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
        public string MiddleName { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public DateTime ServiceSince { get; set; }
        public DateTime? ServiceTo { get; set; }
        public string ImageUrl { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }


        public virtual User User { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<PaymentRecord> PaymentRecords { get; set; }

    }
}
