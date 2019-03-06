using SimpleParser;
using IBuilder = SimpleParser.IBuilder<char>;

namespace BantamTests
{
    public class FakeExpression : ISimpleExpression<char>
    {
        private readonly string _what;

        public FakeExpression(string what)
        {
            _what = what;
        }

        public void Print(IBuilder builder)
        {
            builder.Append(_what);
        }
    }
}