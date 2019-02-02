using System;

namespace Specter.Api.Data.Entities
{
    public class ProjectTeam
    {
        public Guid ProjectId { get; set; }

        public Guid TeamId { get; set; }

        public Project Project { get; set; }

        public Team Team { get; set; }
    }
}