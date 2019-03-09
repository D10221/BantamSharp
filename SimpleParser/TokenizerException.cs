using System;

namespace SimpleParser
{
    internal class TokenizerException : Exception
    {
        public TokenizerException()
        {
        }

        public TokenizerException(string message) : base(message)
        {
        }

        public TokenizerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}