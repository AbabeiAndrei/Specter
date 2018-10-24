using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Specter.DataLayer.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Template> Templates { get; set; }
        
        public ICollection<Timesheet> Timesheets { get; set; }
    }
}
