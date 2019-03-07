using System;

namespace SimpleParser
{
    public class LexerException : Exception
    {
        public LexerException(string message, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}