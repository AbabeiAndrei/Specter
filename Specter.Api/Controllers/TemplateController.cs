using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

using AutoMapper;

using Specter.Api.Models;
using Specter.Api.Data.Entities;
using Specter.Api.Data.Repository;

namespace Specter.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TemplateController : ControllerBase
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public TemplateController(ITemplateRepository templateRepository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _templateRepository = templateRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TemplateModel>>> Get()
        {
            var user = await _userManager.FindByIdAsync(User.Identity.Name);

            if(user == null)
                return Unauthorized();

            var result = _templateRepository.GetForUser(user.Id);

            return Ok(result.Select(_mapper.Map));
        }
    }   
}