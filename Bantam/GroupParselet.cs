using SimpleParser;


using IParser = SimpleParser.IParser<Bantam.TokenType>;

namespace Bantam
{
    /// <summary>
    /// Parses token used to group an expression, like "a * (b + c)".
    /// </summary>
    public class GroupParselet : IParselet<TokenType>
    {
        private readonly TokenType _right;

        public GroupParselet(TokenType right)
        {
            _right = right;
        }

        public ISimpleExpression Parse(IParser parser, IToken<TokenType> token)
        {
            var simpleExpression = parser.ParseExpression();
            parser.Consume(_right);
            return simpleExpression;
        }
    }
}
