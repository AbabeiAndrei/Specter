using System;
using System.Collections.Generic;

namespace Specter.Api.Services.Filtering
{
    [Serializable]
    internal sealed class ParseReportFilterException : Exception
    {
        public IEnumerable<IReportingFilterError> Errors { get; set; }

        public ParseReportFilterException(IEnumerable<IReportingFilterError> errors)
        {
            Errors = errors;
        }

        public ParseReportFilterException(IEnumerable<IReportingFilterError> errors, string message) : base(message)
        {
            Errors = errors;
        }

        public ParseReportFilterException(IEnumerable<IReportingFilterError> errors, string message, Exception innerException) : base(message, innerException)
        {
            Errors = errors;
        }
    }
}