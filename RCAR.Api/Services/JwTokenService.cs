using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RCAR.Api.DTOs;
using RCAR.Api.Extension;
using RCAR.Api.Helpers;
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

        public async Task<RefreshTokenDTO> GenerateToken(string email)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Email == email);
            if (user != null)
            {
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
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = jwtTokenHandler.CreateToken(tokenDescriptior);
                var writeToken = jwtTokenHandler.WriteToken(token);
                var refreshToken = GenerateRefreshToken.GenerateRefreshTokenJwt();
                user.RefreshToken = refreshToken;
                await _unitOfWork.SaveChangesAsync();

                return new RefreshTokenDTO()
                {
                    AccessToken = writeToken,
                    RefreshToken = refreshToken
                };
            }
            return null;

        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.CurrentValue.Secret);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false
            };

            var tokenHanlder = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Token nieważny");

            return principal;
        }

        public async Task<RefreshTokenDTO> GenerateFreshToken(ClaimsPrincipal principal, string refreshToken)
        {
            var username = principal.Claims.ElementAt(0).Value;
            var user = await _unitOfWork.User.FindOneAsync(u => u.Email == username);
            if (user != null)
            {
                if (user.RefreshToken != refreshToken)
                    throw new SecurityTokenException("Nieważny token odświeżania");

                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtConfig.CurrentValue.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, principal.Claims.ElementAt(0).Value),
                        new Claim(ClaimTypes.Name, principal.Claims.ElementAt(1).Value)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };

                var token = jwtTokenHandler.CreateToken(tokenDescriptor);
                var writeToken = jwtTokenHandler.WriteToken(token);
                var newRefreshToken = GenerateRefreshToken.GenerateRefreshTokenJwt();
                user.RefreshToken = refreshToken;
                await _unitOfWork.SaveChangesAsync();

                return new RefreshTokenDTO()
                {
                    AccessToken = writeToken,
                    RefreshToken = refreshToken
                };
            }
            return null;
        }
    }
}
