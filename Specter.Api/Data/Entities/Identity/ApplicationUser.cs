using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;

namespace Specter.Api.Data.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual string FirstName { get; set; }
        
        public virtual string LastName { get; set; }

        public virtual ICollection<Template> Templates { get; set; }
        
        public virtual ICollection<Timesheet> Timesheets { get; set; }

        public virtual ICollection<UserProject> Projects { get; set; }
    }
}