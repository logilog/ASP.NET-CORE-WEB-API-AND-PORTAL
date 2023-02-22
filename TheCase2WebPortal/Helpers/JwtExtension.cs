using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TheCase2WebPortal.Helpers
{
    public class JwtExtension
    {
        public static List<Claim> JwtTokenDecode(string token, string JwtOptionsKey,string JwtOptionsIssuer, string JwtOptionsAudience)
        {
            var handler = new JwtSecurityTokenHandler().ValidateToken(token, new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptionsKey)),
                ValidIssuer = JwtOptionsIssuer,
                ValidateIssuer = true,
                ValidAudience = JwtOptionsAudience,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            }, out SecurityToken stoken);
            var claims = new List<Claim>();
            foreach (var item in handler.Claims)
            {
                claims.Add(item);
            }
            return claims;
        }
    }
}
