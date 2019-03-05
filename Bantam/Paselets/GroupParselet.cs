using SimpleParser;

using ISimpleExpression = SimpleParser.ISimpleExpression<char>;
using IParser = SimpleParser.IParser<Bantam.TokenType, char>;

namespace Bantam.Paselets
{
    /// <summary>
    /// Parses parentheses used to group an expression, like "a * (b + c)".
    /// </summary>
    public class GroupParselet : IParselet<TokenType, char>
    {
        public ISimpleExpression Parse(IParser parser, IToken<TokenType> token)
        {
            var simpleExpression = parser.ParseExpression();
            parser.Consume(TokenType.RIGHT_PAREN);
            return simpleExpression;
        }
    }
}
