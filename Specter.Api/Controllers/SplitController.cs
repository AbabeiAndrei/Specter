using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Microsoft.Extensions.Configuration;
using System.Reflection;
using Specter.Api.Services;

namespace Specter.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SplitController : ControllerBase
    {
        private readonly IConfiguration _configuration;

#if DEBUG
        private readonly ITestDataSeeder _testDataSeeder;
#endif

#if DEBUG
        public SplitController(IConfiguration configuration, ITestDataSeeder testDataSeeder)
        {
            _configuration = configuration;
            _testDataSeeder = testDataSeeder;
        }
#else
        public SplitController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
#endif
        [HttpGet("name")]
        [AllowAnonymous]
        public virtual ActionResult<string> Get()
        {
            return _configuration.GetValue<string>("apiName") ?? "Split";
        }

        [HttpGet("version")]
        [AllowAnonymous]
        public virtual ActionResult<string> Version()
        {
            return _configuration.GetValue<string>("version") ?? Assembly.GetExecutingAssembly().GetName().Version.ToString(2);
        }

#if DEBUG
        [HttpPost("seed")]
        [AllowAnonymous]
        public virtual ActionResult Seed()
        {
            if(!_testDataSeeder.CanSeed)
                return BadRequest("Database is already seeded");

            _testDataSeeder.SeedTestData();

            return Ok();
        }
#endif
    }
}