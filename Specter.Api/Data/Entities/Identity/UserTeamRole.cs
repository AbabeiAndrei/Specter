using System;

using Microsoft.AspNetCore.Identity;

namespace Specter.Api.Data.Entities
{
    public class UserTeamRole : IdentityUserRole<Guid>
    {
        public virtual Guid? TeamId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Role Role { get; set; }

        public virtual Team Team { get; set; }
    }
}