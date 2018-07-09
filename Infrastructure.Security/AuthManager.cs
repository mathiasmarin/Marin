using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Security
{
    public class AuthManager:IAuthManager
    {
        private readonly SignInManager<MarinAppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthManager(SignInManager<MarinAppUser> signInManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool rememberMe)
        {
           return await _signInManager.PasswordSignInAsync(userName, password, false, false);
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public string GenerateJwtToken(MarinAppUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken
            (
                issuer: _configuration["JwtIssuer"],
                audience: _configuration["JwtIssuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(_configuration["JwtKey"])),
                    SecurityAlgorithms.HmacSha256)
            );

            try
            {
                return new JwtSecurityTokenHandler().WriteToken(token);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}