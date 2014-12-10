using System;
using Bantam.Expressions;

namespace Bantam.Paselets
{
    internal class AssignParselet : InfixParselet
    {
        public Expression Parse(Parser parser, Expression left, Token token)
        {
            Expression right = parser.ParseExpression();

            if (left as NameExpression != null)
                throw new ParseException(
                    "The left-hand side of an assignment must be a name.");

            String name = ((NameExpression) left).getName();
            return new AssignExpression(name, right);
        }

        public int GetPrecedence()
        {
            return Precedence.ASSIGNMENT;
        }
    }
}
