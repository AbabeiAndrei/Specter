using System;
using System.ComponentModel.DataAnnotations;

namespace Specter.Api.Models
{
    public class CategoryModel
    {
        public virtual Guid Id { get; set; }

        [Required]
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }
    }
}