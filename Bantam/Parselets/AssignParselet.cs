
using SimpleParser;

namespace Bantam
{
    public class AssignParselet : IParselet<TokenType>
    {
        public AssignParselet(TokenType tokenType, int precedence)
        {
            Precedence = precedence;
            TokenType = tokenType;
        }

        public TokenType TokenType { get; }
        public int Precedence { get; }

        public ParseletType ParseletType { get; } = ParseletType.Infix;
        public ISimpleExpression<TokenType> Left { get; set; }
        public ISimpleExpression<TokenType> Right { get; set; }

        public ISimpleExpression<TokenType> Parse(IParser<TokenType> parser, IToken<TokenType> token, ISimpleExpression<TokenType> left)
        {
            //Why -1
            Right = parser.Parse(Precedence - 1);
            Left = left;
            if (left as NameExpression == null) throw new ParseletException($"Expected {TokenType.NAME} but found {left?.Token}.", this);
            return new AssignExpression(left, Right);
        }
    }
}
