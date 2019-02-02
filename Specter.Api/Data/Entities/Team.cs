using System;
using System.Collections.Generic;

namespace Specter.Api.Data.Entities 
{
    public class Team : IRemovable
    {
        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual bool Removed { get; set; }
        
        public virtual ICollection<UserTeamRole> Users { get; set; }

        public virtual ICollection<ProjectTeam> Projects { get; set; }
    }
}