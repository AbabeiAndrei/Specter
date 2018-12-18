using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Specter.Api.Data;
using Specter.Api.Data.Entities;

namespace Specter.Api.Services
{
    public class SpecterSeeder : ISeeder
    {
        private readonly UserManager<ApplicationUser> _manager;

        public SpecterSeeder(UserManager<ApplicationUser> manager)
        {
            _manager = manager;
        }

        public async Task Seed()
        {
            if(_manager.Users.Any())
                return;
            
            var user = new ApplicationUser
            {
                UserName = "admin",
                FirstName = "Andrei",
                LastName = "Admin",
                Email = "admin@specter.com"
            };

            var result = await _manager.CreateAsync(user, "123123Ab!");

            if(!result.Succeeded) 
                throw new Exception(result.Errors.First().Description); 
            
        }
    }
}