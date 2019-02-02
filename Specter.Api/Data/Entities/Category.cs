using System;
using System.Linq;

namespace Specter.Api.Data.Entities
{
    public class Category : IRemovable
    {
        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }
        
        public virtual bool Removed { get; set; }
        
        public virtual IQueryable<Timesheet> Timesheets { get; set; }
    }
}