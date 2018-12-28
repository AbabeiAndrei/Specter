using System;

namespace Specter.Api.Data.Entities
{
    public class Project : IRemovable
    {
        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }
        
        public virtual string Description { get; set; }
        
        public virtual bool Removed { get; set; }
    }
}