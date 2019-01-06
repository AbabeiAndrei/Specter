using System;

namespace Specter.Api.Data.Entities
{
    public class Timesheet : IRemovable
    {
        public virtual Guid Id { get; set; }

        public virtual int InternalId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual int Time { get; set; }

        public virtual string UserId { get; set; }

        public virtual Guid CategoryId { get; set; }

        public virtual Guid? DeliveryId { get; set; }

        public virtual bool Locked { get; set; }

        public virtual bool Removed { get; set; }
    }
}
