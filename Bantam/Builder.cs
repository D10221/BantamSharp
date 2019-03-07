using SimpleParser;
using System.Collections.Generic;

namespace Bantam
{
    public class Builder : IBuilder
    {
        private string _result;

        public IBuilder Append(string s)
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