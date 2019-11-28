// TokenMgr.cs   copyright tom jones

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Agent.Helpers
{
    public class TokenMgr
    {
        private static string Secret = "27FB803228D498842D1D46693B93443F9B422D3BFF1007645E04AF9E1F865F6E"; //  "XCAP05H6LoKvbRRa/QkqLNMI7cOHguaRyHzyg7n5qEkGjQmtBhz4SzYh4Fqwjyi3KJHlSXKPwVu2+bXr6CtpgQ==";

        public static string GenerateToken (string iss, string sub, int minsToLive)
        {
            byte[] key = Convert.FromBase64String(Secret);
            SymmetricSecurityKey secKey = new SymmetricSecurityKey(key);
          
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {  new Claim(ClaimTypes.Name, sub)  }),
                Expires = DateTime.UtcNow.AddMinutes(minsToLive),
                SigningCredentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = jwtHandler.CreateJwtSecurityToken(descriptor);
            return jwtHandler.WriteToken(token);

        }
    }
}
