using System;

namespace Specter.Api.Models
{
    public class TimesheetBaseModel
    {
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual string Category { get; set; }

        public virtual string Project { get; set; }

        public virtual string Delivery { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual float Time { get; set; }

        public virtual bool Locked { get; set; }

    }
    public class TimesheetModel : TimesheetBaseModel
    {
        public virtual Guid Id { get; set; }

        public virtual string InternalId { get; set; }
        
        public virtual Guid CategoryId { get; set; }

        public virtual Guid ProjectId { get; set; }

        public virtual Guid? DeliveryId { get; set; }
    }
}