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
using Specter.Api.Exceptions;
using Specter.Api.Data.Entities;
using Specter.Api.Data.Repository;

namespace Specter.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TimesheetController : ControllerBase
    {
        private const int MIN_YEAR = 2000;

        private readonly IMapper _mapper;
        private readonly IDateParserService _dateParserService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITimesheetRepository _timesheetRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ITimesheetIdCalculator _timesheetIdCalculator;

        public TimesheetController(IMapper mapper, 
                                   IDateParserService dateParserService, 
                                   UserManager<ApplicationUser> userManager, 
                                   ITimesheetRepository timesheetRepository,
                                   ICategoryRepository categoryRepository,
                                   IDeliveryRepository deliveryRepository,
                                   IProjectRepository projectRepository,
                                   ITimesheetIdCalculator timesheetIdCalculator)
        {
            _mapper = mapper;
            _dateParserService = dateParserService;
            _userManager = userManager;
            _timesheetRepository = timesheetRepository;
            _categoryRepository = categoryRepository;
            _deliveryRepository = deliveryRepository;
            _projectRepository = projectRepository;
            _timesheetIdCalculator = timesheetIdCalculator;
        }

        [Authorize]
        [HttpGet]
        public Task<ActionResult<IEnumerable<TimesheetModel>>> Get()
        {
            return Get("today");
        }
        
        [Authorize]
        [HttpGet("{date}")]
        public Task<ActionResult<IEnumerable<TimesheetModel>>> Get(string date)
        {
            return Get(new DateFilterIntervalModel(date, date));
        }
        
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimesheetModel>>> Get([FromBody] DateFilterIntervalModel date)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var tsFrom = _dateParserService.Parse(date.From);
            var tsTo = _dateParserService.Parse(date.To).AddDays(1);

            var dateNow = _dateParserService.GetDate(DateTime.Now);

            if(tsFrom.Year < MIN_YEAR || tsTo.Year < MIN_YEAR)
                return BadRequest("Date to old");

            if(tsFrom > tsTo)
                return BadRequest("Invalid interval");

            var user = await _userManager.FindByIdAsync(User.Identity.Name);

            if(user == null)
                return Unauthorized();

            var result = _timesheetRepository.GetAll()
                                             .Where(ts => ts.UserId == user.Id && ts.Date >= tsFrom && ts.Date <= tsTo);
        
            var models = result.ToList().Select(_mapper.Map<TimesheetModel>).ToList();

            foreach(var model in models)
            {
                var category = _categoryRepository.GetById(model.CategoryId);
                model.Category = category?.Name; 

                if(model.DeliveryId.HasValue)
                {
                    var delivery = _deliveryRepository.GetById(model.DeliveryId.Value);
                    model.Delivery = delivery?.Name;
                }
                
                var project = _projectRepository.GetById(model.ProjectId);
                model.Project = project?.Name;
            }

            return Ok(models);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<TimesheetModel>> Get(Guid id)
        {
            var user = await _userManager.FindByIdAsync(User.Identity.Name);

            if(user == null)
                return Unauthorized();

            var ts = _timesheetRepository.GetById(id);

            if(ts == null)
                return NotFound();

            if(ts.UserId != user.Id)
                return Unauthorized();

            return Ok(_mapper.Map<TimesheetModel>(ts));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<TimesheetModel>> Post([FromBody] TimesheetUpdateModel model)
        {
            var user = await _userManager.FindByIdAsync(User.Identity.Name);

            if(user == null)
                return Unauthorized();

            var ts = _mapper.Map<Timesheet>(model);

            if(ts == null)
                return BadRequest();

            Guid projectId;

            if(ts.DeliveryId.HasValue && ts.DeliveryId.Value != Guid.Empty)
            {
                var delivery = _deliveryRepository.GetById(ts.DeliveryId);

                if(delivery == null)
                    return BadRequest();

                projectId = delivery.ProjectId;
            }
            else if(model.ProjectId != Guid.Empty)
                projectId = model.ProjectId;
            else
                return BadRequest(Errors.Timesheet.DeliveryOrProjectNotProvided);

            ts.UserId = user.Id;
            ts.InternalId = _timesheetIdCalculator.Calculate(projectId);

            _timesheetRepository.Insert(ts);

            return CreatedAtAction(nameof(Get), ts.Id, _mapper.Map<TimesheetModel>(ts));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] TimesheetUpdateModel model)
        {
            var user = await _userManager.FindByIdAsync(User.Identity.Name);

            if(user == null)
                return Unauthorized();

            var ts = _timesheetRepository.GetById(id);

            if(ts == null)
                return NotFound();

            if(ts.Locked)
                return BadRequest(Errors.Timesheet.TimesheetIsLocked);

            if(ts.UserId != user.Id)
                return Unauthorized();

            ts.Name = model.Name;
            ts.Description = model.Description;
            ts.Date = model.Date;
            ts.Time = model.Time;
            ts.Locked = model.Locked;

            _timesheetRepository.Update(ts);

            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(User.Identity.Name);

            if(user == null)
                return Unauthorized();

            var ts = _timesheetRepository.GetById(id);

            if(ts == null)
                return NotFound();

            if(ts.Locked)
                return BadRequest(Errors.Timesheet.TimesheetIsLocked);

            if(ts.UserId != user.Id)
                return Unauthorized();

            _timesheetRepository.Delete(ts);

            return Ok();
        }
    }
}
