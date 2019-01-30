using System;

namespace Specter.Api.Data.Entities
{
    public class Timesheet : IRemovable
    {
        public virtual Guid Id { get; set; }

        public virtual int InternalId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual DateTime Created { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual float Time { get; set; }

        public virtual Guid UserId { get; set; }

        public virtual Guid CategoryId { get; set; }

        public virtual Guid ProjectId { get; set; }

        public virtual Guid? DeliveryId { get; set; }

        public virtual bool Locked { get; set; }

        public virtual bool Removed { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Project Project { get; set; }

        public virtual Delivery Delivery { get; set; }
        
        public virtual Category Category { get; set; }
        
        public string InternalIdMap => (Delivery?.Project?.WorkItemIdPrefix ?? Project.WorkItemIdPrefix) + InternalId.ToString().PadLeft(4, '0');
    }
}
