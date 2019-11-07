using System.Linq;

namespace uParserTests
{
    public class GroupExpression : ISimpleExpression
    {
        public Token Token { get; }
        public ISimpleExpression[] Elements { get; }

        public GroupExpression(Token token, ISimpleExpression[] elements)
        {
            Token = token;
            Elements = elements;
        }
        public override string ToString()
        {
            return Elements.Select(e => e.ToString()).Aggregate((a, b) => a + " " + b);
        }
    }
}
