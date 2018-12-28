using System;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

using Specter.Api.Data.Entities;

namespace Specter.Api.Services
{
    public interface IAuthenticationService
    {
        Task<string> Authenticate(ApplicationUser user, string password, bool persistent = false);
        Task SignOut();
        bool IsUserSignedIn(ClaimsPrincipal user);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthenticationService(SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<string> Authenticate(ApplicationUser user, string password, bool persistent = false)
        {
            var result = await _signInManager.PasswordSignInAsync(user, password, persistent, false);

            if(!result.Succeeded)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Secret"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool IsUserSignedIn(ClaimsPrincipal user)
        {
            return _signInManager.IsSignedIn(user);
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}