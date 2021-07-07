using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCAR.Domain.Entities
{
    public class Car
    {
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public DateTime ServiceSince { get; set; }
        public DateTime? ServiceTo { get; set; }
        public string ImageUrl { get; set; }

        public virtual Member Member { get; set; }
        public int MemberId { get; set; }
        public virtual User User { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<PaymentRecord> PaymentRecords { get; set; }


    }
}
