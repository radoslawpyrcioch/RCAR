using Microsoft.AspNetCore.Mvc;
using RCAR.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Api.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;
        public ServiceController(IServiceService serviceService) => _serviceService = serviceService;       

    }
}
