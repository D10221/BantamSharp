using Bantam.Expressions;
using SimpleParser;

namespace Bantam.Paselets
{
    /// <summary>
    ///     Parselet for the condition or "ternary" operator, like "a ? b : c".
    /// </summary>
    public class ConditionalParselet : InfixParselet
    {
        public ISimpleExpression Parse(IParser parser, ISimpleExpression left, IToken token)
        {
            var thenArm = parser.ParseExpression(Precedence.ZERO);
            parser.Consume(TokenType.COLON);
            //WHy  precedence -1 
            var elseArm = parser.ParseExpression(Precedence.CONDITIONAL - 1);

            return new ConditionalExpression(left, thenArm, elseArm);
        }

        public Precedence Precedence
        {
            get { return  Precedence.CONDITIONAL; }
        }
    }
}
