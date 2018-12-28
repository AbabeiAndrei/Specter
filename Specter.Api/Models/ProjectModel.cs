using System;
using System.Collections.Generic;

namespace Specter.Api.Models
{
    public class ProjectModel
    {
        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }
    }

    public class ProjectExModel : ProjectModel
    {
        public virtual IEnumerable<DeliveryModel> Deliveries { get; set; }
    }
}