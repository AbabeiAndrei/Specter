using System.Collections.Generic;

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
    }
}