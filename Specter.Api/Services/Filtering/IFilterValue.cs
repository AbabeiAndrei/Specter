using System.Linq;
using System.Collections.Generic;
using System;

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

        public override string ToString()
        {
            if (Complex == null)
                return CreateStringFriendly(Value, Operation);

            var complexValues = Complex.OrderBy(fv => fv.Order)
                                       .Select(fv => fv.ToString())
                                       .Where(s => !string.IsNullOrEmpty(s));

            return string.Join(string.Empty, complexValues);
        }

        private string CreateStringFriendly(string value, Operation? operation)
        {
            return value + (operation.HasValue
                                ? $" {operation.Value} "
                                : string.Empty);
        }
    }
}