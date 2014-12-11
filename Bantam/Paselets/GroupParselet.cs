using SimpleParser;

namespace Bantam.Paselets
{
    /// <summary>
    /// Parses parentheses used to group an expression, like "a * (b + c)".
    /// </summary>
    public class GroupParselet : IPrefixParselet
    {
        public ISimpleExpression Parse(IParser parser, IToken token)
        {
            var simpleExpression = parser.ParseExpression();
            parser.Consume(TokenType.RIGHT_PAREN);
            return simpleExpression;
        }
    }
}
