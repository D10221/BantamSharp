
using SimpleParser;

namespace Bantam
{
    public class AssignParselet : InfixParselet<TokenType>
    {
        public ISimpleExpression Left { get; set; }
        public ISimpleExpression Right { get; set; }

        public ISimpleExpression Parse(IParser<TokenType> parser, ISimpleExpression left, IToken<TokenType> token)
        {
            //Why -1
            Right = parser.ParseExpression(Precedence - 1);
            Left = left;
            //if (left as NameExpression == null)throw new ParseException("The left-hand side of an assignment must be a name.");

            var name = ((NameExpression)left).Name;
            return new AssignExpression(name, Right);
        }

        public int Precedence { get; } = (int)Bantam.Precedence.ASSIGNMENT;
    }
}
