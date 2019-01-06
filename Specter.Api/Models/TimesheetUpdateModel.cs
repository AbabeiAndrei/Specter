using System;

namespace Specter.Api.Models
{
    public class TimesheetUpdateModel : TimesheetBaseModel
    {
        public virtual Guid CategoryId { get; set; }

        public virtual Guid? DeliveryId { get; set; }

        public virtual Guid ProjectId { get; set; }
    }
}