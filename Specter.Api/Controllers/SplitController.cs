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

namespace Specter.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SplitController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        
#if DEBUG
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IDeliveryRepository _deliveryRepository;
#endif

#if DEBUG
        public SplitController(IConfiguration configuration, 
                               ICategoryRepository categoryRepository, 
                               IProjectRepository projectRepository,
                               IDeliveryRepository deliveryRepository)
        {
            _configuration = configuration;
            _projectRepository = projectRepository;
            _categoryRepository = categoryRepository;
            _deliveryRepository = deliveryRepository;
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
            if(_projectRepository.GetAll().Any())
                return BadRequest("Database is already seeded");

            #region Categories

            var cat1 = new Category
            {
                Name = "Elaboration",
                Description = "Elaboration phase of task"
            };
            var cat2 = new Category
            {
                Name = "Construction",
                Description = "Construction phase of task"
            };
            var cat3 = new Category
            {
                Name = "Testing",
                Description = "Testing phase of task"
            };
            var cat4 = new Category
            {
                Name = "Other",
                Description = "Other phase of task"
            };

            _categoryRepository.Insert(cat1);
            _categoryRepository.Insert(cat2);
            _categoryRepository.Insert(cat3);
            _categoryRepository.Insert(cat4);

            #endregion

            #region Projects

            var proj1 = new Project
            {
                Name = "Minecraft",
                Code = "MCRAFT",
                Description = "New frontend for SICLID",
                WorkItemIdPrefix = "MC"
            };
            
            var proj2 = new Project
            {
                Name = "Jetix",
                Code = "JETIX",
                Description = "Current frontend for SICLID",
                WorkItemIdPrefix = "JTX"
            };
            
            var proj3 = new Project
            {
                Name = "Other",
                Code = "INTERNAL",
                Description = "Other projects or work related items",
                WorkItemIdPrefix = "IBM"
            };

            _projectRepository.Insert(proj1);
            _projectRepository.Insert(proj2);
            _projectRepository.Insert(proj3);

            #endregion

            #region Deliveries

            var del1 = new Delivery
            {
                Name = "Minecraft 1.0",
                Order = 0,
                ProjectId = proj1.Id
            };
            var del2 = new Delivery
            {
                Name = "Minecraft 2.0",
                Order = 1,
                ProjectId = proj1.Id
            };
            var del3 = new Delivery
            {
                Name = "Minecraft 2.1",
                Order = 2,
                ProjectId = proj1.Id
            };
            var del4 = new Delivery
            {
                Name = "Minecraft 3.0",
                Order = 3,
                ProjectId = proj1.Id
            };
            var del5 = new Delivery
            {
                Name = "Jetix 2.1",
                Order = 0,
                ProjectId = proj2.Id
            };
            var del6 = new Delivery
            {
                Name = "Jetix 2.2",
                Order = 1,
                ProjectId = proj2.Id
            };

            _deliveryRepository.Insert(del1);
            _deliveryRepository.Insert(del2);
            _deliveryRepository.Insert(del3);
            _deliveryRepository.Insert(del4);
            _deliveryRepository.Insert(del5);
            _deliveryRepository.Insert(del6);

            #endregion

            return Ok();
        }
#endif
    }
}