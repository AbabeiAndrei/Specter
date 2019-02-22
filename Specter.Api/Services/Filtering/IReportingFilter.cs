using System.Linq;
using System.Text;

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

        public override string ToString()
        {
            var filters = new[]
            {
                User,
                Project,
                Delivery,
                Category,
                Date,
                Time,
                Text
            }.Where(fi => fi != null)
            .Select(fi => fi.ToString())
            .Where(s => !string.IsNullOrEmpty(s))
            .ToList();

            if (filters.Count == 0)
                return "Full report";

            var sb = new StringBuilder("Report");

            sb.Append(" for ");

            sb.Append(string.Join(", ", filters));

            return sb.ToString();
        }
    }
}