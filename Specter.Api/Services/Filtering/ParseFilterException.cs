using System;

namespace Specter.Api.Services.Filtering
{
    [Serializable]
    internal sealed class ParseFilterException : Exception
    {
        public int Length { get; set; }
        
        public int Position { get; private set; }

        public string Text { get; set; }


        public ParseFilterException(int position)
        {
            Position = position;
        }

        public ParseFilterException(int position, string message) : base(message)
        {
            Position = position;
        }

        public ParseFilterException(int position, string message, Exception innerException) : base(message, innerException)
        {
            Position = position;
        }
    }
}