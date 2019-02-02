using System;
using System.Linq;

namespace Specter.Api.Data.Entities
{
    public class Project : IRemovable
    {
        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Code { get; set; }
        
        public virtual string Description { get; set; }

        public virtual string WorkItemIdPrefix { get; set; }
        
        public virtual bool Removed { get; set; }

        public virtual IQueryable<Delivery> Deliveries { get; set; }

        public virtual IQueryable<Timesheet> Timesheets { get; set; }
       
        public virtual IQueryable<ProjectTeam> Teams { get; set; }
    }
}