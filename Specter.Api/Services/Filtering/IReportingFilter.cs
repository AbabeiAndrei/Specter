namespace Specter.Api.Services.Filtering
{
    public interface IReportingFilter
    {
        IFilterItem User { get; }

        IFilterItem Project { get; }

        IFilterItem Delivery { get; }

        IFilterItem Category { get; }

        IFilterItem Date { get; }

        IFilterItem Time { get; }

        IFilterItem Text { get; }
    }

    public class ReportingFilter : IReportingFilter
    {
        public virtual IFilterItem User { get; set; }

        public virtual IFilterItem Project { get; set; }

        public virtual IFilterItem Delivery { get; set; }

        public virtual IFilterItem Category { get; set; }

        public virtual IFilterItem Date { get; set; }

        public virtual IFilterItem Time { get; set; }

        public virtual IFilterItem Text { get; set; }
    }
}