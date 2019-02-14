using System;

namespace Specter.Api.Models
{
    public class BaseTemplateModel
    {
        public virtual string Name { get; set; }

        public virtual string Data { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid? ForkId { get; set; }
    }

    public class TemplateModel : BaseTemplateModel
    {
        public virtual Guid Id { get; set; }
    }

    public class PublicTemplateModel : TemplateModel
    {
        public virtual Guid CreatedBy { get; set; }
    }
}