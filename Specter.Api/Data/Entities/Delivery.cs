using System;
using System.Collections.Generic;

namespace Specter.Api.Data.Entities
{
    public class Delivery : IRemovable
    {
        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }
        
        public virtual string Description { get; set; }

        public virtual int Order { get; set; }

        public virtual Guid ProjectId { get; set; }
        
        public virtual bool Removed { get; set; }

        public virtual Project Project { get; set; }

        public virtual ICollection<Timesheet> Timesheets { get; set; }
    }
}