using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using EZChat.Master.Identity.Models;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EZChat.Master.Identity.Services
{
    public class JsonWebTokenGenerator : IJsonWebTokenGenerator
    {
        private readonly string _issuer;
        private readonly SigningCredentials _credentials;
        private readonly int _expiresDays;

        public JsonWebTokenGenerator(IConfiguration config)
        {
            var securityKey = config["JwtBearer:Key"];
            var bytes = Encoding.UTF8.GetBytes(securityKey);
            var key = new SymmetricSecurityKey(bytes);

            _issuer = config["JwtBearer:Issuer"];
            _credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            _expiresDays = 365;
        }

        public JsonWebToken Generate(AppUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
            };

            var token = new JwtSecurityToken(issuer: _issuer,
                                             audience: _issuer,
                                             claims: claims,
                                             signingCredentials: _credentials,
                                             expires: DateTime.UtcNow.AddDays(_expiresDays));

            return new JsonWebToken
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
