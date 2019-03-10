using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BLL.AppStart;
using BLL.IdentityWrappers;
using BLL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSecurityToken _jwt;
        private readonly IRoleManager _manager;

        public TokenService(ITokenSettings configuration, IRoleManager manager)
        {
            _manager = manager;
            var configuration1 = configuration;

            var issuer = configuration1.TokenIssuer;
            var audience = configuration1.TokenAudience;
            var secretKey = configuration1.TokenSecretKey;
            var lifetime = configuration1.TokenLifetime;

            var roleClaims = await _manager.GetRoleClaims()

            var currentTime = DateTime.UtcNow;
            _jwt = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                notBefore: currentTime,
                claims: 
                expires: currentTime.Add(TimeSpan.FromMinutes(int.Parse(lifetime))),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(secretKey)),
                    SecurityAlgorithms.HmacSha256)
            );

        }

        public string GetEncodedJwtToken()
        {
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(_jwt);

            return encodedToken;
        }
    }
}
