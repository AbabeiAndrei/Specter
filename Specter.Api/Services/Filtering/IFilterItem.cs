using System.Linq;
using System.Text;
using System.Collections.Generic;

using Specter.Api.Extensions;

namespace Specter.Api.Services.Filtering
{
    public interface IFilterItem
    {
        string Name { get; }

        IEnumerable<IFilterValue> Values { get; }
    }

    public class FilterItem : IFilterItem
    {
        public virtual string Name { get; set; }
        
        public virtual IEnumerable<IFilterValue> Values { get; set; }

        public override string ToString()
        {
            var filters = Values as IList<IFilterValue> ?? Values.ToList();

            if (filters.Count <= 0)
                return string.Empty;

            var sb = new StringBuilder(Name.ToTitleCase() + ": ");

            var values = filters.OrderBy(fv => fv.Order)
                                .Select(fv => fv.ToString())
                                .Where(s => !string.IsNullOrEmpty(s));

            sb.Append(string.Join(string.Empty, values));

            return sb.ToString();
        }
    }
}