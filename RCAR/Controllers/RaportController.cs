using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Controllers
{
    public class RaportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
