
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
        public ISimpleExpression Left { get; set; }
        public ISimpleExpression Right { get; set; }

        public ISimpleExpression Parse(IParser<TokenType> parser, IToken<TokenType> token, ISimpleExpression left)
        {
            //Why -1
            Right = parser.ParseExpression(Precedence - 1);
            Left = left;
            if (left as NameExpression == null) throw new ParseException($"Expected {TokenType.NAME} but found {left.Token}.");
            return new AssignExpression(left, Right);
        }
    }
}
