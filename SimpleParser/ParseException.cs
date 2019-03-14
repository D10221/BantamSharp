using System;

namespace SimpleParser
{
    public class ParseException : Exception
    {
        public ParseException(string message) : base(message)
        {
        }
    }   
    public class ParseletException : Exception
    {
        public object Parselet { get; }

        public ParseletException(string message, object parselet) : base(message)
        {
            this.Parselet = parselet;
        }
    }
}