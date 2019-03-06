using SimpleParser;
using System.Collections.Generic;

namespace Bantam
{
    public class Builder : IBuilder<char>
    {
        private string _result;

        public IBuilder<char> Append(IEnumerable<char> s)
        {
            _result += s;
            return this;
        }

        public IBuilder<char> Append(char c)
        {
            _result += c.ToString();
            return this;
        }

        public string Build()
        {
            return _result;
        }
    }
}