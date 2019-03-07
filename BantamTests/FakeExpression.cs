using SimpleParser;

namespace BantamTests
{
    public class FakeExpression : ISimpleExpression
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