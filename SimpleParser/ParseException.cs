using System;

namespace SimpleParser
{
    public class ParseException : Exception
    {        
        public ParseException(string msg, params object[] parameters) : base(string.Format(msg, parameters))
        {
        }
        
        public ParseException(string message) : base(message)
        {
        }
    }
}