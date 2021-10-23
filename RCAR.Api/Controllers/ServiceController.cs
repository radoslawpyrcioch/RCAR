using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RCAR.Api.DTOs.ServiceDTOs;
using RCAR.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var currentUser = User.Claims.ElementAt(0).Value;
            if (currentUser != null)
            {
                var dto = new ServiceIndexDTO()
                {
                    Services = await _serviceService.GetAllServiceAsync(currentUser)
                };
                return Ok(dto);
            }
            return BadRequest(new UnauthorizedAccessException());
        }
    }
}
