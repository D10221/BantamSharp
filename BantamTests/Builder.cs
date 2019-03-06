using System.Collections.Generic;
using System.Text;
using SimpleParser;
using IBuilder = SimpleParser.IBuilder<char>;

namespace BantamTests
{
    /// <summary>
    /// Abstraction :)
    /// </summary>
    public class Builder:IBuilder
    {
        private readonly StringBuilder _builder;

        public Builder()
        {
            _builder = new StringBuilder();
        }

        public IBuilder Append(IEnumerable<char> s)
        {
            _builder.Append(s);
            return this;
        }

        public IBuilder Append(char c)
        {
            _builder.Append(c);
            return this;
        }

        public  string Build()
        {
            return _builder.ToString();
        }
    }
}