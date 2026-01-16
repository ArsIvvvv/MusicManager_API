using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MusicMicroservice.Application.Common.Interfaces.Authentication;
using MusicMicroservice.Application.Common.Settings;
using MusicMicroservice.Domain.Entities.Identity;

namespace MusicMicroservice.Application.Authentication
{
    public class JwtTokenGenerator: IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<User> _userManager;

        public JwtTokenGenerator(IOptions<JwtSettings> options, UserManager<User> userManager)
        {
            _jwtSettings = options.Value;
            _userManager = userManager;
        }
        
        public async Task<string> GenerateToken(User user)
        {

            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, user.Id),
                new (ClaimTypes.Name, user.UserName!),
                new (ClaimTypes.Email, user.Email!),
                new (ClaimTypes.DateOfBirth, user.DateOfBirth.ToString("yyyy-MM-dd")),
                
            };

            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: cred
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}