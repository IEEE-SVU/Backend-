using Domain.IRepositories;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.TokenGenerator
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IRepository<User> _userRepo;
        private readonly IConfiguration _configuration;
        public TokenGenerator(IRepository<User> userRepo, IConfiguration config) 
        {
            _userRepo = userRepo;
            _configuration = config;
        }
        public  async Task<string> GenerateTokenAsync(Guid Id)
        {
            if (Id == Guid.Empty)
                throw new ArgumentException("Invalid user ID.");

            var user = await _userRepo.GetByIDAsync(Id);
            if (user == null)
                throw new ArgumentException("User not found.");

            var jwtSection = _configuration.GetSection("JWT");

            var secretKey = jwtSection.GetValue<string>("SecretKey");
            var duration = jwtSection.GetValue<int>("DurationMinutes");
            var issuer = jwtSection.GetValue<string>("Issuer");
            var audience = jwtSection.GetValue<string>("Audience");

            if (string.IsNullOrEmpty(secretKey) || duration == 0)
            {
                throw new Exception("JWT Settings are missing in appsettings.json. Please check SecretKey and DurationMinutes.");
            }

            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,user.Role.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Expires = DateTime.UtcNow.AddMinutes(duration),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(claims)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
