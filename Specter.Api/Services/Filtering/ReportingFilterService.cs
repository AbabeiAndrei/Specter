using System.Linq;
using System.Collections.Generic;

using Specter.Api.Services.Filtering;

using Flee;
using Flee.PublicTypes;
using System;
using System.Text.RegularExpressions;

namespace Specter.Api.Services
{
    public interface IReportingFilterService
    {
        bool IsValid(string filter);
        IEnumerable<IReportingFilterError> Validate(string filter);
        IReportingFilter Parse(string filter);
        bool TryParse(string filter, out IReportingFilter result);
    }

    public class ReportingFilterService : IReportingFilterService
    {
        public bool IsValid(string filter)
        {
            return !Validate(filter).Any();
        }

        public IEnumerable<IReportingFilterError> Validate(string filter)
        {
            var filterInterpreded = InterpretFilter(filter);
            return null;
        }

        private string InterpretFilter(string filter, out IEnumerable<string> variables)
        {
            var vars = new List<string>();
            var newFilter = filter;

            foreach(Match match in Regex.Matches(filter, "\"([^\"]*)\""))
            {
                var variable = match.Value.Trim('"');
                newFilter = newFilter.Replace(match.Value, variable);

                vars.Add(variable);
            }

            variables = vars;

            return newFilter;
        }

        public IReportingFilter Parse(string filter)
        {
            var validateResult = Validate(filter).ToList();

            if(validateResult.Count > 0)
                throw new ParseReportFilterException(validateResult);

            return null;
        }

        public bool TryParse(string filter, out IReportingFilter result)
        {
            try
            {
                result = Parse(filter);
                return true;
            }
            catch(ParseReportFilterException)
            {
                result = null;
                return false;
            }
        }
    }
}