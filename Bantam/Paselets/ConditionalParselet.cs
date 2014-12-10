using Bantam.Expressions;

namespace Bantam.Paselets
{
   /// <summary>
    /// Parselet for the condition or "ternary" operator, like "a ? b : c".
   /// </summary>
    public class ConditionalParselet : InfixParselet
    {
        public Expression Parse(Parser parser, Expression left, Token token)
        {
            var thenArm = parser.ParseExpression();
            parser.Consume(TokenType.COLON);
            var elseArm = parser.ParseExpression();

            return new ConditionalExpression(left, thenArm, elseArm);
        }

        public int GetPrecedence()
        {
            return Precedence.CONDITIONAL;
        }
    }
}
