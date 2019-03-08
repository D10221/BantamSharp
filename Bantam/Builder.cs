using SimpleParser;
using System.Collections.Generic;
using System.Text;

namespace Bantam
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