using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

using AutoMapper;

using Specter.Api.Models;
using Specter.Api.Services;
using Specter.Api.Data.Entities;

namespace Specter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly UserManager<ApplicationUser> _manager;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IUrlCreatorService _urlCreatorService;
        private readonly IEmailTemplateBuilder _emailTemplateBuilder;

        public UsersController(IAuthenticationService authenticationService, UserManager<ApplicationUser> manager, IMapper mapper, IEmailService emailService, IUrlCreatorService urlCreatorService, IEmailTemplateBuilder emailTemplateBuilder)
        {
            _authenticationService = authenticationService;
            _manager = manager;
            _mapper = mapper;
            _emailService = emailService;
            _urlCreatorService = urlCreatorService;
            _emailTemplateBuilder = emailTemplateBuilder;
        }

        // GET api/values
        [AllowAnonymous]
        [HttpGet("profile/{email}")]
        public async Task<ActionResult<UserModel>> Profile([FromRoute] string email)
        {
            var user = await _manager.FindByEmailAsync(email);

            if(user == null)
                return NotFound();

            return Ok(_mapper.Map<UserModel>(user));
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<LoginUserModel>> Authenticate([FromBody] LoginModel model) 
        {
            var user = await _manager.FindByEmailAsync(model.Email);

            if(user == null)
                return NotFound();

            var token = await _authenticationService.Authenticate(user, model.Password, model.Persist);

            if(string.IsNullOrWhiteSpace(token))
                return NotFound();

            var result = _mapper.Map<LoginUserModel>(user);

            result.Token = token;

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("logout")]
        public async Task<ActionResult> SignOut()
        {
            await _authenticationService.SignOut();

            return Ok();
        }
    
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> Register([FromBody] RegisterModel model)
        {
            if(model == null)
                return BadRequest();

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };

            var result = await _manager.CreateAsync(user, model.Password);

            var token = await _manager.GenerateEmailConfirmationTokenAsync(user);

            var url = _urlCreatorService.User.ActivateUserLink(token);

            var emailTemplate = _emailTemplateBuilder.BuildTemplate(EmailTemplates.ActivateUser, user.FirstName, url);

            await _emailService.SendEmailAsync(model.Email, emailTemplate);

            if(!result.Succeeded)
                return BadRequest(result);

            return CreatedAtAction("Profile", _mapper.Map<UserModel>(user));
        }
    
        [AllowAnonymous]
        [HttpPost("forgot")]
        public async Task<ActionResult> ForgotPassword([FromBody] string email)
        {
            var user = await _manager.FindByEmailAsync(email);

            if(user == null)
                return NotFound();

            var token = await _manager.GeneratePasswordResetTokenAsync(user);

            var url = _urlCreatorService.User.CreateResetPasswordLink(token);

            var emailTemplate = _emailTemplateBuilder.BuildTemplate(EmailTemplates.UserResetPassword, url);

            var result = await _emailService.SendEmailAsync(email, emailTemplate);

            if(result)
                return Ok();
            
            return StatusCode(500);
        }
    
        [AllowAnonymous]
        [HttpPost("reset")]
        public async Task<ActionResult> ResetPassword([FromBody] PasswordResetModel model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _manager.FindByEmailAsync(model.Email);

            if(user == null)
                return NotFound();

            var result = await _manager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            if(!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }
    
        [AllowAnonymous]
        [HttpPost("activate")]
        public async Task<ActionResult> Activate([FromBody] ActivateUserModel model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _manager.FindByEmailAsync(model.Email);

            if(user == null)
                return NotFound();

            var result = await _manager.ConfirmEmailAsync(user, model.Token);

            if(!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }

        [Authorize]
        [HttpPost("profile")]
        public async Task<ActionResult<UserModel>> Update([FromBody] UserUpdateModel model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!_authenticationService.IsUserSignedIn(User))
                return Unauthorized();

            var user = await _manager.GetUserAsync(User);

            if(user == null)
                return NotFound();

            if(!string.IsNullOrEmpty(model.CurrentPassword) && !string.IsNullOrEmpty(model.NewPassword))
            {
                var changePasswordResult = await _manager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                if(!changePasswordResult.Succeeded)
                    return BadRequest(changePasswordResult.Errors);
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Preferences = _mapper.Map<ApplicatioUserPreferences>(model.Preferences);

            var result = await _manager.UpdateAsync(user);
            
            if(!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }

        
        [Authorize]
        [HttpPatch("darkMode")]
        public async Task<ActionResult<UserModel>> ToggleDarkMode([FromBody] bool useDarkMode)
        {
            var user = await _manager.FindByIdAsync(User.Identity.Name);

            if(user == null)
                return NotFound();

            user.Preferences.DarkMode = useDarkMode;

            var result = await _manager.UpdateAsync(user);
            
            if(!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }
    }
}
