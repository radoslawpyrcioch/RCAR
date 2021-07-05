using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Models
{
    public class MessageVM
    {
        public string Topic { get; set; }
        public string Message { get; set; }
        public string LinkName { get; set; }
        public string LinkUrl { get; set; }
    }
}
