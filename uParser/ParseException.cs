using System;
using System.Runtime.Serialization;

namespace uParser
{
    [Serializable]
    public class ParseException : Exception
    {
        private object p;

        public ParseException()
        {
        }

        public ParseException(object p)
        {
            this.p = p;
        }

        public ParseException(string message) : base(message)
        {
        }

        public ParseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ParseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}