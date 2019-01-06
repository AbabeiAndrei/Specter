using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Specter.Api.Models
{
    public class ProjectModel
    {
        public virtual Guid Id { get; set; }

        [Required]
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }
        
        [Required]
        public virtual string WorkItemIdPrefix { get; set; }
    }

    public class ProjectExModel : ProjectModel
    {
        public virtual IEnumerable<DeliveryModel> Deliveries { get; set; }
    }
}