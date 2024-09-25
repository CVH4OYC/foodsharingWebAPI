﻿using FoodsharingWebAPI.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodsharingWebAPI.Infrastructure
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions options;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            this.options = options.Value;
        }
        public string GenerateToken(Models.User user)
        {
            var claims = new List<Claim> { new Claim("userId", user.UserName) };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(options.ExpiresHours));

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
