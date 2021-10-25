﻿using Microsoft.AspNetCore.Http;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneService(int id)
        {
            var currentUser = User.Claims.ElementAt(0).Value;
            if (currentUser != null)
            {
                var dto = await _serviceService.GetOneServiceAsync(currentUser, id);
                if (dto != null)
                    return Ok(dto);
                return NotFound();
            }
            return BadRequest(new UnauthorizedAccessException());
        }

        [HttpPost]
        public async Task<IActionResult> CreateServie(ServiceCreateDTO dto)
        {
            var currentUser = User.Claims.ElementAt(0).Value;
            if (currentUser != null)
            {
                var resultDto = await _serviceService.CreateServiceAsync(dto, currentUser);
                if (resultDto != null)
                {
                    return Created(string.Format("/Service/{0}", resultDto.ServiceId), resultDto);
                }
                return BadRequest(string.Format("Serwis o takim numerze {0} już istnieje", dto.ServiceNo));
            }
            return BadRequest(new UnauthorizedAccessException());
        }
    }
}