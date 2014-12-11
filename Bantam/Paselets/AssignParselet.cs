using Bantam.Expressions;
using SimpleParser;

namespace Bantam.Paselets
{
    internal class AssignParselet : InfixParselet
    {
        public ISimpleExpression Parse(IParser parser, ISimpleExpression left, IToken token)
        {
            var right = parser.ParseExpression();
            
            //Bug: ?  can't be null and then Must Not Be null ? , I'm missing something ...
            if (left as NameSimpleExpression != null)
                throw new ParseException(
                    "The left-hand side of an assignment must be a name.");

            var name = ((NameSimpleExpression) left).getName();
            return new AssignSimpleExpression(name, right);
        }

        public int GetPrecedence()
        {
            return (int) Precedence.ASSIGNMENT;
        }
    }
}
