using SimpleParser;
using IParser = SimpleParser.IParser<SimpleParser.TokenType>;
using IPrefixParselet = SimpleParser.IPrefixParselet<SimpleParser.TokenType>;
namespace Bantam.Paselets
{
    /// <summary>
    /// Parses parentheses used to group an expression, like "a * (b + c)".
    /// </summary>
    public class GroupParselet : IPrefixParselet
    {
        public ISimpleExpression Parse(IParser parser, IToken<TokenType> token)
        {
            var simpleExpression = parser.ParseExpression();
            parser.Consume(TokenType.RIGHT_PAREN);
            return simpleExpression;
        }
    }
}
