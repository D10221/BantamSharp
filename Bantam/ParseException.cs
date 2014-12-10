using System;

namespace Bantam
{
    public class ParseException : Exception
    {
        public ParseException(Token token) : this("Troubles parsing \"{0}\".", token)
        {
            
        }
        public ParseException(string msg, params object[] parameters):base(string.Format(msg,parameters))
        {
            
        }
        
        public ParseException(string msg,Token token):base(string.Format(msg,token.GetText()))
        {
            
        }
        public ParseException(string message):base(message)
        {
            
        }
    }
}