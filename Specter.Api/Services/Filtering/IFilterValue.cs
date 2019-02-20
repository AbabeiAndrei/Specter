using System.Collections.Generic;

namespace Specter.Api.Services.Filtering
{
    public enum Operation 
    {
        Or,
        And,
        Until
    }
    public interface IFilterValue
    {
        Operation? Operation { get; }
        int Order { get; }
        string Value { get; }
        IEnumerable<IFilterValue> Complex { get; }	
    }

    public class FilterValue : IFilterValue
    {
        
        public virtual Operation? Operation { get; set; }
        public virtual int Order{ get; set; }
        public virtual string Value { get; set; }
        public virtual IEnumerable<IFilterValue> Complex { get; set; }	
    }
}