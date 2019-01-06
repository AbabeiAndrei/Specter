using System;
using System.ComponentModel.DataAnnotations;

namespace Specter.Api.Models
{
    public class DeliveryModel
    {
        public virtual Guid Id { get; set; }

        [Required]
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual string ProjectId { get; set; }

        [Required]
        public virtual int Order { get; set; }
    }

    public class DeliveryExModel : DeliveryModel
    {
        public virtual string Project { get; set; }
    }
}