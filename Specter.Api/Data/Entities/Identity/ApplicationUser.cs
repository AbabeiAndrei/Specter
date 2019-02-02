using System;
using System.Linq;

using Microsoft.AspNetCore.Identity;

namespace Specter.Api.Data.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual string FirstName { get; set; }
        
        public virtual string LastName { get; set; }

        public virtual ApplicatioUserPreferences Preferences { get; set; }

        public virtual IQueryable<Template> Templates { get; set; }
        
        public virtual IQueryable<Timesheet> Timesheets { get; set; }

        public virtual IQueryable<UserTeamRole> Roles { get; set; }

        public virtual IQueryable<Team> Teams => Roles?.Select(r => r.Team);
    }
}