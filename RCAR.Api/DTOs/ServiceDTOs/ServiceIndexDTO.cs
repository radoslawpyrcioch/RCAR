using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Api.DTOs.ServiceDTOs
{
    public class ServiceIndexDTO
    {
        public IEnumerable<ServiceDTO> Services { get; set; }
    }
}
