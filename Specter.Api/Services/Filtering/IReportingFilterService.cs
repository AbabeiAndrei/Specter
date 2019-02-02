using System.Linq;
using System.Collections.Generic;

using Specter.Api.Services.Filtering;

using System;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using Specter.Api.Extensions;

namespace Specter.Api.Services
{

    public interface IReportingFilterService
    {
        bool IsValid(string filter);
        IEnumerable<IReportingFilterError> Validate(string filter);
        IReportingFilter Parse(string filter, FilterDictionaryItemNotFoundHandler dictNotFoundHandler = null);
        bool TryParse(string filter, out IReportingFilter result, FilterDictionaryItemNotFoundHandler dictNotFoundHandler = null);
    }

    public class ReportingFilterService : IReportingFilterService
    {
        private readonly IFilterKeywordDictionary _dictionary;

        public ReportingFilterService(IFilterKeywordDictionary dictionary)
        {
            _dictionary = dictionary;
        }

        public bool IsValid(string filter)
        {
            return !Validate(filter).Any();
        }

        public IEnumerable<IReportingFilterError> Validate(string filter)
        {
            try
            {
                Parse(filter);

                return Enumerable.Empty<IReportingFilterError>();
            }
            catch(ParseFilterException ex)
            {
                return new []
                {
                    new ReportingFilterError(ex.Text, ex.Position, ex.Length, ex.Message)
                };
            }
            catch(ArgumentOutOfRangeException ex)
            {
                return new []
                {
                    new ReportingFilterError(string.Empty, 0, 0, ex.Message)
                };
            }
        }
        

        public IReportingFilter Parse(string filter, FilterDictionaryItemNotFoundHandler dictNotFoundHandler = null)
        {
            var parser = new FilterParser(filter, _dictionary);

            if(dictNotFoundHandler != null)
                parser.FilterDictionaryNotFound += dictNotFoundHandler;
                
            var reportFilter = new ReportingFilter();

            foreach(var filterItem in parser.Parse())
                AddToReportFilter(reportFilter, filterItem);

            return reportFilter;
        }

        public bool TryParse(string filter, out IReportingFilter result, FilterDictionaryItemNotFoundHandler dictNotFoundHandler = null)
        {
            try
            {
                result = Parse(filter, dictNotFoundHandler);
                return true;
            }
            catch(ParseReportFilterException)
            {
                result = null;
                return false;
            }
        }

        private void AddToReportFilter(ReportingFilter result, IFilterItem filterItem)
        {
            if(filterItem.Name.Equals(nameof(result.User), StringComparison.InvariantCultureIgnoreCase))
                result.User = filterItem;
            else if(filterItem.Name.Equals(nameof(result.Project), StringComparison.InvariantCultureIgnoreCase))
                result.Project = filterItem;
            else if(filterItem.Name.Equals(nameof(result.Delivery), StringComparison.InvariantCultureIgnoreCase))
                result.Delivery = filterItem;
            else if(filterItem.Name.Equals(nameof(result.Category), StringComparison.InvariantCultureIgnoreCase))
                result.Category = filterItem;
            else if(filterItem.Name.Equals(nameof(result.Date), StringComparison.InvariantCultureIgnoreCase))
                result.Date = filterItem;
            else if(filterItem.Name.Equals(nameof(result.Time), StringComparison.InvariantCultureIgnoreCase))
                result.Time = filterItem;
            else if(filterItem.Name.Equals(nameof(result.Text), StringComparison.InvariantCultureIgnoreCase))
                result.Text = filterItem;
            else 
                throw new ArgumentOutOfRangeException($"Undefined '{filterItem.Name}' report filter type");
        }
    }
}