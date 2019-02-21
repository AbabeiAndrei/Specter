using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

using AutoMapper;

using Specter.Api.Models;
using Specter.Api.Services;
using Specter.Api.Extensions;
using Specter.Api.Data.Entities;
using Specter.Api.Data.Repository;
using Specter.Api.Services.Filtering;

namespace Specter.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReportingController : ControllerBase
    {
        private readonly IReportingFilterService _reportingFilterService;
        private readonly ITimesheetRepository _timesheetRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ReportingController(IReportingFilterService reportingFilterService, 
                                   ITimesheetRepository timesheetRepository,
                                   UserManager<ApplicationUser> userManager, 
                                   IMapper mapper)
        {
            _reportingFilterService = reportingFilterService;
            _timesheetRepository = timesheetRepository;
            _userManager = userManager;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async virtual Task<ActionResult<ReportModel>> Get([FromQuery] string filter)
        {
            var errors = _reportingFilterService.Validate(filter, ReportingDictionaryItemHandler).ToList();
            if(errors.Count > 0)
                return BadRequest(errors);

            var repFilter = _reportingFilterService.Parse(filter, ReportingDictionaryItemHandler);

            var user = await _userManager.FindByIdAsync(User.Identity.Name);
            
            if(user == null)
                return Unauthorized();

            var timesheets = _timesheetRepository.GetAll();

            foreach(var filterExpr in CreateExpressionFilters(repFilter))
                timesheets = timesheets.Where(filterExpr);

            timesheets = timesheets.OrderBy(ts => ts.Date)
                                   .ThenBy(ts => ts.User.UserName)
                                   .ThenBy(ts => ts.Project.Name)
                                   .ThenBy(ts => ts.Delivery.Name)
                                   .ThenBy(ts => ts.Category.Name)
                                   .ThenBy(ts => ts.Created)
                                   .ThenBy(ts => ts.InternalId);                                   

            var report = new ReportModel
            {
                Filter = filter,
                Generated = DateTime.Now,
                Timesheets = timesheets.ToList().Select(_mapper.Map<TimesheetModel>)
            };

            return report;
        }

        private IEnumerable<Expression<Func<Timesheet, bool>>> CreateExpressionFilters(IReportingFilter filter)
        {
            if(filter == null)
                yield break;

            if(filter.User != null)
                yield return filter.User.ToExpression<Timesheet>((u, o) => ts => ts.User.UserName.ToLower() == u.ToLower());
                
            if(filter.Project != null)
                yield return filter.Project.ToExpression<Timesheet>((p, o)  => ts => ts.Project.Name.ToLower() == p.ToLower());

            if(filter.Delivery != null)
                yield return filter.Delivery.ToExpression<Timesheet>((d, o)  => ts => ts.Delivery.Name.ToLower() == d.ToLower());

            if(filter.Category != null)
                yield return filter.Category.ToExpression<Timesheet>((c, o)  => ts => ts.Category.Name.ToLower() == c.ToLower());

            if(filter.Text != null)
               yield return filter.Text.ToExpression<Timesheet>((t, o)  => ts => ts.Name.Contains(t, StringComparison.OrdinalIgnoreCase) ||
                                                                           ts.Description.Contains(t, StringComparison.OrdinalIgnoreCase));

            if(filter.Date != null)
                yield return filter.Date.ToExpression((d, o) => 
                {
                    var date = DateTime.Parse(d).Date;
                    var nextDate = date.AddDays(1);

                    return o.HasValue && o.Value == Operation.Until
                            ? (Expression<Func<Timesheet, bool>>) (ts => ts.Date >= date)
                            : ts => ts.Date <= nextDate;
                });

            if(filter.Time != null)
                yield return filter.Time.ToExpression((t, o)  =>
                {
                    var time = int.Parse(t);

                    return o.HasValue && o.Value == Operation.Until
                            ? (Expression<Func<Timesheet, bool>>) (ts => ts.Time >= time)
                            : ts => ts.Time <= time;
                });
        }

        private void ReportingDictionaryItemHandler(IFilterParser parser, IFilterKeywordDictionary dictionary, IFilterDictionaryItemNotFoundArgs args)
        {
            if(args.Dictionary == null || args.Keyword == null)
                return;

            if(args.Dictionary.Equals(nameof(IReportingFilter.User), StringComparison.OrdinalIgnoreCase) &&
               args.Keyword.Equals("Me", StringComparison.OrdinalIgnoreCase))
            {
                var user = _userManager.FindByIdAsync(User.Identity.Name).GetAwaiter().GetResult();

                if(user != null)
                    args.Value = user.UserName;   
            }
        }

    }
}