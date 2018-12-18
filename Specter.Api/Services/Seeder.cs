using System.Linq;
using System.Threading.Tasks;
using Specter.Api.Data;

namespace Specter.Api.Services
{
    public class SpecterSeeder : ISeeder
    {
        private readonly IApplicationContext _context;

        public SpecterSeeder(IApplicationContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            if(_context.Users.Any())
                return;

            using(var store = new UserStore<ApplicationUser>(_context))
            using(var manager = new UserManager<ApplicationUser>(store))
            {
                var user = new ApplicationUser
                {
                    FirstName = "Andrei",
                    LastName = "Admin",
                    Email = "admin@specter.com"
                };

                var result = await manager.CreateAsync(user, "1");

                if(!result.Succeeded) 
                    throw new Exception(result.Errors.First()); 
            }
        }
    }
}