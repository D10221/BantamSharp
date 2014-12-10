using System.Text;
using Bantam.Expressions;

namespace BantamTests
{
    public class Builder:IBuilder
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

        public IBuilder Append(char c)
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