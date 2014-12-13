using Bantam.Expressions;
using SimpleParser;

namespace Bantam.Paselets
{
    public class AssignParselet : InfixParselet
    {
        public ISimpleExpression Left { get; set; }
        public ISimpleExpression Right { get; set; }

        public ISimpleExpression Parse(IParser parser, ISimpleExpression left, IToken token)
        {
            Right = parser.ParseExpression();
            Left = left;      
            if (left as NameExpression == null)
                throw new ParseException(
                    "The left-hand side of an assignment must be a name.");

            var name = ((NameExpression) left).GetName();
            return new AssignExpression(name, Right);
        }

        public int Precedence
        {
            get { return (int) SimpleParser.Precedence.ASSIGNMENT; }
        }
    }
}
