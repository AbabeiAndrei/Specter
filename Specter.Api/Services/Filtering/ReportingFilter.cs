namespace Specter.Api.Services.Filtering
{
    public interface IReportingFilter
    {
        string Users { get; }

        string Projects { get; }

        string Deliveries { get; }

        string Dates { get; }

        string Hours { get; }

        string Texts { get; }
    }

    public class ReportingFilter : IReportingFilter
    {
        public virtual string Users { get; set; }

        public virtual string Projects { get; set; }

        public virtual string Deliveries { get; set; }

        public virtual string Dates { get; set; }

        public virtual string Hours { get; set; }

        public virtual string Texts { get; set; }
    }
}