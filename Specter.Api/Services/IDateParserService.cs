using System;
using System.Linq;

namespace Specter.Api.Services
{
    public interface IDateParserService
    {
        DateTime Parse(string date, bool includeTime = false);
        DateTime GetDate(DateTime date);
    }

    public class DateParserService : IDateParserService
    {
        public virtual DateTime Parse(string date, bool includeTime = false)
        {
            DateTime result;
            if(string.IsNullOrWhiteSpace(date))
                throw new ArgumentNullException(nameof(date));

            if(EqualsDate(date, "today", "now"))
                result = DateTime.Now;
            else if (EqualsDate(date, "yesterday"))
                result = DateTime.Now.AddDays(-1);
            else if (EqualsDate(date, "tomorrow"))
                result = DateTime.Now.AddDays(1);
            else
                result = DateTime.Parse(date);

            if(includeTime)
                return result;

            return GetDate(result);
        }

        public virtual DateTime GetDate(DateTime date)
        {
            return date.Date;
        }

        protected virtual bool EqualsDate(string first, params string[] equals)
        {
            return equals.Any(s => first.Equals(s, StringComparison.OrdinalIgnoreCase));
        }
    }
}