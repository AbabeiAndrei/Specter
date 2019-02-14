using System;
using System.Linq;

namespace Specter.Api.Data.Entities
{
    public enum Visibility : short
    {
        Public = 0,
        Team = 1,
        Private = 2
    }

    public class Template
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Data { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public Visibility Visibility { get; set; }

        public Guid? ForkId { get; set; }

        public bool Deleted { get; set; }

        public ApplicationUser CreatedByUser { get; set; }

        public Template ForkTemplate { get; set; }

        public IQueryable<Template> Forks { get; set; }

        public IQueryable<TemplateHistory> Edits { get; set; }
    }
}
