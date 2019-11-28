using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TcAuthentication.Token
{
    public static class AuthnConfig
    {
        public static string GenerateJWT()
        {
            // key from https://passwordsgenerator.net/sha256-hash-generator/  on TopCat
            var securekey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("27FB803228D498842D1D46693B93443F9B422D3BFF1007645E04AF9E1F865F6E"));
            var creds = new SigningCredentials(securekey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim("UserName", "Admin"),
                new Claim("Role", "one"), };

            var token = new JwtSecurityToken("tid:tr:foobar",
                "http://localhost:8765",
                claims,
                DateTime.UtcNow,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        //Conf Authn
        internal static TokenValidationParameters tokenValidationParams;
        public static void ConfigAuthN(this IServiceCollection services)
        {
            var securekey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("27FB803228D498842D1D46693B93443F9B422D3BFF1007645E04AF9E1F865F6E"));
            var creds = new SigningCredentials(securekey, SecurityAlgorithms.HmacSha256);

            tokenValidationParams = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidIssuer = "",
                ValidateLifetime = true,
                ValidAudience = "",
                ValidateAudience = true,
                RequireSignedTokens = true,
                IssuerSigningKey = creds.Key,
                ClockSkew = TimeSpan.FromMinutes(30)
            };
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParams;
                options.RequireHttpsMetadata = false;
            });
        }

    }
}
