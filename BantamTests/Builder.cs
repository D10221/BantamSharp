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
        private readonly StringBuilder _builder = new StringBuilder();        

        public IBuilder Append(object c)
        {
            _builder.Append(c);
            return this;
        }

        public override string ToString()
        {
            return _builder.ToString();
        }
    }
}