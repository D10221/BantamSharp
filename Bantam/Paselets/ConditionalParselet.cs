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
            ISimpleExpression thenArm = parser.ParseExpression();
            parser.Consume(TokenType.COLON);
            ISimpleExpression elseArm = parser.ParseExpression();

            return new ConditionalExpression(left, thenArm, elseArm);
        }

        public int Precedence
        {
            get { return (int) SimpleParser.Precedence.CONDITIONAL; }
        }
    }
}
