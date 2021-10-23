using RCAR.Api.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RCAR.Api.Services.Interfaces
{
    public interface IJwTokenService
    {
        Task<bool> IsValidUserEmailAndPassword(string email, string password);
        Task<RefreshTokenDTO> GenerateToken(string email);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken);
        Task<RefreshTokenDTO> GenerateFreshToken(ClaimsPrincipal principal, string refreshToken);
    }
}
