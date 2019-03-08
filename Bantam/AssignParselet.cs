
using SimpleParser;

namespace Bantam
{
    public class AssignParselet : InfixParselet<TokenType>
    {
        public ISimpleExpression Left { get; set; }
        public ISimpleExpression Right { get; set; }

        public ISimpleExpression Parse(IParser<TokenType> parser, IToken<TokenType> token, ISimpleExpression left)
        {
            //Why -1
            Right = parser.ParseExpression(Precedence - 1);
            Left = left;
            if (left as NameExpression == null)throw new ParseException("The left-hand side of an assignment must be a name.");            
            return new AssignExpression(left, Right);
        }

        public int Precedence { get; } = (int)Bantam.Precedence.ASSIGNMENT;
    }
}
