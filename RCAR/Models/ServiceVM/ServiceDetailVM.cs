using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models.ServiceVM
{
    public class ServiceDetailVM
    {
        public int ServiceId { get; set; }
        public string ServiceNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string FullName 
        { get
            {
                return FirstName + " " + LastName;
            } 
        }
        public string Phone { get; set; }
        public DateTime ServiceSince { get; set; }
        public DateTime ServiceTo { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
