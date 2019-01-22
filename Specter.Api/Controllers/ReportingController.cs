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
using Specter.Api.Data.Entities;
using System.Reflection;
using Specter.Api.Services;

namespace Specter.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReportingController : ControllerBase
    {
        private readonly IReportingFilterService _reportingFilterService;

        public ReportingController(IReportingFilterService reportingFilterService)
        {
            _reportingFilterService = reportingFilterService;
        }
        
        [HttpGet]
        public virtual ActionResult<ReportModel> Get([FromQuery] string filter)
        {
            if(!_reportingFilterService.IsValid(filter))
                return BadRequest("Incorect filter value");

            return null;
        }
    }
}