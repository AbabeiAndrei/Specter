using System;

namespace Specter.Api.Data.Entities
{
    public class UserProject : IRemovable
    {
        public virtual Guid UserId { get; set; }

        public virtual Guid ProjectId { get; set; }

        public virtual Guid RoleId { get; set; }
        
        public virtual bool Removed { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Project Project { get; set; }

    }
}