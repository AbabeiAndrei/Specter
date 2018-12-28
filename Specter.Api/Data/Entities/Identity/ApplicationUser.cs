using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;

namespace Specter.Api.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual string FirstName { get; set; }
        
        public virtual string LastName { get; set; }

        public ICollection<Template> Templates { get; set; }
        
        public ICollection<Timesheet> Timesheets { get; set; }
    }
}