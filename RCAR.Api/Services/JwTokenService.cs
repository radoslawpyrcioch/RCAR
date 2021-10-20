using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RCAR.Api.Extension;
using RCAR.Api.Services.Interfaces;
using RCAR.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RCAR.Api.Services
{
    public class JwTokenService : IJwTokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IOptionsMonitor<JwtConfig> _jwtConfig;

        public JwTokenService(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _jwtConfig = optionsMonitor;
        }

        public async Task<bool> IsValidUserEmailAndPassword(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<string> GenerateToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var role = await _userManager.GetRolesAsync(user);

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.CurrentValue.Secret);
            var tokenDescriptior = new SecurityTokenDescriptor
            {
                
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, email),
                    new Claim(ClaimTypes.Role, role[0]),

                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptior);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
