using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using JWTTOKEN.Entities;
using Microsoft.Extensions.Options;

namespace JWTTOKEN.Services
{
    public interface ITokenService
    {
        string GenerateToken(Claim[] additionalClaims);

    }
    public class TokenService : ITokenService
    {
        private readonly JWTSettings _jwtSettings;
        public TokenService(IOptions<JWTSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        public string GenerateToken(Claim[] additionalClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var issuer = _jwtSettings.Issuer;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.UtcNow.Add(new TimeSpan(0, 15, 0)),
                claims: additionalClaims,
                signingCredentials: creds
            );

            return tokenHandler.WriteToken(token);
        }
    }
}

