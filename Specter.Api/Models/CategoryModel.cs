using System;

namespace Specter.Api.Models
{
    public class CategoryModel
    {
        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }
    }
}