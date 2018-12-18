using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using Specter.Api.Model;
using Specter.Api.Data.Entities;

namespace Specter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signinManager;

        public UsersController(SignInManager<ApplicationUser> signinManager)
        {
            _signinManager = signinManager;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<UserModel>> Authenticate([FromBody] LoginModel model) 
        {
            var user = _signinManager.UserManager.Users.FirstOrDefault(au => au.Email == model.Email);

            if(user == null)
                return NotFound();

            var signinResult = await _signinManager.PasswordSignInAsync(user, model.Password, model.Persist, true);

            if(!signinResult.Succeeded)
                return NotFound();

            var result = new UserModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = Guid.NewGuid().ToString() //todo
            };

            return Ok(result);
        }
    }
}
