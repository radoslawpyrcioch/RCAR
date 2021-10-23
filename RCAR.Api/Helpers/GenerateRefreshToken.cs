using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace RCAR.Api.Helpers
{
    public static class GenerateRefreshToken
    {
        public static string GenerateRefreshTokenJwt()
        {
            var random = new byte[32];
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }
    }
}
