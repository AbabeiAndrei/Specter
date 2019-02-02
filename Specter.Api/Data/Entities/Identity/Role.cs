using System;
using System.Linq;

using Microsoft.AspNetCore.Identity;

namespace Specter.Api.Data.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public virtual IQueryable<UserTeamRole> Users { get; set; }
    }
}