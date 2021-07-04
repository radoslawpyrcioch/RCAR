using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCAR.Domain.Entities
{
    public class PaymentRecord
    {
        public int PaymentRecordId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Tax { get; set; }
        public decimal NetAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
