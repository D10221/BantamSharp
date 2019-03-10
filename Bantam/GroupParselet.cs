using SimpleParser;


using IParser = SimpleParser.IParser<Bantam.TokenType>;

namespace Bantam
{
    /// <summary>
    /// Parses token used to group an expression, like "a * (b + c)".
    /// </summary>
    public class GroupParselet : IParselet<TokenType>
    {
        public TokenType TokenType { get;}

        public ParseletType ParseletType { get; } = ParseletType.Prefix;

        public int Precedence { get; }

        public TokenType Right { get; }

        public GroupParselet(TokenType tokenType, TokenType right)
        {
            TokenType = tokenType;
            Right = right;
        }

        public ISimpleExpression Parse(IParser parser, IToken<TokenType> token, ISimpleExpression _)
        {
            var expression = parser.ParseExpression();
            parser.Consume(expected: Right);
            return expression;
        }
    }
}
