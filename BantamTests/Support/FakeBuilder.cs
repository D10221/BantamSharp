using SimpleParser;

namespace BantamTests.Support
{
    public class FakeBuilder : IBuilder
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

        public  string Build()
        {
            return _result;
        }
    }
}