using System.Collections.Generic;
using System.Text;
using SimpleParser;

namespace BantamTests
{
    /// <summary>
    /// Abstraction :)
    /// </summary>
    public class Builder : IBuilder
    {
        private readonly StringBuilder _builder;

        public Builder()
        {
            _builder = new StringBuilder();
        }
        
        public IBuilder Append(string s)
        {
            _builder.Append(s);
            return this;
        }

        public IBuilder Append(object c)
        {
            _builder.Append(c);
            return this;
        }

        public string Build()
        {
            return _builder.ToString();
        }
    }
}