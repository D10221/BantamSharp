using Bantam.Expressions;
using SimpleParser;

namespace Bantam.Paselets
{
   /// <summary>
    /// Parselet for the condition or "ternary" operator, like "a ? b : c".
   /// </summary>
    public class ConditionalParselet : InfixParselet
    {
        public ISimpleExpression Parse(IParser parser, ISimpleExpression left, IToken token)
        {
            var thenArm = parser.ParseExpression();
            parser.Consume(TokenType.COLON);
            var elseArm = parser.ParseExpression();

            return new ConditionalSimpleExpression(left, thenArm, elseArm);
        }

        public int GetPrecedence()
        {
            return (int) Precedence.CONDITIONAL;
        }
    }
}
