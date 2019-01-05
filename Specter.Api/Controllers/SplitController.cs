using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using AutoMapper;

using Specter.Api.Models;
using Specter.Api.Data.Repository;
using Microsoft.Extensions.Configuration;

namespace Specter.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SplitController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SplitController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult<string> Get()
        {
            return _configuration.GetValue<string>("apiName") ?? "Split";
        }
    }
}