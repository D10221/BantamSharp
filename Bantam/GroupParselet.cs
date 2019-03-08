using SimpleParser;


using IParser = SimpleParser.IParser<Bantam.TokenType>;

namespace Bantam
{
    /// <summary>
    /// Parses token used to group an expression, like "a * (b + c)".
    /// </summary>
    public class GroupParselet : IParselet<TokenType>
    {
        public TokenType TokenType {get; set;} 
        
        public ParseletType ParseletType { get; } = ParseletType.Prefix;

        public int Precedence { get; }

        private readonly TokenType _right;

        public GroupParselet(TokenType right)
        {
            _right = right;
        }

        public ISimpleExpression Parse(IParser parser, IToken<TokenType> token, ISimpleExpression _)
        {
            var expression = parser.ParseExpression();
            parser.Consume(expected: _right);
            return expression;
        }
    }
}
