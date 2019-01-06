using System;
using System.Collections.Generic;

namespace Specter.Api.Data.Entities
{
    public class Project : IRemovable
    {
        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }
        
        public virtual string Description { get; set; }

        public virtual string WorkItemIdPrefix { get; set; }
        
        public virtual bool Removed { get; set; }

        public virtual ICollection<Delivery> Deliveries { get; set; }

        public virtual ICollection<UserProject> Users { get; set; }

        public virtual ICollection<Timesheet> Timesheets { get; set; }
    }
}