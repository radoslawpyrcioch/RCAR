using Microsoft.AspNetCore.Mvc;
using RCAR.Api.DTOs;
using RCAR.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JwTokenController : Controller
    {
        private readonly IJwTokenService _jwTokenService;

        public JwTokenController(IJwTokenService jwTokenService)
        {
            _jwTokenService = jwTokenService;
        }

        [HttpPost("CreateToken")]
        public async Task<IActionResult> SignIn(UserCardDTO dto)
        {
            var result = await _jwTokenService.IsValidUserEmailAndPassword(dto.Email, dto.Password);
            if (result)
            {
                var token = await _jwTokenService.GenerateToken(dto.Email);
                return Ok(token);
            }
            return NotFound();
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> Refresh(RefreshTokenDTO dto)
        {
            var principal = _jwTokenService.GetPrincipalFromExpiredToken(dto.AccessToken);
            if (principal != null)
            {
                var token = await _jwTokenService.GenerateFreshToken(principal, dto.RefreshToken);
                if (token != null)
                    return Ok(token);
            }
            return NotFound();
        }
    }
}
