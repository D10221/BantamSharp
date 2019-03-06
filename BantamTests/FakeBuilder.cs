using System.Collections.Generic;
using IBuilder = SimpleParser.IBuilder<char>;

namespace BantamTests
{
    public class FakeBuilder : IBuilder
    {
        private string _result;

        public IBuilder Append(IEnumerable<char> s)
        {
            _result += s;
            return this;
        }

        public IBuilder Append(char c)
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