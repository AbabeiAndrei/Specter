using System;
using System.Collections.Generic;

namespace Specter.Api.Models 
{
    public abstract class BaseReportModel
    {
        public virtual string Filter { get; set; }

        public virtual string FilterText { get; set; }

        public virtual DateTime Generated { get; set; }
    }

    public class ReportModel : BaseReportModel
    {
        public virtual IEnumerable<TimesheetBaseModel> Timesheets { get; set; }
    }

    public class ReportGroupedModel : BaseReportModel
    {
        public virtual IEnumerable<ReportUserGroupingModel> Users { get; set; }
    }

    public class ReportUserGroupingModel
    {
        public virtual UserModel User { get; set; }

        public virtual IEnumerable<ReportProjectGroupingModel> Projects { get; set; }
    }

    public class ReportProjectGroupingModel
    {
        public virtual ProjectModel Project { get; set; }

        public virtual IEnumerable<ReportDeliveryGroupingModel> Deliveries { get; set; }
    }

    public class ReportDeliveryGroupingModel
    {
        public virtual DeliveryModel Delivery { get; set; }

        public virtual IEnumerable<ReportCategoryGroupingModel> Categories { get; set; }
    }

    public class ReportCategoryGroupingModel
    {
        public virtual CategoryModel Category { get; set; }

        public virtual IEnumerable<ReportItemModel> Items { get; set; }
    }

    public class ReportItemModel
    {
        public virtual TimesheetBaseModel Timesheet { get; set; }
    }
}