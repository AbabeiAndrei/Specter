using System;

namespace Specter.Api.Data.Entities
{
    public class TemplateHistory
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Data { get; set; }

        public DateTime EditedAt { get; set; }

        public Guid TemplateId { get; set; }

        public Template Template { get; set; }

        public string Reason { get; set; }
    }
}
