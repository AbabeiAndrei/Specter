using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Specter.Api.Model;

namespace Specter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
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
        public ActionResult<UserModel> Authenticate([FromBody] LoginModel model) 
        {
            if(string.Equals(model.Email, "admin@specter.com", StringComparison.InvariantCultureIgnoreCase) && 
               model.Password == "1")
                return new UserModel
                {
                    Id = 1,
                    Email = "admin@specter.com",
                    FirstName = "Andrei",
                    LastName = "Ababei",
                    Token = Guid.NewGuid().ToString()
                };

            return NotFound();
        }
    }
}
