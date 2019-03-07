using System;

namespace SimpleParser
{
    public class ParseException<TTokenType> : Exception
    {
        public ParseException(IToken<TTokenType> token) : this("Troubles parsing \"{0}\".", token)
        {
        }
        public ParseException(string msg, params object[] parameters) : base(string.Format(msg, parameters))
        {
        }

        public ParseException(string msg, IToken<TTokenType> token) : base(string.Format(msg, token.Text))
        {
        }
        public ParseException(string message) : base(message)
        {
        }
    }
}