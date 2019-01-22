using System;

namespace Specter.Api.Services.Filtering
{
    public interface IReportingFilterError
    {
        string Text { get; }
        int Position { get; }
        int Length { get; }
        string Error { get; }
        int ErrorCode { get; }
        string Description { get; }
    }

    public class ReportingFilterError : IReportingFilterError
    {
        private readonly string _text;
        private readonly int _position;
        private readonly int _length;
        private readonly string _error;
        private int _errorCode;
        private string _description;

        public virtual string Text => _text;
        public virtual int Position => _position;
        public virtual int Length => _length;
        public virtual string Error => _error;
        public virtual int ErrorCode 
        {
            get => _errorCode;
            set => _errorCode = value;
        }
        public virtual string Description
        {
            get => _description;
            set => _description = value;
        }

        public ReportingFilterError(string text, int position, int length, string error)
        {
            _text = text ?? throw new ArgumentNullException(nameof(text));
            _position = position;
            _length = length;
            _error = error ?? throw new ArgumentNullException(nameof(error));
        }
    }
}